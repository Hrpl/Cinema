using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using СinemaSchedule.Application.Features.Hall.Command.CreateCommand;
using СinemaSchedule.Application.Features.Session.Command.CreateSession;
using СinemaSchedule.Application.Features.Session.Query.GetSession;

namespace СinemaSchedule.API.Controllers;

[ApiController]
[Route("api/session")]
public class SessionController : Controller
{
    private readonly IMediator _mediator;
    
    public SessionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Tags = new[] {"Session"},
        Summary = "Создание нового сеанса",
        Description = ""
    )]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSession([FromBody] CreateSessionDto dto)
    {
        var request = new CreateSessionCommand(dto);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? Created() 
            : BadRequest(result);
    }

    [HttpGet]
    [SwaggerOperation(
        Tags = new[] { "Session" },
        Summary = "Получение сессии",
        Description = "Возвращает отсортированный массив сессий"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSession([FromQuery] GetSessionDto dto)
    {
        var query = new GetSessionsQuery(dto);
        var result = await _mediator.Send(query);
        
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }
}