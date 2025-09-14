using MediatR;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.Session.Query.GetSession;

public record GetSessionsQuery(GetSessionDto dto) : IRequest<CustomResult<List<MovieSessionGroup>>>;