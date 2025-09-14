using MediatR;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.SessionPriceOverride.Command;

public record CreatePriceOverrideCommand(CreatePriceOverrideDto dto) : IRequest<CustomResult<SessionPriceOverrideEntity>>;