namespace СinemaSchedule.Domen.Entities;

public class HallEntity : BaseEntity
{
    public string? Name { get; set; }
    public short CountPlace {get; set;}
    public int TechnicalBreakDuration { get; set; } // в минутах
}