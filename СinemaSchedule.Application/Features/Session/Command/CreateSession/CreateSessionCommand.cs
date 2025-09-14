using MediatR;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.Session.Command.CreateSession;

public record CreateSessionCommand(CreateSessionDto dto) : IRequest<CustomResult<SessionEntity>>;