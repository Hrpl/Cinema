namespace СinemaSchedule.Application.Features.Session.Command.UpdateSessionPrice;

public class UpdateSessionPriceCommand
{
    public int SessionId { get; set; }
    public DateTime EffectiveDate { get; set; }
    public decimal Price { get; set; }
    public bool ApplyToFuture { get; set; } = false;
}