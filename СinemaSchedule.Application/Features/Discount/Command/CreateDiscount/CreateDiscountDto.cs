namespace СinemaSchedule.Application.Features.Discount.Command.CreateDiscount;

public class CreateDiscountDto
{
    public decimal Percentage { get; set; }
    public DateTime EffectiveDate { get; set; }
}