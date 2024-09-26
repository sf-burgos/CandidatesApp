using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp._3.Infrastructure.Queries;
using CandidatesApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CandidatesApp._1.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController:ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidates()
        {
            var candidates = await _mediator.Send(new GetAllCandidatesQuery());
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidateById(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery(id));

            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }

        [HttpPost]
        public async Task<ActionResult<Candidate>> CreateCandidate([FromBody] CreateCandidateCommand command)
        {
            var createdCandidate = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCandidateById), new { id = createdCandidate.Id }, createdCandidate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var command = new DeleteCandidateCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
