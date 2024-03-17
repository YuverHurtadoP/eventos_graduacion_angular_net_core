using Event_graduation.Aplication.UseCase.InterfaceUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event_graduation.Dominio.Repository;
using Event_graduation.Models;
using Event_graduation.Aplication.DTOS.Response;
using Event_graduation.Aplication.mapper;
using Event_graduation.Aplication.DTOS.Request;

namespace Event_graduation.Aplication.UseCase.Impl
{
    public class EventUseCaseImpl : IEventUseCase
    {
        private readonly IGenericRepository<Event> _genericRepository;

        public EventUseCaseImpl(IGenericRepository<Event> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void  AddEventAsync(EventRequestDto request)
        {
            Event info = EventMapper.MapToEntity(request);
            info.UpdateDate = null;
            info.CreationDate = DateTime.Now; 
             _genericRepository.AddEvent(info);
        }

        public async Task DeleteEventAsync(int id)
        {
            _genericRepository.DeleteEvent(id);
        }

        public async Task<IEnumerable<EventResponseDto>> GetAllEventAsync()
        {
            IEnumerable<Event> eventData = _genericRepository.ListEvent();
            IEnumerable<EventResponseDto> eventDto = EventMapper.MapListToDto(eventData);
            return eventDto;
        }

        public Task<EventResponseDto> GetEventByIdAsync(int id)
        {
           var eventData = _genericRepository.GetEventById(id);
            EventResponseDto eventDto = EventMapper.MapToDto(eventData);
            return Task.FromResult(eventDto);
        }

        public async Task UpdateEventAsync(int id, EventRequestDto request)
        {
            // Obtener el evento actual
            var existingEvent =  _genericRepository.GetEventById(id);
            if (existingEvent == null)
            {
                // Manejar el caso en que el evento no exista
                return;
            }

            // Actualizar solo los campos necesarios
            existingEvent.InstitutionName = request.InstitutionName;
            existingEvent.InstitutionAddress = request.InstitutionAddress;
            existingEvent.NumberOfStudents = request.NumberOfStudents;
            existingEvent.StartTime = request.StartTime;
            existingEvent.ServicePrice = request.ServicePrice;
            existingEvent.UpdateDate = DateTime.Now;

            // Llamar al método UpdateEvent pasando solo las propiedades que se deben actualizar
            _genericRepository.UpdateEvent(existingEvent,
                e => e.InstitutionName,
                e => e.InstitutionAddress,
                e => e.NumberOfStudents,
                e => e.StartTime,
                e => e.ServicePrice,
                e => e.UpdateDate
            );
        }

    }
}
