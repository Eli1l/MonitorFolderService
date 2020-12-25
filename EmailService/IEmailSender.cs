using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    internal interface IEmailSender
    {
        Task SendEmail(Message message);
    }
}