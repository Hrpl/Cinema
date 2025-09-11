namespace СinemaSchedule.Application.Features.Discount.Command.CreateDiscount;

public class CreateDiscountCommand
{
    public decimal Percentage { get; set; }
    public DateTime EffectiveDate { get; set; }
}