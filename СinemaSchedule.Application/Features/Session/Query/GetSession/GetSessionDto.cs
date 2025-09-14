namespace СinemaSchedule.Application.Features.Session.Query.GetSession;

public class GetSessionDto
{
    public int[]? GenreIds { get; set; }
    public int? ReleaseYear { get; set; }
    public int? AgeRestriction { get; set; }
    public int? MinDuration { get; set; }
    public int? MaxDuration { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int[]? HallIds { get; set; }
    public DateTime? SpecificDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}