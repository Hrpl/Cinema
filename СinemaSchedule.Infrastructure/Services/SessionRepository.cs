using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Infrastructure.Services;
public class SessionRepository : ISessionRepository
    {
        private readonly CinemaSheduleAppContext _context;

        public SessionRepository(CinemaSheduleAppContext context)
        {
            _context = context;
        }

        public async Task<SessionEntity?> GetByIdAsync(int id)
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SessionEntity>> GetAllAsync()
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<SessionEntity>> GetActiveAsync()
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .Where(s => s.IsActive && 
                           (s.ActiveFromDate == null || s.ActiveFromDate <= DateTime.Now) &&
                           s.StartTime > DateTime.Now)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
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

        public async Task<List<SessionEntity>> GetByHallIdAsync(int hallId)
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .Where(s => s.HallId == hallId)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<SessionEntity>> GetOverlappingSessionsAsync(int hallId, DateTime startTime, DateTime endTime)
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Where(s => s.HallId == hallId &&
                           s.IsActive &&
                           (s.ActiveFromDate == null || s.ActiveFromDate <= DateTime.Now) &&
                           s.StartTime < endTime &&
                           s.StartTime.AddMinutes(s.Movie.Duration + s.Hall.TechnicalBreakDuration) > startTime)
                .ToListAsync();
        }

        public async Task<List<SessionEntity>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var startDateTime = startDate.Date;
            var endDateTime = endDate.Date.AddDays(1).AddTicks(-1);

            return await _context.Session
                .Include(s => s.Movie)
                .ThenInclude(m => m.MovieGenreEntities)
                .ThenInclude(mg => mg.Genre)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .Where(s => s.StartTime >= startDateTime && 
                           s.StartTime <= endDateTime &&
                           s.IsActive &&
                           (s.ActiveFromDate == null || s.ActiveFromDate <= DateTime.Now))
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<SessionEntity>> GetSessionsWithFiltersAsync(
            List<int>? genreIds = null,
            int? minYear = null,
            int? maxYear = null,
            int? minAgeRestriction = null,
            int? maxAgeRestriction = null,
            int? minDuration = null,
            int? maxDuration = null,
            decimal? maxPrice = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            List<int>? hallIds = null)
        {
            var query = _context.Session
                .Include(s => s.Movie)
                .ThenInclude(m => m.MovieGenreEntities)
                .ThenInclude(mg => mg.Genre)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .Where(s => s.IsActive && 
                           (s.ActiveFromDate == null || s.ActiveFromDate <= DateTime.Now) &&
                           s.StartTime > DateTime.Now);

            // Apply filters
            if (genreIds?.Any() == true)
            {
                query = query.Where(s => s.Movie.MovieGenreEntities.Any(mg => genreIds.Contains(mg.GenreId)));
            }

            if (minYear.HasValue)
            {
                query = query.Where(s => s.Movie.ReleaseYear >= minYear.Value);
            }

            if (maxYear.HasValue)
            {
                query = query.Where(s => s.Movie.ReleaseYear <= maxYear.Value);
            }

            if (minAgeRestriction.HasValue)
            {
                query = query.Where(s => Convert.ToInt32(s.Movie.AgeLimit) >= minAgeRestriction.Value);
            }

            if (maxAgeRestriction.HasValue)
            {
                query = query.Where(s => Convert.ToInt32(s.Movie.AgeLimit) <= maxAgeRestriction.Value);
            }

            if (minDuration.HasValue)
            {
                query = query.Where(s => s.Movie.Duration >= minDuration.Value);
            }

            if (maxDuration.HasValue)
            {
                query = query.Where(s => s.Movie.Duration <= maxDuration.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(s => s.BasePrice <= maxPrice.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(s => s.StartTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(s => s.StartTime <= endDate.Value);
            }

            if (hallIds?.Any() == true)
            {
                query = query.Where(s => hallIds.Contains(s.HallId));
            }

            return await query
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<SessionEntity?> GetSessionWithCurrentPriceAsync(int sessionId, DateTime date)
        {
            return await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride
                    .Where(sp => sp.EffectiveFrom <= date)
                    .OrderByDescending(sp => sp.EffectiveFrom)
                    .Take(1))
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task<List<SessionEntity>> GetSessionsWithPricesByDateAsync(DateTime date)
        {
            var sessions = await _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.PriceOverride)
                .Where(s => s.StartTime.Date == date.Date &&
                           s.IsActive &&
                           (s.ActiveFromDate == null || s.ActiveFromDate <= DateTime.Now))
                .ToListAsync();

            // For each session, get the effective price for the given date
            foreach (var session in sessions)
            {
                var effectivePrice = session.PriceOverride
                    .Where(sp => sp.EffectiveFrom <= date)
                    .OrderByDescending(sp => sp.EffectiveFrom)
                    .FirstOrDefault();

                if (effectivePrice != null)
                {
                    session.BasePrice = effectivePrice.NewPrice;
                }
            }

            return sessions;
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

        public async Task DeleteAsync(int id)
        {
            var session = await GetByIdAsync(id);
            if (session != null)
            {
                _context.Session.Remove(session);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Session.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsMovieActiveAsync(int movieId)
        {
            return await _context.Movie
                .AnyAsync(m => m.Id == movieId && m.IsInRelease);
        }

        public async Task<bool> IsHallAvailableAsync(int hallId, DateTime startTime, DateTime endTime, int? excludeSessionId = null)
        {
            var query = _context.Session
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Where(s => s.HallId == hallId &&
                           s.IsActive &&
                           (s.ActiveFromDate == null || s.ActiveFromDate <= DateTime.Now) &&
                           s.StartTime < endTime &&
                           s.StartTime.AddMinutes(s.Movie.Duration + s.Hall.TechnicalBreakDuration) > startTime);

            if (excludeSessionId.HasValue)
            {
                query = query.Where(s => s.Id != excludeSessionId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task<List<SessionEntity>> GetSessionsRequiringActivationAsync()
        {
            return await _context.Session
                .Where(s => !s.IsActive && 
                           s.ActiveFromDate.HasValue && 
                           s.ActiveFromDate <= DateTime.Now)
                .ToListAsync();
        }
    }