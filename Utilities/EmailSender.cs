using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Utilities
{
	public class EmailSender : IEmailSender
	{
		public EmailSender() { }
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			return SendEmailAsync(email, subject, htmlMessage, null);
		}

		
		public Task SendEmailAsync(string email, string subject, string htmlMessage, byte[]? image = null)
		{
			string? sender = "QuiNHCE171689@fpt.edu.vn";
			string? password = "htka xpyy dxsb ckqr";
			MailMessage mail = new MailMessage
			{
				From = new MailAddress(sender),
				Subject = subject,
				Body = htmlMessage,
				IsBodyHtml = true,
			};

			mail.To.Add(email);
			if (image != null)
			{
				Attachment attachment = new Attachment(new MemoryStream(image), "Attachment");
				mail.Attachments.Add(attachment);
			}
			SmtpClient client = new()
			{
				Port = 587,
				Host = "smtp.gmail.com",
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(sender, password)
			};

			return client.SendMailAsync(mail);
		}
	}
}
