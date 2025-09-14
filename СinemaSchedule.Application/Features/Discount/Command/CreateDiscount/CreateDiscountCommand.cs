using MediatR;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.Discount.Command.CreateDiscount;

public record CreateDiscountCommand(CreateDiscountDto dto) : IRequest<CustomResult<DiscountEntity>>;