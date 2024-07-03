using DAL.Models;
using System.Net;
using System.Net.Mail;

namespace CompanyMVC.Helper
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{// port : server sprate with many ports (sections)
		 // host : the server name
			var Client = new SmtpClient("smtp.gmail.com", 587);//we use non secure port (which haven't cerificate )=> TLS 
			Client.EnableSsl = true;//To make email encrypted 
			Client.Credentials = new NetworkCredential("mr2438844@gmail.com", "nhzbicqjbhorzhev");//=>this responsible for emailname and password of the email server which send the email of user
			Client.Send("mr2438844@gmail.com", email.To, email.Subject, email.Body); //=> this function responsible for send email Take sender email [server email] & reciver email
		}
	}
}
