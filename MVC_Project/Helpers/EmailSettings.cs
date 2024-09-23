using MVC_Project_DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_Project_PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com",587);
			//client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mi4654110@gmail.com", "MM050699mm##");
			client.Send("mi4654110@gmail.com", email.Receipents, email.Subject, email.Body);
		}
	}
}
