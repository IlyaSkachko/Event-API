using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.DTO.EventParticipant;
using Events.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{

    [Route("api/events")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IEventParticipantService eventParticipantService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;

        public EventController(IEventService eventService, IEventParticipantService eventParticipantService, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.eventService = eventService;
            this.eventParticipantService = eventParticipantService;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
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

        [HttpGet("{eventId}/participants")]
        public async Task<IActionResult> GetParticipants(int eventId, CancellationToken cancellationToken)
        {
            return Ok(await eventParticipantService.GetByIdAsync(eventId, cancellationToken));
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] EventDTO eventDTO, CancellationToken cancellationToken)
        {
            await eventService.AddAsync(eventDTO, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPost("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> AddParticipant(int eventId, int participantId, CancellationToken cancellationToken)
        {

            await eventParticipantService.InsertAsync(new EventParticipantDTO { EventId = eventId, ParticipantId = participantId}, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDTO eventDTO, CancellationToken cancellationToken)
        {
            var _event = mapper.Map<EventDTO>(eventDTO);

            _event.Id = id;

            await eventService.UpdateAsync(_event, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPut("{eventId}/image")]
        public async Task<IActionResult> UpdateImageEvent(int eventId, IFormFile file, CancellationToken cancellationToken)
        {
            var url = await cloudinaryService.UploadImage(file);

            await eventService.AddImageAsync(eventId, url, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id, CancellationToken cancellationToken)
        {
            await eventService.DeleteAsync(id, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpDelete("{eventId}/participant/{participantId}")]
        public async Task<IActionResult> DeleteParticipantFromEvent(int eventId, int participantId, CancellationToken cancellationToken)
        {
            await eventParticipantService.DeleteAsync(eventId, participantId, cancellationToken);
             
            return Ok();
        }
    }
}
