using CandidatesApp.Application.Commands;
using CandidatesApp.Application.DTOs;
using CandidatesApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CandidatesApp.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetAllCandidates()
        {
            var candidateDTOs = await _mediator.Send(new GetAllCandidatesQuery());
            return Ok(candidateDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidateById(int id)
        {
            var candidateDTO = await _mediator.Send(new GetCandidateByIdQuery(id));

            if (candidateDTO == null)
            {
                return NotFound();
            }

            return Ok(candidateDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateCandidate([FromBody] CreateCandidateCommand command)
        {
            var createdCandidateDTO = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCandidateById), new { id = createdCandidateDTO.Id }, createdCandidateDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var result = await _mediator.Send(new DeleteCandidateCommand(id));

            if (string.IsNullOrWhiteSpace(result))
            {
                return NotFound("The candidate ID in the URL was not found to delete.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CandidateDTO>> UpdateCandidate(int id, [FromBody] UpdateCandidateCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The candidate ID in the URL does not match the one in the body. Please check and try again.");
            }

            var updatedCandidateDTO = await _mediator.Send(command);

            if (updatedCandidateDTO == null)
            {
                return NotFound($"Candidate with ID {id} does not exist.");
            }

            return Ok(updatedCandidateDTO);
        }
    }
}
