namespace СinemaSchedule.Domen;

public class GenreEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<MovieGenreEntity> Films { get; set; } = new();
}