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
    public class ExperienceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ExperienceController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ExperienceDto>> AddExperience(int id, [FromBody] ExperienceDto experienceDto)
        {
            if (experienceDto == null)
            {
                return BadRequest("Experience cannot be null.");
            }

            var command = new AddExperienceCommand(id, experienceDto);

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound($"Candidate with ID {id} not found.");
            }

            return CreatedAtAction(nameof(GetExperiences), new { id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ExperienceDto>>> GetExperiences(int id)
        {
            var experiences = await _mediator.Send(new GetExperiencesByCandidateIdQuery(id));

            if (experiences == null || !experiences.Any())
            {
                return NotFound($"No experiences found for candidate with ID {id}.");
            }

            return Ok(experiences);
        }

        [HttpDelete("{experienceId}/candidate/{candidateId}")]
        public async Task<IActionResult> DeleteExperience(int experienceId, int candidateId)
        {
            var command = new DeleteExperienceCommand(candidateId, experienceId);
            var result = await _mediator.Send(command);

            if (result==null)
            {
                return NotFound($"Experience with ID {experienceId} for candidate {candidateId} does not exist.");
            }

            return Ok($"Experience with ID {experienceId} for candidate {candidateId} deleted successfully.");
        }

        [HttpPut("{experienceId}/candidate/{candidateId}")]
        public async Task<ActionResult<ExperienceDto>> UpdateExperience(int experienceId, int candidateId, [FromBody] UpdateExperienceCommand command)
        {
            if (candidateId != command.CandidateId || experienceId != command.ExperienceId)
            {
                return BadRequest("Candidate ID or Experience ID in the URL does not match the one in the body.");
            }

            var updatedExperience = await _mediator.Send(command);

            if (updatedExperience == null)
            {
                return NotFound($"Experience with ID {experienceId} for candidate with ID {candidateId} does not exist.");
            }

            return Ok(updatedExperience);
        }
    }
}
