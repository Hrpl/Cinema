using MediatR;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.Hall.Command.CreateCommand;

public record CreateHallCommand(CreateHallDto Dto) : IRequest<CustomResult<HallEntity>>;