using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Discount.Command.CreateDiscount;

public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CustomResult<DiscountEntity>>
{
    private readonly IDiscountRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateDiscountHandler> _logger;

    public CreateDiscountHandler(IDiscountRepository repository, IMapper mapper, ILogger<CreateDiscountHandler> logger)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<CustomResult<DiscountEntity>> Handle(CreateDiscountCommand request,
        CancellationToken cancellationToken)
    {
        DiscountEntity discount = _mapper.Map<DiscountEntity>(request.dto);

        try
        {
            await _repository.AddAsync(discount);
            _logger.LogInformation("Создание скидки");
            return CustomResult<DiscountEntity>.Success(discount);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при создании скидки ex: {ex.Message}");
            return CustomResult<DiscountEntity>.Failure("Ошибка при создании скидки");
        }
    }
}