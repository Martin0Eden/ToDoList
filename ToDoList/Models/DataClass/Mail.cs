using System.Net;
using System.Net.Mail;

namespace ToDoList.Models
{
    public class Mail
    {
        public static MailMessage CreateMessage(string emailto,string subject,string body)
        {
           MailAddress from = new MailAddress("todolistfromdymitrash@gmail.com", "ToDoList");
           MailAddress to = new MailAddress(emailto);
            MailMessage message = new MailMessage(from,to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            return message;
        }

        public static void SendMail(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("todolistfromdymitrash@gmail.com", "pjgweqemqstusubs");

            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
