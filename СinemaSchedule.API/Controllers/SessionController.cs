using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using СinemaSchedule.Application.Features.Hall.Command.CreateCommand;
using СinemaSchedule.Application.Features.Session.Command.CreateSession;

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
    public async Task<IActionResult> CreateHall([FromBody] CreateSessionDto dto)
    {
        var request = new CreateSessionCommand(dto);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? Created() 
            //? CreatedAtAction(nameof(GetHallById), new { id = result.Value }, result.Value) 
            : BadRequest(result);
    }
}