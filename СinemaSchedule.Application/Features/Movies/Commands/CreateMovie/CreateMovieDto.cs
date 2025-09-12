namespace СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public short ReleaseYear { get; set; }
    public short Duration { get; set; }
    public string AgeLimit { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public List<int> GenreIds { get; set; } = new();
}