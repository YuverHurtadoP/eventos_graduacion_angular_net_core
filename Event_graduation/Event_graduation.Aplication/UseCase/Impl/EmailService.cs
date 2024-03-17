using Event_graduation.Aplication.DTOS.Request;
using Event_graduation.Aplication.UseCase.InterfaceUseCase;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Event_graduation.Aplication.UseCase.Impl
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
     

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                    var message = new MailMessage(_emailSettings.Username, toEmail, subject, body);
                    message.IsBodyHtml = true;

                    await client.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                // Manejar el error adecuadamente
                Console.WriteLine($"Error al enviar correo electrónico: {ex.Message}");
                throw;
            }
        }
    }
}
