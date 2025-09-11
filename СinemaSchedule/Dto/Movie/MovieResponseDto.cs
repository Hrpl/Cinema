namespace СinemaSchedule.Dto.Movie;

public class MovieResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public short ReleaseYear { get; set; }
    public short Duration { get; set; }
    public string AgeRating { get; set; }
    public List<string> Genres { get; set; }
    public bool IsInRelease { get; set; }
}