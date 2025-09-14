using СinemaSchedule.Domen.Dto;

namespace СinemaSchedule.Application.Features.Session.Query.GetSession;

public class MovieSessionGroup
{
    public MovieDto Movie { get; set; }
    public List<SessionDto> Sessions { get; set; } = new();
}