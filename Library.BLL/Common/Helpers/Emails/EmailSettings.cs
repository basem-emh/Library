using Library.BLL.Common.Helpers.Emails;
using System.Net;
using System.Net.Mail;

namespace Library.BLL.Common.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("basememad1907@gmail.com", "hqevxxprlmuoemym");
            client.Send("basememad1907@gmail.com", email.To, email.Subject, email.Body);
        }

    }
}
