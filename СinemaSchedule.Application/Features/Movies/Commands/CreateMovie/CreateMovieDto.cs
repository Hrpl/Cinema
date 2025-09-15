using Microsoft.AspNetCore.Http;

namespace СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public short ReleaseYear { get; set; }
    public short Duration { get; set; }
    public string AgeLimit { get; set; }
    public IFormFile Poster { get; set; } 
    public List<int> GenreIds { get; set; } = new();
}