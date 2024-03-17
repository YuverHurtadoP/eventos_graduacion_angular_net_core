using Event_graduation.Aplication.DTOS.Request;
using Event_graduation.Aplication.DTOS.Response;
using Event_graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_graduation.Aplication.mapper
{
    public static class EventMapper
    {
        public static EventResponseDto MapToDto(Event @event)
        {
            if(@event != null){
                var dto = new EventResponseDto
                {
                    Id = @event.Id,
                    InstitutionName = @event.InstitutionName,
                    InstitutionAddress = @event.InstitutionAddress,
                    NumberOfStudents = @event.NumberOfStudents,
                    StartTime = @event.StartTime,
                    ServicePrice = @event.ServicePrice,
                    Email = @event.Email,
                    CreationDate = @event.CreationDate,
                    UpdateDate = @event.UpdateDate
                };
                return dto;
            }
            else
            {
                return null;
            }
          
        }


        public static IEnumerable<EventResponseDto> MapListToDto(IEnumerable<Event> events)
        {
            var eventDtos = new List<EventResponseDto>();

            foreach (var @event in events)
            {
                var eventDto = new EventResponseDto
                {
                    Id = @event.Id,
                    InstitutionName = @event.InstitutionName,
                    InstitutionAddress = @event.InstitutionAddress,
                    NumberOfStudents = @event.NumberOfStudents,
                    StartTime = @event.StartTime,
                    ServicePrice = @event.ServicePrice,
                    Email = @event.Email,
                    CreationDate = @event.CreationDate,
                    UpdateDate = @event.UpdateDate
                };

                eventDtos.Add(eventDto);
            }

            return eventDtos;
        }

        public static Event MapToEntity(EventRequestDto eventDto)
        {
            return new Event
            {
              
                InstitutionName = eventDto.InstitutionName,
                InstitutionAddress = eventDto.InstitutionAddress,
                NumberOfStudents = eventDto.NumberOfStudents,
                StartTime = eventDto.StartTime,
                ServicePrice = eventDto.ServicePrice,
                Email = eventDto.Email,
               
            };
        }
    }
}
