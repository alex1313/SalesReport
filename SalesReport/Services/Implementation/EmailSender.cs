namespace SalesReport.Services.Implementation
{
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;

    public class EmailSender : IEmailSender
    {
        private readonly ContentType _attachmentContentType = new ContentType(MediaTypeNames.Text.Plain);

        public void Send(string address, string subject, string body, MemoryStream attachmentStream)
        {
            var senderAddress = new MailAddress(ConfigurationManager.AppSettings["SenderMailAddress"]);
            var senderPassword = ConfigurationManager.AppSettings["SenderMailPassword"];
            var recipientAddress = new MailAddress(address);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderAddress.Address, senderPassword)
            };

            var attachment = new Attachment(attachmentStream, _attachmentContentType)
            {
                Name = "Sales report.xlsx"
            };

            using (var mail = new MailMessage(senderAddress, recipientAddress)
            {
                Subject = subject,
                Body = body,
                Attachments = { attachment }
            })

            smtp.Send(mail);
        }
    }
}