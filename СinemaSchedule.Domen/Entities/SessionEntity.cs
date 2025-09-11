namespace СinemaSchedule.Domen.Entities;

public class SessionEntity : BaseEntity
{
    public int MovieId { get; set; }
    public MovieEntity Movie { get; set; }
    
    public int HallId { get; set; }
    public HallEntity Hall { get; set; }
    public List<SessionPriceOverrideEntity> PriceOverride { get; set; } = new();
    
    public DateTime StartTime { get; set; }
    
    public decimal BasePrice { get; set; }
    
    public bool IsActive { get; set; } = false;
    
    public DateTime? ActiveFromDate { get; set; }
    
    public int DurationDays { get; set; } = 1;
    
}