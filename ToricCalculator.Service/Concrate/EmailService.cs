using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using ToricCalculator.Service.Abstract;
using ToricCalculator.Service.Model;
using MailKit.Net.Smtp;
using ToricCalculator.Models;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace ToricCalculator.Service.Concrate
{
	public class EmailService : IEmailService
	{
		private readonly EmailConfiguration _emailConfiguration;
		private readonly ILogger<EmailService> _logger;

		public EmailService(EmailConfiguration emailConfiguration, ILogger<EmailService> logger)
		{
			_emailConfiguration = emailConfiguration;
			_logger = logger;
		}
		public void SendEmail(Message message)
		{
			var emailMessage = CreateEmailMessage(message);
			Send(emailMessage);
		}
		public async Task SenEmail2(Message message)
		{
			var emailMessage = CreateEmailMessage(message);
			 await Send2(emailMessage);
		}
		public void SendEmail3(Message message)
		{
			var emailMessage = CreateEmail(message);
			Send(emailMessage);
		}
		private MimeMessage CreateEmail(Message message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;
			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = string.Format
				("<div style='color:blue;'><span style='color:rgb(41, 105, 176);'><strong>Surgeon Name</strong><strong>:</strong></span><span style='color:rgb(40, 50, 78);'><strong>&nbsp;</strong>{0}</span></div><div style='color:blue;'><strong><span style='color: rgb(41, 105, 176);'> Reference Name:</span></strong><span style='color:rgb(40, 50, 78);'>&nbsp;", message.Content)
			};
			//if (message.Attachments != null && message.Attachments.Any())
			//{
			//	byte[] fileBytes;
			//	foreach (var attachment in message.Attachments)
			//	{
			//		using (var ms = new MemoryStream())
			//		{
			//			attachment.CopyTo(ms);
			//			fileBytes = ms.ToArray();
			//		}
			//		bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
			//	}
			//}
			emailMessage.Body = bodyBuilder.ToMessageBody();
			return emailMessage;
		}
		private MimeMessage CreateEmailMessage(Message message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;
			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = string.Format
				("<div style='color:blue;'><span style='color:rgb(41, 105, 176);'><strong>Surgeon Name</strong><strong>:</strong></span><span style='color:rgb(40, 50, 78);'><strong>&nbsp;</strong>{0}</span></div><div style='color:blue;'><strong><span style='color: rgb(41, 105, 176);'> Reference Name:</span></strong><span style='color:rgb(40, 50, 78);'>&nbsp;{1}</span></div><div style='color:blue;'><strong><span style= 'color: rgb(41, 105, 176);'>Date:</span></strong><span style='color: rgb(40, 50, 78);'>&nbsp;{2}</span ></ div><div style='color:blue;'><span style='color:rgb(41, 105, 176);'><strong>Eye Selection</strong><strong>:</strong></span><span style='color:rgb(40, 50, 78);'><strong>&nbsp;</strong>{3}</span></div><div style='color:blue;'><strong><span style='color: rgb(41, 105, 176);'> IOL:</span></strong><span style='color:rgb(40, 50, 78);'>&nbsp;{4}</span></div><div style='color:blue;'><strong><span style= 'color: rgb(41, 105, 176);'>Alignment:</span></strong><span style='color: rgb(40, 50, 78);'>&nbsp;{5}</span ></ div><div style='color:blue;'><strong><span style= 'color: rgb(41, 105, 176);'>Residual Astigmatism:</span></strong><span style='color: rgb(40, 50, 78);'>&nbsp;{6}</span ></ div>", message.SurgeonName, message.ReferenceName, DateTime.Now, message.EyeSelection, message.IOL, message.Alignment, message.ResidualAstigmatism)
			};
			var fileName = "PDF/" + message.GuidId.ToString() + ".pdf";
			bodyBuilder.Attachments.Add(fileName);
			//if (message.Attachments != null && message.Attachments.Any())
			//{
			//	byte[] fileBytes;
			//	foreach (var attachment in message.Attachments)
			//	{
			//		using (var ms = new MemoryStream())
			//		{
			//			attachment.CopyTo(ms);
			//			fileBytes = ms.ToArray();
			//		}
			//		bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
			//	}
			//}
			emailMessage.Body = bodyBuilder.ToMessageBody();
			return emailMessage;
		}
		private void Send(MimeMessage mailMessage)
		{
			using (SmtpClient client = new SmtpClient())
			{
				try
				{
					client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
					client.Send(mailMessage);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
					//TODO:error message logla
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}
		private async Task Send2(MimeMessage mailMessage)
		{
			using (SmtpClient client = new SmtpClient())
			{
				try
				{
					client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
					client.Send(mailMessage);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
					//TODO:error message logla
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}
	}

}
