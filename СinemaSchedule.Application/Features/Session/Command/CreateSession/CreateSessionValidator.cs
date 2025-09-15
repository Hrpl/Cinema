using FluentValidation;

namespace СinemaSchedule.Application.Features.Session.Command.CreateSession;

public class CreateSessionValidator : AbstractValidator<CreateSessionCommand>
{
    public CreateSessionValidator()
    {
        RuleFor(x => x.dto.MovieId)
            .GreaterThan(0)
            .WithMessage("ID фильма должен быть положительным числом");

        RuleFor(x => x.dto.HallId)
            .GreaterThan(0)
            .WithMessage("ID зала должен быть положительным числом");

        RuleFor(x => x.dto.StartTime)
            .NotEmpty()
            .WithMessage("Время начала сеанса обязательно для заполнения")
            .GreaterThanOrEqualTo(DateTime.Now.AddMinutes(-5))
            .WithMessage("Время начала сеанса не может быть в прошлом")
            .LessThanOrEqualTo(DateTime.Now.AddYears(1))
            .WithMessage("Время начала сеанса не может быть более чем на год вперед");

        RuleFor(x => x.dto.BasePrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Базовая цена не может быть отрицательной")
            .LessThanOrEqualTo(10000)
            .WithMessage("Базовая цена не может превышать 10000 рублей");

        RuleFor(x => x.dto.ActiveFromDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Дата активации не может быть в прошлом")
            .LessThanOrEqualTo(DateTime.Today.AddMonths(3))
            .WithMessage("Дата активации не может быть более чем на 3 месяца вперед")
            .When(x => x.dto.ActiveFromDate.HasValue);

        RuleFor(x => x.dto.DurationDays)
            .InclusiveBetween(1, 365)
            .WithMessage("Продолжительность показа должна быть от 1 до 365 дней");
    }
}