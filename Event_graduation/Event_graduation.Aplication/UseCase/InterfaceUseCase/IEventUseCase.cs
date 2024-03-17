using Event_graduation.Aplication.DTOS.Request;
using Event_graduation.Aplication.DTOS.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_graduation.Aplication.UseCase.InterfaceUseCase
{
    public interface IEventUseCase
    {

        Task<EventResponseDto> GetEventByIdAsync(int id);
        Task<IEnumerable<EventResponseDto>> GetAllEventAsync();
        void AddEventAsync(EventRequestDto request);
        Task UpdateEventAsync(int id, EventRequestDto request);
        Task DeleteEventAsync(int id);
    }
}
