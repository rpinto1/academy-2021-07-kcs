using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.EmailService.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}