using FluentValidation;
namespace СinemaSchedule.Application.Features.Discount.Command.CreateDiscount;

public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountValidator()
    {
        RuleFor(x => x.dto.DiscountPercentage)
            .GreaterThan(0)
            .WithMessage("Процент скидки должен быть больше 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Процент скидки не может превышать 100");

        RuleFor(x => x.dto.StartDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Дата начала не может быть в прошлом")
            .LessThanOrEqualTo(DateTime.Today.AddYears(1))
            .WithMessage("Дата начала не может быть более чем на год вперед");
    }
}