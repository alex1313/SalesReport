namespace SalesReport.Services.Implementation
{
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;

    public class EmailSender : IEmailSender
    {
        private readonly ContentType _attachmentContentType = new ContentType(MediaTypeNames.Text.Plain);

        public void Send(string address, string subject, string body, MemoryStream attachmentStream)
        {
            //TODO: getting from configuration file
            var fromAddress = new MailAddress("from@gmail.com", "From Name");
            const string fromPassword = "fromPassword";

            var toAddress = new MailAddress(address);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var attachment = new Attachment(attachmentStream, _attachmentContentType);

            using (var mail = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                Attachments = { attachment }
            })
            
            smtp.Send(mail);
        }
    }
}