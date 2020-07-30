using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Database;
using WebScrapper.Models;

namespace WebScrapper.AcessDataBase
{
	public static class AcessDataBase
	{
		public static DataTable GetAllUrl()
		{
			return DataBaseAccessProcessing.GetAllUrl();
		}

		public static bool InsertWebsiteDetailsIntoDataBase(WebsiteDetails websiteDetails)
		{
			return DataBaseAccessProcessing.InsertWebsiteDetailsIntoDataBase(websiteDetails);
		}

		public static bool InsertProductDetailsIntoDataBase(ProductDetails productDetails, int searchId)
		{
			return DataBaseAccessProcessing.InsertProductDetailsIntoDataBase(productDetails, DataBaseAccessProcessing.GetCategoryID(searchId), searchId);
		}
	}
}