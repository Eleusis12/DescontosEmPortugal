using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Models;

namespace WebScrapper.Helpers
{
	public class Delegates
	{
		public delegate void UrlExtractionHandler(string URL);

		public delegate void WebsiteDetailsExtractionHandler(WebsiteDetails websiteDetails);

		public delegate void NotificationHandler();
	}
}