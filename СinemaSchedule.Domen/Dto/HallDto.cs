namespace СinemaSchedule.Domen.Dto;

public class HallDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int TechnicalBreakDuration { get; set; }
}