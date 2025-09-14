using MediatR;
using СinemaSchedule.Domen.Dto;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.Movies.Query.GetMovies;

public record GetMoviesQuery(GetMoviesDto Dto) : IRequest<CustomResult<List<MovieDto>>>;