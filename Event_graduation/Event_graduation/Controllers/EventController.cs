using Event_graduation.Aplication.DTOS.Request;
using Event_graduation.Aplication.DTOS.Response;
using Event_graduation.Aplication.UseCase.InterfaceUseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event_graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventUseCase _eventUseCase;
        private readonly IEmailService _emailService;

        public EventController(IEventUseCase eventUseCase, IEmailService emailService)
        {
            _eventUseCase = eventUseCase;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventAsync()
        {
            var eventDtos = await _eventUseCase.GetAllEventAsync();
            return Ok(eventDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventByIdAsync(int id)
        {
            var eventDto = await _eventUseCase.GetEventByIdAsync(id);
            if (eventDto == null)
            {
                return NotFound();
            }
            return Ok(eventDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddEventAsync([FromBody] EventRequestDto request)
        {
            try
            {
                 _eventUseCase.AddEventAsync(request);
                await _emailService.SendEmailAsync( request.Email.Trim(),
                    "CREACIÓN DE EVENTO",
                    $"Le informamos que su evento ha sido creado con éxito, con los siguientes datos.  Nombre Institución: {request.InstitutionName}, Dirección: {request.InstitutionAddress}, Número de estudiantes: {request.NumberOfStudents}, Hora de inicio: {request.StartTime}, Precio del servicio: {request.ServicePrice}"
                    );

                return Ok(); // Devuelve un 200 OK si la operación fue exitosa
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Devuelve un 500 Internal Server Error si hay una excepción
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            await _eventUseCase.DeleteEventAsync(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventAsync(int id, [FromBody] EventRequestDto request)
        {
           
            try
            {
                await _eventUseCase.UpdateEventAsync(id, request);
                await _emailService.SendEmailAsync(request.Email.Trim(),
                  "ACTUALIZACIÓN DE EVENTO",
                  $"Le informamos que su evento ha sido ACTUALIZADO con éxito, con los siguientes datos.  Nombre Institución: {request.InstitutionName}, Dirección: {request.InstitutionAddress}, Número de estudiantes: {request.NumberOfStudents}, Hora de inicio: {request.StartTime}, Precio del servicio: {request.ServicePrice}"
                  );
                return Ok(); // Devuelve un 200 OK si la operación fue exitosa
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Devuelve un 500 Internal Server Error si hay una excepción
            }
        }

    }
}
