using Events.Application.DTO.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Interfaces.UseCase.Event
{
    public interface IGetAllEventUseCase
    {
        Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
