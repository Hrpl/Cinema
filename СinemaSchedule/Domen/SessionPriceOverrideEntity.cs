namespace СinemaSchedule.Domen;

public class SessionPriceOverrideEntity
{
    public int Id { get; set; }
    
    public int SessionId { get; set; }
    public SessionEntity Session { get; set; }
    
    public DateTime EffectiveFrom { get; set; }
    
    public decimal NewPrice { get; set; }
}