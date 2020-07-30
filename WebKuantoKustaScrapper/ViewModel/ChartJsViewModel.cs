using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebKuantoKustaScrapper.Models;

namespace WebKuantoKustaScrapper.ViewModel
{
	public class ChartJsViewModel
	{
		public Product Product { get; set; }
		public string ChartJson { get; set; }
		public ChartJS Chart { get; set; }
	}
}