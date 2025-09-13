namespace СinemaSchedule.Application.Features.SessionPriceOverride.Command;

public class CreatePriceOverrideDto
{
    public int SessionId { get; set; }
    
    public DateTime EffectiveFrom { get; set; }
    
    public decimal NewPrice { get; set; }
}