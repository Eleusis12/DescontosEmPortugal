using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebKuantoKustaScrapper.Models;
using WebKuantoKustaScrapper.ViewModel;

namespace WebKuantoKustaScrapper.ViewComponents
{
	[ViewComponent(Name = "chartjs")]
	public class ChartJsViewComponent : ViewComponent
	{
		//public IViewComponentResult Invoke(string chartData)
		//{
		//	// Ref: https://www.chartjs.org/docs/latest/

		//	ChartJS chart = JsonConvert.DeserializeObject<ChartJS>(chartData);

		//	var chartModel = new ChartJsViewModel
		//	{
		//		Chart = chart,
		//		ChartJson = JsonConvert.SerializeObject(chart, new JsonSerializerSettings
		//		{
		//			NullValueHandling = NullValueHandling.Ignore
		//		})
		//	};

		//	return View(chartModel);
		//}

		public IViewComponentResult Invoke(ChartJS chart)
		{
			// Ref: https://www.chartjs.org/docs/latest/

			var chartModel = new ChartJsViewModel
			{
				Chart = chart,
				ChartJson = JsonConvert.SerializeObject(chart, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				})
			};

			return View(chartModel);
		}
	}
}