using DescontosEmPortugal.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DescontosEmPortugal.Web.ViewModel
{
	public class ChartJsViewModel
	{
		public Product Product { get; set; }
		public string ChartJson { get; set; }
		public ChartJS Chart { get; set; }
	}
}