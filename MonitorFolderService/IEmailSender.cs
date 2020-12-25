using EmailService;

namespace MonitorFolderService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}