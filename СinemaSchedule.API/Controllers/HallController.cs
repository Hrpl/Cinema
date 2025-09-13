using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using СinemaSchedule.Application.Features.Hall.Command.CreateCommand;
using СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;

namespace СinemaSchedule.API.Controllers;

public class HallController : Controller
{
    private readonly IMediator _mediator;

    public HallController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Tags = new[] {"Hall"},
        Summary = "Создание нового зала",
        Description = ""
    )]
    public async Task<IActionResult> CreateHall([FromBody] CreateHallDto dto)
    {
        var request = new CreateHallCommand(dto);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? Created() 
            //? CreatedAtAction(nameof(GetHallById), new { id = result.Value }, result.Value) 
            : BadRequest(result);
    }
}