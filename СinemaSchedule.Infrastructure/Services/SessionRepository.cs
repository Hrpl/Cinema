using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Application.Features.Session.Query.GetSession;
using СinemaSchedule.Domen;
using СinemaSchedule.Domen.Dto;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Infrastructure.Services;
public class SessionRepository : ISessionRepository, IGetSessionRepository <GetSessionDto, List<MovieSessionGroup>>
    {
        private readonly CinemaSheduleAppContext _context;

        public SessionRepository(CinemaSheduleAppContext context)
        {
            _context = context;
        }

        public async Task<List<MovieSessionGroup>> GetAll(GetSessionDto request)
        {
            var query = _context.Session
                .Include(s => s.Movie)
                .ThenInclude(m => m.MovieGenreEntities)
                .ThenInclude(mv => mv.Genre)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .Where(s => s.IsActive);

            // Apply date filtering
            query = ApplyDateFilter(query, request);

            // Apply other filters
            query = ApplyFilters(query, request);

            var sessions = await query.ToListAsync();

            // Group by movie and order
            var groupedSessions = sessions
                .GroupBy(s => s.Movie)
                .Select(g => new MovieSessionGroup
                {
                    Movie = new MovieDto
                    {
                        Id = g.Key.Id,
                        Title = g.Key.Title,
                        ReleaseYear = g.Key.ReleaseYear,
                        AgeLimit = g.Key.AgeLimit,
                        Duration = g.Key.Duration,
                        Genres = g.Key.MovieGenreEntities.Select(mg => mg.Genre.Name).ToList()
                    },
                    Sessions = g.OrderBy(s => s.StartTime)
                        .Select(s => new SessionDto
                        {
                            Id = s.Id,
                            MovieId = s.MovieId,
                            HallId = s.HallId,
                            HallName = s.Hall.Name,
                            StartTime = s.StartTime,
                            Price = GetCurrentPrice(s),
                            IsActive = s.IsActive
                        })
                        .ToList()
                })
                .OrderBy(g => g.Sessions.Min(s => s.StartTime))
                .ToList();

            return groupedSessions;
        }

        public async Task<List<SessionEntity>> GetByMovieIdAsync(int movieId)
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .Where(s => s.MovieId == movieId)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<SessionEntity>> GetOverlappingSessionsAsync(int hallId, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Where(s => s.HallId == hallId &&
                           s.IsActive &&
                           (s.ActiveFromDate == null || s.ActiveFromDate <= DateTimeOffset.UtcNow) &&
                           s.StartTime < endTime &&
                           s.StartTime.AddMinutes(s.Movie.Duration + s.Hall.TechnicalBreakDuration) > startTime)
                .ToListAsync();
        }

        public async Task AddAsync(SessionEntity session)
        {
            await _context.Session.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SessionEntity session)
        {
            _context.Session.Update(session);
        }
        
        private IQueryable<SessionEntity> ApplyDateFilter(IQueryable<SessionEntity> query, GetSessionDto request)
    {
        if (request.SpecificDate.HasValue)
        {
            var date = request.SpecificDate.Value.Date;
            return query.Where(s => s.StartTime.Date == date);
        }

        if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            var startDate = request.StartDate.Value.Date;
            var endDate = request.EndDate.Value.Date;
            return query.Where(s => s.StartTime.Date >= startDate && s.StartTime.Date <= endDate);
        }

        return query.Where(s => s.StartTime >= DateTime.Today);
    }

    private IQueryable<SessionEntity> ApplyFilters(IQueryable<SessionEntity> query, GetSessionDto request)
    {
        if (request.GenreIds != null && request.GenreIds.Any())
        {
            query = query.Where(s => s.Movie.MovieGenreEntities.Any(mg => request.GenreIds.Contains(mg.GenreId)));
        }

        if (request.ReleaseYear.HasValue)
        {
            query = query.Where(s => s.Movie.ReleaseYear == request.ReleaseYear.Value);
        }

        if (request.AgeRestriction.HasValue)
        {
            query = query.Where(s => Convert.ToInt32(s.Movie.AgeLimit) == request.AgeRestriction.Value);
        }

        if (request.MinDuration.HasValue)
        {
            query = query.Where(s => s.Movie.Duration >= request.MinDuration.Value);
        }

        if (request.MaxDuration.HasValue)
        {
            query = query.Where(s => s.Movie.Duration <= request.MaxDuration.Value);
        }

        if (request.HallIds != null && request.HallIds.Any())
        {
            query = query.Where(s => request.HallIds.Contains(s.HallId));
        }

        return query;
    }

    private decimal GetCurrentPrice(SessionEntity session)
    {
        var currentPriceOverride = session.PriceOverride
            .Where(po => po.EffectiveFrom <= DateTime.Now)
            .OrderByDescending(po => po.EffectiveFrom)
            .FirstOrDefault();

        return currentPriceOverride?.NewPrice ?? session.BasePrice;
    }
}