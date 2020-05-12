using System.Threading.Tasks;

namespace Rentacar.Areas.Admin.Service
{
    public  interface IEmailSender
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
