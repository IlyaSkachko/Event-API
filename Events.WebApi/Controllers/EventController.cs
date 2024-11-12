using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.DTO.EventParticipant;
using Events.Application.UseCases.CacheUseCase.ImageCache;
using Events.Application.UseCases.CacheUseCase.ImageCache.Interfaces;
using Events.Application.UseCases.CloudinaryUseCase.Upload.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Delete.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Get.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Insert.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Update.Interfaces;
using Events.Application.UseCases.EventUseCase.Delete.Interfaces;
using Events.Application.UseCases.EventUseCase.Get.Interfaces;
using Events.Application.UseCases.EventUseCase.Insert.Interfaces;
using Events.Application.UseCases.EventUseCase.Update.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Events.WebApi.Controllers
{

    [Route("api/events")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IGetAllEventUseCase getAllEventUseCase;
        private readonly IGetByIdEventUseCase getByIdEventUseCase;
        private readonly IGetByLocationEventUseCase getByLocationEventUseCase;
        private readonly IGetByNameEventUseCase getByNameEventUseCase;
        private readonly IGetByCategoryEventUseCase getByCategoryEventUseCase;
        private readonly IGetByDateEventUseCase getByDateEventUseCase;
        private readonly IInsertEventUseCase insertEventUseCase;
        private readonly IUpdateImageEventUseCase updateImageEventUseCase;
        private readonly IUpdateEventUseCase updateEventUseCase;
        private readonly IDeleteEventUseCase deleteEventUseCase;
        private readonly IInsertEventParticipantUseCase insertEventParticipantUseCase;
        private readonly IDeleteEventParticipantUseCase deleteEventParticipantUseCase;
        private readonly IGetAllEventParticipantUseCase getAllEventParticipantUseCase;
        private readonly IGetByIdEventParticipantUseCase getByIdEventParticipant;
        private readonly IUpdateEventParticipantUseCase updateEventParticipantUseCase;
        private readonly IMapper mapper;
        private readonly ICacheImageUseCase cacheImageUseCase;
        private readonly IUploadImageCloudinaryUseCase uploadImageCloudinaryUseCase;

        public EventController(IGetAllEventUseCase getAllEventUseCase, IGetByIdEventUseCase getByIdEventUseCase, IGetByLocationEventUseCase getLocationEventUseCase,
            IGetByNameEventUseCase getNameEventUseCase, IGetByCategoryEventUseCase getByCategoryEventUseCase, IGetByDateEventUseCase getByDateEventUseCase,
            IInsertEventUseCase insertEventUseCase, IUpdateImageEventUseCase updateImageEventUseCase, IUpdateEventUseCase updateEventUseCase, 
            IDeleteEventUseCase deleteEventUseCase, IInsertEventParticipantUseCase insertEventParticipantUseCase, 
            IDeleteEventParticipantUseCase deleteEventParticipantUseCase, IGetAllEventParticipantUseCase getAllEventParticipantUseCase, 
            IGetByIdEventParticipantUseCase getByIdEventParticipant, IUpdateEventParticipantUseCase updateEventParticipantUseCase, 
            IMapper mapper, ICacheImageUseCase cacheImageUseCase, IUploadImageCloudinaryUseCase uploadImageCloudinaryUseCase)
        {
            this.getAllEventUseCase = getAllEventUseCase;
            this.getByIdEventUseCase = getByIdEventUseCase;
            this.getByLocationEventUseCase = getLocationEventUseCase;
            this.getByNameEventUseCase = getNameEventUseCase;
            this.getByCategoryEventUseCase = getByCategoryEventUseCase;
            this.getByDateEventUseCase = getByDateEventUseCase;
            this.insertEventUseCase = insertEventUseCase;
            this.updateImageEventUseCase = updateImageEventUseCase;
            this.updateEventUseCase = updateEventUseCase;
            this.deleteEventUseCase = deleteEventUseCase;
            this.insertEventParticipantUseCase = insertEventParticipantUseCase;
            this.deleteEventParticipantUseCase = deleteEventParticipantUseCase;
            this.getAllEventParticipantUseCase = getAllEventParticipantUseCase;
            this.getByIdEventParticipant = getByIdEventParticipant;
            this.updateEventParticipantUseCase = updateEventParticipantUseCase;
            this.mapper = mapper;
            this.cacheImageUseCase = cacheImageUseCase;
            this.uploadImageCloudinaryUseCase = uploadImageCloudinaryUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await getAllEventUseCase.ExecuteAsync(pageNumber, pageSize, cancellationToken));
        }


        [HttpGet("{location}")]
        public async Task<IActionResult> GetByLocation(string location, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await getByLocationEventUseCase.ExecuteAsync(pageNumber, pageSize, location, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await getByIdEventUseCase.ExecuteAsync(id, cancellationToken));
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
        {
            return Ok(await getByNameEventUseCase.ExecuteAsync(name, cancellationToken));
        }

        [HttpGet("{date:datetime}")]
        public async Task<IActionResult> GetByDate(DateTime date, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await getByDateEventUseCase.ExecuteAsync(pageNumber, pageSize, date, cancellationToken));
        }


        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await getByCategoryEventUseCase.ExecuteAsync(pageNumber, pageSize, categoryId, cancellationToken));
        }

        [HttpGet("{eventId}/participants")]
        public async Task<IActionResult> GetParticipants(int eventId, CancellationToken cancellationToken)
        {
            return Ok(await getByIdEventParticipant.ExecuteAsync(eventId, cancellationToken));
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] EventDTO eventDTO, CancellationToken cancellationToken)
        {
            await insertEventUseCase.ExecuteAsync(eventDTO, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPost("{eventId}/participants/{participantId}")]
        public async Task<IActionResult> AddParticipant(int eventId, int participantId, CancellationToken cancellationToken)
        {
            await insertEventParticipantUseCase.ExecuteAsync(new EventParticipantDTO { EventId = eventId, ParticipantId = participantId}, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDTO eventDTO, CancellationToken cancellationToken)
        {
            var _event = mapper.Map<EventDTO>(eventDTO);

            _event.Id = id;

            await updateEventUseCase.ExecuteAsync(_event, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPut("{eventId}/image")]
        public async Task<IActionResult> UpdateImageEvent(int eventId, IFormFile file, CancellationToken cancellationToken)
        {
            var cacheDuration = TimeSpan.FromMinutes(60); 
            
            string url = await cacheImageUseCase.ExecuteAsync(eventId, file, uploadImageCloudinaryUseCase.ExecuteAsync, cacheDuration);
            
            await updateImageEventUseCase.ExecuteAsync(eventId, url, cancellationToken);
            
            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id, CancellationToken cancellationToken)
        {
            await deleteEventUseCase.ExecuteAsync(id, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpDelete("{eventId}/participant/{participantId}")]
        public async Task<IActionResult> DeleteParticipantFromEvent(int eventId, int participantId, CancellationToken cancellationToken)
        {
            await deleteEventParticipantUseCase.ExecuteAsync(eventId, participantId, cancellationToken);
             
            return Ok();
        }
    }
}
