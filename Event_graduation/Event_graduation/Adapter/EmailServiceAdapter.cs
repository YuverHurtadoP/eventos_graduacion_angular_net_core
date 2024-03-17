using Event_graduation.Aplication.UseCase.InterfaceUseCase;
using static System.Net.Mime.MediaTypeNames;

namespace Event_graduation.Adapter
{
    public class EmailServiceAdapter : IEmailService
    {
        private readonly IEmailService _emailService;

        public EmailServiceAdapter(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Llamar al servicio de correo electrónico de la capa de aplicación
            await _emailService.SendEmailAsync(toEmail, subject, body);
        }
    }
}
