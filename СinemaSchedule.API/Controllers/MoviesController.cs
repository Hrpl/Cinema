using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;
using СinemaSchedule.Application.Features.Movies.Commands.DeactivateMovie;
using СinemaSchedule.Application.Features.Movies.Query.GetMovies;

namespace СinemaSchedule.API.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : Controller
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Tags = new[] {"Movies"},
        Summary = "Создание нового фильма",
        Description = "Возвращает id созданного аккаунта"
    )]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto dto)
    {
        var request = new CreateMovieCommand(dto);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? Created() 
            //? CreatedAtAction(nameof(GetMovieById), new { id = result.Value }, result.Value) 
            : BadRequest(result);
    }

    [HttpPatch]
    [SwaggerOperation(
        Tags = new[] { "Movies" },
        Summary = "Удаление фильма из проката"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeactivateMovie([FromQuery] int movieId)
    {
        var request = new DeactivateMovieCommand(movieId);
        var result = await _mediator.Send(request);
        
        return result.IsSuccess 
            ? Ok("Фильм успешно убран из проката и отменены все сеансы") 
            : BadRequest(result);
    }

    [HttpGet]
    [SwaggerOperation(
        Tags = new[] { "Movies" },
        Summary = "Получение информации о фильмах"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovies([FromQuery] GetMoviesDto dto)
    {
        var request = new GetMoviesQuery(dto);
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}