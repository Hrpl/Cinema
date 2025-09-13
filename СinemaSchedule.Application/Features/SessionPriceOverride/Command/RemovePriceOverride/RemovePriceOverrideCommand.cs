using MediatR;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.SessionPriceOverride.Command.RemovePriceOverride;

public record RemovePriceOverrideCommand(int Id) : IRequest<MbResult<SessionPriceOverrideEntity>>;