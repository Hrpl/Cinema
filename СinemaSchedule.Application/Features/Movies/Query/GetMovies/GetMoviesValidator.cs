using FluentValidation;

namespace СinemaSchedule.Application.Features.Movies.Query.GetMovies;

public class GetMoviesValidator : AbstractValidator<GetMoviesQuery>
{
    public GetMoviesValidator()
    {
        RuleFor(x => x.Dto.SearchTerm)
            .MaximumLength(100)
            .WithMessage("Поисковый запрос не должен превышать 100 символов")
            .When(x => !string.IsNullOrEmpty(x.Dto.SearchTerm));

        RuleFor(x => x.Dto.GenreIds)
            .Must(ids => ids == null || ids.All(id => id > 0))
            .WithMessage("Все идентификаторы жанров должны быть положительными числами")
            .Must(ids => ids == null || ids.Distinct().Count() == ids.Count)
            .WithMessage("Жанры не должны повторяться");

        RuleFor(x => x.Dto.MinYear)
            .InclusiveBetween(1895, DateTime.Now.Year)
            .WithMessage($"Минимальный год должен быть между 1895 и {DateTime.Now.Year}")
            .When(x => x.Dto.MinYear.HasValue);

        RuleFor(x => x.Dto.MaxYear)
            .InclusiveBetween(1895, DateTime.Now.Year + 1)
            .WithMessage($"Максимальный год должен быть между 1895 и {DateTime.Now.Year + 1}")
            .When(x => x.Dto.MaxYear.HasValue);

        RuleFor(x => x)
            .Must(x => !x.Dto.MinYear.HasValue || !x.Dto.MaxYear.HasValue || x.Dto.MinYear <= x.Dto.MaxYear)
            .WithMessage("Минимальный год не может быть больше максимального");

        RuleFor(x => x.Dto.MinAgeRestriction)
            .InclusiveBetween(0, 21)
            .WithMessage("Минимальное возрастное ограничение должно быть от 0 до 21")
            .When(x => x.Dto.MinAgeRestriction.HasValue);

        RuleFor(x => x.Dto.MaxAgeRestriction)
            .InclusiveBetween(0, 21)
            .WithMessage("Максимальное возрастное ограничение должно быть от 0 до 21")
            .When(x => x.Dto.MaxAgeRestriction.HasValue);
    }
}