using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DescontosEmPortugal.Web.ViewModel;

namespace DescontosEmPortugal.Web.Controllers
{
	public class ContactUsController : Controller
	{
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
					await smtp.SendMailAsync(message);
					return RedirectToAction("Index");
				}
			}
			return View();
		}
	}
}