using FluentValidation;

namespace СinemaSchedule.Application.Features.Hall.Command.CreateCommand;

public class CreateHallValidator : AbstractValidator<CreateHallCommand>
{
    public CreateHallValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty()
            .WithMessage("Название зала обязательно для заполнения")
            .MinimumLength(2)
            .WithMessage("Название зала должно содержать минимум 2 символа")
            .MaximumLength(100)
            .WithMessage("Название зала не может превышать 100 символов")
            .Matches(@"^[a-zA-Zа-яА-Я0-9\s\.\-]+$")
            .WithMessage("Название зала может содержать только буквы, цифры, пробелы, точки и дефисы");

        RuleFor(x => x.Dto.TechnicalBreakDuration)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Длительность технического перерыва не может быть отрицательной")
            .LessThanOrEqualTo(1440)
            .WithMessage("Длительность технического перерыва не может превышать 1440 минут (24 часа)");
    }
}