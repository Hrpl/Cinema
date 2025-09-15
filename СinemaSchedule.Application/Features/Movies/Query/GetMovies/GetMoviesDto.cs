namespace СinemaSchedule.Application.Features.Movies.Query.GetMovies;

public class GetMoviesDto
{
    public string? SearchTerm { get; set; }
    public List<int>? GenreIds { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public int? MinAgeRestriction { get; set; }
    public int? MaxAgeRestriction { get; set; }
    public bool? ActiveOnly { get; set; } = true;
}