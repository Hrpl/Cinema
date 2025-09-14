using MediatR;
using СinemaSchedule.Domen.Dto;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.SessionPriceOverride.Query.GetByIdPriceOverride;

public record GetByIdPriceOverrideQuery(int Id) : IRequest<CustomResult<SessionPriceOverrideDto>>;