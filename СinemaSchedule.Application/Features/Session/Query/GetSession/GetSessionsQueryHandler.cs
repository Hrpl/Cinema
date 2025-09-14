using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Session.Query.GetSession;

public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, CustomResult<List<MovieSessionGroup>>>
{
    private readonly IGetSessionRepository<GetSessionDto, List<MovieSessionGroup>> _repository;
    private readonly ILogger<GetSessionsQueryHandler> _logger;

    public GetSessionsQueryHandler(IGetSessionRepository<GetSessionDto, List<MovieSessionGroup>> repository, ILogger<GetSessionsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CustomResult<List<MovieSessionGroup>>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetAll(request.dto);
            _logger.LogInformation("Get Sessions query executed");
            return CustomResult<List<MovieSessionGroup>>.Success(result) ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "В запросе на получение сессии произошла ошибка");
            return CustomResult<List<MovieSessionGroup>>.Failure("В запросе на получение сессии произошла ошибка");
        }
    }
}