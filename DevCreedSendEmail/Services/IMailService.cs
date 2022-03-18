using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCreedSendEmail.Services
{
  public  interface IMailService
    {
        Task SendEmail(string mailto,string subject,string body,IList<IFormFile>attachments=null);
    }
}
