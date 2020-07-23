using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebKuantoKustaScrapper.Models;

namespace WebKuantoKustaScrapper.Controllers
{
	public class ProductPriceController : Controller
	{
		private readonly ProdutosContext _context;

		public ProductPriceController(ProdutosContext context)
		{
			_context = context;
		}

		// GET: ProductPriceController
		public ActionResult Index()
		{
			return View(_context.Product.ToList());
		}

		// GET: ProductPriceController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: ProductPriceController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ProductPriceController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ProductPriceController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: ProductPriceController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ProductPriceController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: ProductPriceController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}