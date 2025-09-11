namespace СinemaSchedule.Application.Features.Movies.Commands.UpdateMovie;

public class UpdateMovieCommand
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int Duration { get; set; }
    public int AgeRestriction { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public List<int> GenreIds { get; set; } = new();
}