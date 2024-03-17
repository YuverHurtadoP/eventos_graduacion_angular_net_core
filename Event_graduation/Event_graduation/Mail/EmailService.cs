using System.Net.Mail;
using System.Net;

namespace Event_graduation.Mail
{
    public class EmailService
    {

        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailService(string smtpServer, int port, string username, string password)
        {
            _smtpServer = smtpServer;
            _port = port;
            _username = username;
            _password = password;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _port))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_username, _password);

                    var message = new MailMessage(_username, toEmail, subject, body);
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
