using CodeChallenge.Application.Exceptions;
using CodeChallenge.Application.Features.Movies.Commands;
using CodeChallenge.Application.Features.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMediator mediator, ILogger<MovieController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        [HttpGet(Name = "GetMovies")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetMovies()
        {
            try
            {
                var result = await _mediator.Send(new GetAllMovieQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetMovieByIdQuery() { Id = id });
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ByDirector/{directorId}", Name = "GetMovieByDirectorId")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetByMovieId(Guid directorId)
        {
            try
            {
                var result = await _mediator.Send(new GetMovieByDirectorIdQuery() { DirectorId = directorId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "CreateMovie")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateMovie([FromBody] CreateMovieCommand command)
        {
            if (command == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut(Name = "UpdateMovie")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateMovie([FromBody] UpdateMovieCommand command)
        {
            if (command == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteMovie(Guid id)
        {
            try
            {
                var command = new DeleteMovieCommand() { Id = id };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}