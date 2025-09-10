namespace СinemaSchedule.Domen;

public class MovieEntity : BaseEntity
{
    public short ReleaseYear { get; set; }
    public short Duration { get; set; } // в минутах
    public byte AgeLimit { get; set; }
    
    public List<MovieGenreEntity> MovieGenreEntities { get; set; } = new();
    
    public string? Poster { get; set; }
}