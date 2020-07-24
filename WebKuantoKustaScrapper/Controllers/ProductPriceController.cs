using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKuantoKustaScrapper.Helpers;
using WebKuantoKustaScrapper.Models;

namespace WebKuantoKustaScrapper.Controllers
{
	public class ProductPriceController : Controller
	{
		private readonly ProdutosContext _context;

		public string NameSort { get; set; }
		public string DateSort { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		public IList<Product> Products { get; set; }

		public ProductPriceController(ProdutosContext context)
		{
			_context = context;
		}

		// GET: ProductPriceController
		public async Task<ActionResult> IndexAsync(string orderBy, string currentFilter, string q, int? pageNumber)
		{
			var Categories = _context.Categoria.ToList();
			var produtos = _context.Product.Include(x => x.IdCategoriaNavigation).Include(x => x.IdPrecoNavigation);

			// Obtém as Categorias das pesquisas efetuadas anteriormente
			var categoryQuery = from m in _context.Product
								orderby m.IdCategoriaNavigation.Nome
								select m.IdCategoriaNavigation.Nome;

			// Obtém todos os produtos disponíveis na base de dados

			// De momento Queremos apenas os produtos que se encontram com o preço a descer (possível minimo histórico)
			var LowestEverProducts = produtos.Where(x => x.IdPrecoNavigation.PrecoMaisBaixoFlag == true && x.IdPrecoNavigation.NewProduct == false);

			ViewData["PopularitySortParm"] = String.IsNullOrEmpty(orderBy) ? "pop_desc" : "";
			ViewData["NameSortParm"] = orderBy == "Name" ? "name_desc" : "Name";
			ViewData["DateSortParm"] = orderBy == "Date" ? "date_desc" : "Date";
			ViewData["CurrentSort"] = orderBy;

			if (q != null)
			{
				pageNumber = 1;
			}
			else
			{
				q = currentFilter;
			}

			if (!String.IsNullOrEmpty(q))
			{
				LowestEverProducts = LowestEverProducts.Where(s => s.Nome.Contains(q)
									   || s.Marca.Contains(q)
									   || s.IdCategoriaNavigation.Nome.Contains(q));
			}

			switch (orderBy)
			{
				case "pop_desc":
					LowestEverProducts = LowestEverProducts.OrderByDescending(s => s.Popularidade);
					break;

				case "name_desc":
					LowestEverProducts = LowestEverProducts.OrderByDescending(s => s.Nome);
					break;

				case "Name":
					LowestEverProducts = LowestEverProducts.OrderBy(s => s.Nome);
					break;

				case "Date":
					LowestEverProducts = LowestEverProducts.OrderBy(s => s.IdPrecoNavigation.DataPrecoMaisBaixo);
					break;

				case "date_desc":
					LowestEverProducts = LowestEverProducts.OrderByDescending(s => s.IdPrecoNavigation.DataPrecoMaisBaixo);
					break;

				default:
					LowestEverProducts = LowestEverProducts.OrderBy(s => s.Popularidade);
					break;
			}

			int pageSize = 20;
			return View(await PaginatedList<Product>.CreateAsync(LowestEverProducts.AsNoTracking(), pageNumber ?? 1, pageSize));

			return new EmptyResult();
		}
	}
}