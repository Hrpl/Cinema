using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using СinemaSchedule.Application.Features.Discount.Command.CreateDiscount;
using СinemaSchedule.Application.Features.SessionPriceOverride.Command;

namespace СinemaSchedule.API.Controllers;

[ApiController]
[Route("api/discount")]
public class DiscountController : Controller
{
    private readonly IMediator _mediator;

    public DiscountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Tags = new[] {"Discount"},
        Summary = "Создание скидки"
    )]
    public async Task<ActionResult> CreateSessionPriceOverride([FromBody] CreateDiscountDto dto)
    {
        var request = new CreateDiscountCommand(dto);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? Created()
            //? CreatedAtAction(nameof(GetPriceOverrideById), new { id = result.Value }, result.Value) 
            : BadRequest(result);
    }
}