namespace СinemaSchedule.Dto.Movie;

public class CreateMovieDto
{
    public string Title { get; set; }
    
    public int ReleaseYear { get; set; }
    
    public int Duration { get; set; }
    
    public string AgeRating { get; set; }
    
    public List<int> GenreIds { get; set; } = new List<int>();
    public IFormFile? Poster { get; set; }
}