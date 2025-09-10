namespace СinemaSchedule.Domen;

public class MovieGenreEntity
{
    public int Id { get; set; }
    
    public int MovieId { get; set; }   
    public MovieEntity? Movie { get; set; }
    
    public int GenreId { get; set; }
    public GenreEntity? Genre { get; set; }
}