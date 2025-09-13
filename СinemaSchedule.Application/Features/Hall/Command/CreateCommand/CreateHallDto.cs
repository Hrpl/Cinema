namespace СinemaSchedule.Application.Features.Hall.Command.CreateCommand;

public class CreateHallDto
{
    public string Name { get; set; } = string.Empty;
    public short CountPlace { get; set; }
    public int TechnicalBreakDuration { get; set; }
}