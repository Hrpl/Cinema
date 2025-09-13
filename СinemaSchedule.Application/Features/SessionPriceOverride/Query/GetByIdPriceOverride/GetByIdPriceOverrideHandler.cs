using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Domen.Dto;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.SessionPriceOverride.Query.GetByIdPriceOverride;

public class GetByIdPriceOverrideHandler : IRequestHandler<GetByIdPriceOverrideQuery, MbResult<SessionPriceOverrideDto>>
{
    private readonly ISessionPriceOverrideRepository _priceOverrideRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetByIdPriceOverrideHandler> _logger;

    public GetByIdPriceOverrideHandler(
        ISessionPriceOverrideRepository priceOverrideRepository,
        IMapper mapper,
        ILogger<GetByIdPriceOverrideHandler> logger
        )
    {
        _priceOverrideRepository = priceOverrideRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<MbResult<SessionPriceOverrideDto>> Handle(GetByIdPriceOverrideQuery request, CancellationToken cancellationToken)
    {
        var model = await _priceOverrideRepository.GetByIdAsync(request.Id);
        if (model == null)
        {
            _logger.LogError("Сущность не найдена в базе данных");
            return MbResult<SessionPriceOverrideDto>.Failure("Запись не найдена");
        }
        var result = _mapper.Map<SessionPriceOverrideDto>(model);

        return MbResult<SessionPriceOverrideDto>.Success(result);

    }
}