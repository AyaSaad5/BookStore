using System.Net.Mail;
using System.Net;
using BookStore.DAL.Models;

namespace BookStore.PL.Helpers
{
	public class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("ayasa3d5@gmail.com", "ayqtosninebewhqw");
			client.Send("ayasa3d5@gmail.com", email.To, email.Subject, email.Body);

		}
	}
}
