using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;
using СinemaSchedule.Application.Features.SessionPriceOverride.Command;
using СinemaSchedule.Application.Features.SessionPriceOverride.Command.RemovePriceOverride;
using СinemaSchedule.Application.Features.SessionPriceOverride.Query.GetByIdPriceOverride;

namespace СinemaSchedule.API.Controllers;

public class PriceOverrideController : Controller
{
    private readonly IMediator _mediator;

    public PriceOverrideController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [EndpointName("GetPriceOverrideById")]
    [SwaggerOperation(
        Tags = new[] {"Price Override"},
        Summary = "Получение записи по Id"
    )]
    public async Task<ActionResult> GetPriceOverrideById(int id)
    {
        var request = new GetByIdPriceOverrideQuery(id);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess ?
            Ok(result.Value) :
            NotFound(result);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Tags = new[] {"Price Override"},
        Summary = "Создание переопределения цены на сеанс"
    )]
    public async Task<ActionResult> CreateSessionPriceOverride([FromBody] CreatePriceOverrideDto dto)
    {
        var request = new CreatePriceOverrideCommand(dto);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetPriceOverrideById), new { id = result.Value }, result.Value) 
            : BadRequest(result);
    }
    
    [HttpDelete]
    [SwaggerOperation(
        Tags = new[] { "Price Override" },
        Summary = "Удаление переопределенной цены на сеанс"
    )]
    public async Task<ActionResult> DeactivateMovie([FromQuery] int movieId)
    {
        var request = new RemovePriceOverrideCommand(movieId);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? Ok("Цена удалена") 
            : BadRequest(result);
    }
}