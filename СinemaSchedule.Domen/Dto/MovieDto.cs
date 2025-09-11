namespace СinemaSchedule.Domen.Dto;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public short ReleaseYear { get; set; }
    public short Duration { get; set; }
    public string AgeLimit { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<string> Genres { get; set; } = new();
    public DateTime? NextSessionTime { get; set; }
}