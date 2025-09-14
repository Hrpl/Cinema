namespace СinemaSchedule.Application.Features.Discount.Command.CreateDiscount;

public class CreateDiscountDto
{
    public decimal DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
}