using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescontosEmPortugal.Web.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DescontosEmPortugal.Web.BaseController
{
	public class BaseController : Controller
	{
		public void Notification(string message, NotificationType notificationType)
		{
			TempData["icon"] = notificationType.ToString();
			TempData["message"] = message;
		}

		public void Alert(string message, NotificationType notificationType)
		{
		}
	}
}