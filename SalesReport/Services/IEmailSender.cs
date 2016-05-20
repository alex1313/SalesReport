namespace SalesReport.Services
{
    using System.IO;

    public interface IEmailSender
    {
        void Send(string address, string subject, string body, MemoryStream attachmentStream);
    }
}