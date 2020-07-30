using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Models
{
	public class ProductDetails
	{
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string Brand { get; set; }
		public string WebsiteUrl { get; set; }
		public string ImageLink { get; set; }
		public double CurrentPrice { get; set; }

		public int Popularity { get; set; }
	}
}