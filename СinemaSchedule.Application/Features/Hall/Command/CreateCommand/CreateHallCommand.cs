namespace СinemaSchedule.Application.Features.Hall.Command.CreateCommand;

public class CreateHallCommand
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int TechnicalBreakDuration { get; set; }
}