using DevCreedSendEmail.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.IO;

using System.Threading.Tasks;

namespace DevCreedSendEmail.Services
{
    public class MailService : IMailService
    {
        private readonly Mailsettings _mailsettings;
        public MailService(Mailsettings mailsettings)
        {
            _mailsettings = mailsettings;
        }
        public async Task SendEmail(string mailto, string subject, string body, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailsettings.Email),
                Subject = subject

            };
            email.To.Add(MailboxAddress.Parse(mailto));
            var builder = new BodyBuilder();
            if (attachments != null)
            {
                byte[] filebytes;
                foreach(var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        filebytes = ms.ToArray();
                        builder.Attachments.Add(file.FileName, filebytes, MimeKit.ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailsettings.DisplayName, _mailsettings.Email));
            using var smtp = new SmtpClient();
            smtp.Connect(_mailsettings.Host, _mailsettings.port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailsettings.Email, _mailsettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
