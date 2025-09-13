namespace СinemaSchedule.Application.Features.Session.Command.CreateSession;

public class CreateSessionDto
{
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal BasePrice { get; set; }
    public DateTime? ActiveFromDate { get; set; }
    public bool IsActive { get; set; }
    public int DurationDays { get; set; }
}