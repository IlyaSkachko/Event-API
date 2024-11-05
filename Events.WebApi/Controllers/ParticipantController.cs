using Events.Application.DTO.Participant;
using Events.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly IParticipantService participantService;

        public ParticipantController(IParticipantService participantService)
        {
            this.participantService = participantService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await participantService.GetAllAsync(pageNumber, pageSize, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await participantService.GetByIdAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant([FromBody] CreateParticipantDTO participant, CancellationToken cancellationToken)
        {
            await participantService.InsertAsync(participant, cancellationToken);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateParticipant([FromBody] UpdateParticipantDTO participant, CancellationToken cancellationToken)
        {
            await participantService.UpdateAsync(participant, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id, CancellationToken cancellationToken)
        {
            await participantService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
