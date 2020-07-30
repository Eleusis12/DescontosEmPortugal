using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebKuantoKustaScrapper.Helpers;
using WebKuantoKustaScrapper.Models;
using WebKuantoKustaScrapper.ViewModel;

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
		public async Task<ActionResult> IndexAsync(int IdCategory, string orderBy, string currentFilter, string q, int? pageNumber)

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
			ViewData["Categories"] = new SelectList(Categories, "Id", "Nome");

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
			if (IdCategory > 0)
			{
				LowestEverProducts = LowestEverProducts.Where(s => s.IdCategoria == IdCategory);
			}

			LowestEverProducts = orderBy switch
			{
				"pop_desc" => LowestEverProducts.OrderByDescending(s => s.Popularidade),
				"name_desc" => LowestEverProducts.OrderByDescending(s => s.Nome),
				"Name" => LowestEverProducts.OrderBy(s => s.Nome),
				"Date" => LowestEverProducts.OrderBy(s => s.IdPrecoNavigation.DataPrecoMaisBaixo),
				"date_desc" => LowestEverProducts.OrderByDescending(s => s.IdPrecoNavigation.DataPrecoMaisBaixo),
				_ => LowestEverProducts.OrderBy(s => s.Popularidade),
			};
			int pageSize = 20;

			return View(await PaginatedListProducts<Product>.CreateAsync(
				LowestEverProducts.AsNoTracking(), pageNumber ?? 1, pageSize));

			return new EmptyResult();
		}

		public async Task<IActionResult> Details(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Product.Include(x => x.IdCategoriaNavigation).Include(x => x.IdPrecoNavigation).Include(x => x.IdPrecoNavigation.PrecoVariacoes)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (product == null)
			{
				return NotFound();
			}

			ChartJsViewModel chartJS = new ChartJsViewModel();
			ChartJS chart = new ChartJS();
			Data data = new Data();
			Dataset dataset = new Dataset();
			Options options = new Options();

			chartJS.Product = product;

			// Queremos um gráfico linear

			List<Tuple<string, float>> dataList = new List<Tuple<string, float>>();
			foreach (var item in product.IdPrecoNavigation.PrecoVariacoes)
			{
				// Queremos as datas assim com as mudanças  de preço
				dataList.Add(Tuple.Create(item.DataAlteracao.ToString(), item.Preco));
			}

			dataList = dataList.OrderBy(o => o.Item1).ToList();

			String[] labelsArray = dataList.Select(o => o.Item1).ToArray();
			float[] dataArray = dataList.Select(o => o.Item2).ToArray();

			data.labels = labelsArray;

			//chart.data.datasets.;

			dataset.label = product.Nome;
			dataset.data = dataArray;
			dataset.backgroundColor = new string[] { "rgba(105, 0, 132, .2)" };
			dataset.borderColor = new string[] { "rgba(200, 99, 132, .7)" };
			dataset.borderWidth = 2;

			data.datasets = new Dataset[] { dataset };

			chart.type = "line";
			options.responsive = true;
			chart.options = options;
			chart.data = data;

			chartJS.Chart = chart;

			return View(chartJS);
		}
	}
}