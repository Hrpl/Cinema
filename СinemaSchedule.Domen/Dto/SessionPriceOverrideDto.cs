namespace СinemaSchedule.Domen.Dto;

public class SessionPriceOverrideDto
{
    public int Id { get; set; }
    
    public int SessionId { get; set; }
    
    public DateTime EffectiveFrom { get; set; }
    
    public decimal NewPrice { get; set; }
}