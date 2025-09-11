namespace СinemaSchedule.Domen.Entities;

public class MovieEntity : BaseEntity
{
    public string Title { get; set; }
    public short ReleaseYear { get; set; }
    public short Duration { get; set; } // в минутах
    public string AgeLimit { get; set; }
    
    public List<MovieGenreEntity> MovieGenreEntities { get; set; } = new();
    
    public string? Poster { get; set; }
    public bool IsInRelease { get; set; }
}