using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DescontosEmPortugal.Web.ViewModel;
using DescontosEmPortugal.Web.Enums;
using Microsoft.Extensions.Logging;

namespace DescontosEmPortugal.Web.Controllers
{
	public class ContactUsController : DescontosEmPortugal.Web.BaseController.BaseController
	{
		private readonly ILogger _logger;

		public ContactUsController(ILogger<ContactUsController> logger)
		{
			_logger = logger;
		}

		// GET: ContactUs
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Contact(ContactViewModel C)
		{
			string Name = C.FirstName;
			string Email = C.Email;
			string Message = C.Message;
			string Country = C.Country;

			if (ModelState.IsValid)
			{
				var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
				var message = new MailMessage();
				message.To.Add(new MailAddress(Email));  // replace with valid value
				message.From = new MailAddress("WebScrapperKK@hotmail.com");  // replace with valid value
				message.Subject = "Your email subject";
				message.Body = string.Format(body, Name, Email, Message);
				message.IsBodyHtml = true;
				using (var smtp = new SmtpClient())
				{
					var credential = new NetworkCredential
					{
						UserName = "WebScrapperKK@hotmail.com",  // replace with valid value
						Password = "xZm4xJaGUA4e5pX"  // replace with valid value
					};
					smtp.UseDefaultCredentials = false;
					smtp.Credentials = credential;
					smtp.Host = "smtp.office365.com";//address webmail
					smtp.Port = 587;
					smtp.EnableSsl = true;

					try
					{
						await smtp.SendMailAsync(message);
						Notification("Mensagem enviada com sucesso", NotificationType.success);
					}
					catch (SmtpFailedRecipientsException recipientsException)
					{
						_logger.LogError($"Failed recipients: {string.Join(", ", recipientsException.InnerExceptions.Select(fr => fr.FailedRecipient))}");
						Notification("Não foi possível enviar a mensagem", NotificationType.error);
					}
					catch (SmtpFailedRecipientException recipientException)
					{
						_logger.LogError($"Failed recipient: {recipientException.FailedRecipient}");
						Notification("Não foi possível enviar a mensagem", NotificationType.error);
					}
					catch (SmtpException smtpException)
					{
						_logger.LogError(smtpException.Message);
						Notification("Não foi possível enviar a mensagem", NotificationType.error);
					}

					return RedirectToAction(nameof(Index));
				}
			}
			return View();
		}
	}
}