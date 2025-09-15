using FluentValidation;

namespace СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieValidator()
    {
        RuleFor(x => x.dto.Title)
            .NotEmpty().WithMessage("Название фильма не может быть пустым")
            .Length(1, 200).WithMessage("Длина названия должна быть от 1 до 200 символов");

        RuleFor(x => x.dto.ReleaseYear)
            .InclusiveBetween((short)1895, (short)(DateTime.Now.Year + 1))
            .WithMessage($"Год выпуска должен быть от 1895 до {DateTime.Now.Year + 1}");

        RuleFor(x => x.dto.Duration)
            .InclusiveBetween((short)1, (short)500)
            .WithMessage("Длительность фильма должна быть от 1 до 500 минут");

        RuleFor(x => x.dto.AgeLimit)
            .NotEmpty().WithMessage("Укажите возрастное ограничение")
            .Must(limit => new[] { "0+", "6+", "12+", "16+", "18+" }.Contains(limit))
            .WithMessage("Допустимые значения: 0+, 6+, 12+, 16+, 18+");
        RuleFor(x => x.dto.GenreIds)
            .NotNull().WithMessage("Список жанров не может быть null");
    }
}