using Events.Application.DTO.Event;
using Events.Application.DTO.EventParticipant;
using Events.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IEventParticipantService eventParticipantService;

        public EventController(IEventService eventService, IEventParticipantService eventParticipantService)
        {
            this.eventService = eventService;
            this.eventParticipantService = eventParticipantService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await eventService.GetAllAsync(pageNumber, pageSize, cancellationToken));
        }


        [HttpGet("{location}")]
        public async Task<IActionResult> GetByLocation(string location, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await eventService.GetByLocationAsync(pageNumber, pageSize, location, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await eventService.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
        {
            return Ok(await eventService.GetByNameAsync(name, cancellationToken));
        }

        [HttpGet("{date:datetime}")]
        public async Task<IActionResult> GetByDate(DateTime date, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await eventService.GetByDateAsync(pageNumber, pageSize, date, cancellationToken));
        }


        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await eventService.GetByCategoryAsync(pageNumber, pageSize, categoryId, cancellationToken));
        }

        [HttpGet("{id}/participants")]
        public async Task<IActionResult> GetParticipants(int id, CancellationToken cancellationToken)
        {
            return Ok(await eventParticipantService.GetByIdAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] EventDTO eventDTO, CancellationToken cancellationToken)
        {
            await eventService.AddAsync(eventDTO, cancellationToken);

            return Ok();
        }

        [HttpPost("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> AddParticipant(int eventId, int participantId, CancellationToken cancellationToken)
        {

            await eventParticipantService.InsertAsync(new EventParticipantDTO { EventId = eventId, ParticipantId = participantId}, cancellationToken);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventDTO eventDTO, CancellationToken cancellationToken)
        {
            await eventService.UpdateAsync(eventDTO, cancellationToken);

            return Ok();
        }

        [HttpPut("{id}/image")]
        public async Task<IActionResult> UpdateImageEvent(int eventId, [FromBody] EventImageDTO eventDTO, CancellationToken cancellationToken)
        {
            await eventService.AddImageAsync(eventId, eventDTO, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id, CancellationToken cancellationToken)
        {
            await eventService.DeleteAsync(id, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}/participant")]
        public async Task<IActionResult> DeleteParticipantFromEvent(int id, CancellationToken cancellationToken)
        {
            await eventParticipantService.DeleteAsync(id, cancellationToken);
             
            return Ok();
        }
    }
}
