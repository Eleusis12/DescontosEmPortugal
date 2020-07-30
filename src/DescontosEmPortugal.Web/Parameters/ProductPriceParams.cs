using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebKuantoKustaScrapper.Enums;

namespace WebKuantoKustaScrapper.ViewModel
{
	public class ProductPriceParams
	{
		[BindProperty(Name = "categoria", SupportsGet = true)]
		public int? Categoria { get; set; }

		[BindProperty(Name = "q", SupportsGet = true)]
		public string Q { get; set; }

		[BindProperty(Name = "sortBy", SupportsGet = true)]
		public EnumSortBy? SortBy { get; set; }

		[BindProperty(Name = "lowestPriceEver", SupportsGet = true)]
		public bool? LowestPriceEver { get; set; }

		[BindProperty(Name = "selectMinRange", SupportsGet = true)]
		public int? SelectMinRange { get; set; }

		[BindProperty(Name = "selectMaxRange", SupportsGet = true)]
		public int? SelectMaxRange { get; set; }
	}
}