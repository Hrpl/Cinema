using FluentValidation;
namespace СinemaSchedule.Application.Features.Session.Query.GetSession;

public class GetSessionValidator : AbstractValidator<GetSessionsQuery>
{
    public GetSessionValidator()
    {
        RuleFor(x => x.dto.GenreIds)
            .Must(ids => ids == null || ids.Length > 0)
            .WithMessage("Список идентификаторов жанров не может быть пустым")
            .Must(ids => ids == null || ids.All(id => id > 0))
            .WithMessage("Все идентификаторы жанров должны быть положительными числами");

        RuleFor(x => x.dto.ReleaseYear)
            .Must(year => year == null || (year >= 1895 && year <= DateTime.Now.Year + 1))
            .WithMessage($"Год выпуска должен быть между 1895 и {DateTime.Now.Year + 1}");

        RuleFor(x => x.dto.AgeRestriction)
            .Must(age => age == null || (age >= 0 && age <= 21))
            .WithMessage("Возрастное ограничение должно быть от 0 до 21");

        RuleFor(x => x.dto.MaxDuration)
            .Must(duration => duration == null || duration > 0)
            .WithMessage("Максимальная продолжительность должна быть положительным числом");

        RuleFor(x => x.dto.HallIds)
            .Must(ids => ids == null || ids.Length > 0)
            .WithMessage("Список идентификаторов залов не может быть пустым")
            .Must(ids => ids == null || ids.All(id => id > 0))
            .WithMessage("Все идентификаторы залов должны быть положительными числами");

        RuleFor(x => x.dto.SpecificDate)
            .Must(date => date == null || date >= DateTime.Today)
            .WithMessage("Конкретная дата не может быть в прошлом");

        RuleFor(x => x.dto.EndDate)
            .Must(date => date == null || date >= DateTime.Today)
            .WithMessage("Дата окончания не может быть в прошлом");
    }
}