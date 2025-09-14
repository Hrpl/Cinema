namespace СinemaSchedule.Domen.Dto;

public class SessionDto
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public string HallName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}