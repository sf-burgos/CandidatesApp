using AutoMapper;
using CandidatesApp._2.Aplication.DTOs;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp._3.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CandidatesApp._1.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CandidateController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetAllCandidates()
        {
            var candidates = await _mediator.Send(new GetAllCandidatesQuery());
            var candidateDTOs = _mapper.Map<IEnumerable<CandidateDTO>>(candidates);
            return Ok(candidateDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidateById(int id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery(id));

            if (candidate == null)
            {
                return NotFound();
            }

            var candidateDTO = _mapper.Map<CandidateDTO>(candidate);
            return Ok(candidateDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateCandidate([FromBody] CreateCandidateCommand command)
        {
            var createdCandidate = await _mediator.Send(command);
            var candidateDTO = _mapper.Map<CandidateDTO>(createdCandidate);
            return CreatedAtAction(nameof(GetCandidateById), new { id = candidateDTO.Id }, candidateDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var command = new DeleteCandidateCommand(id);
            var result = await _mediator.Send(command);

            if (string.IsNullOrWhiteSpace(result))
            {
                return NotFound("The candidate ID in the URL No Found to delete");
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

            var updatedCandidate = await _mediator.Send(command);

            if (updatedCandidate == null)
            {
                return NotFound($"Candidate with ID {id} does not exist.");
            }

            var candidateDTO = _mapper.Map<CandidateDTO>(updatedCandidate);
            return Ok(candidateDTO);
        }
    }
}
