using CodeChallenge.Application.Exceptions;
using CodeChallenge.Application.Features.Directors.Commands;
using CodeChallenge.Application.Features.Directors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DirectorController> _logger;

        public DirectorController(IMediator mediator, ILogger<DirectorController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        [HttpGet(Name = "GetDirectors")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetDirectors()
        {
            try
            {
                var result = await _mediator.Send(new GetAllDirectorQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetDirectorById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetDirectorByIdQuery() { Id = id });
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "CreateDirector")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand command)
        {
            if (command == null)
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

        [HttpPut(Name = "UpdateDirector")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateDirector([FromBody] UpdateDirectorCommand command)
        {
            if (command == null)
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

        [HttpDelete("{id}", Name = "DeleteDirector")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteDirector(Guid id)
        {
            try
            {
                var command = new DeleteDirectorCommand() { Id = id };
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