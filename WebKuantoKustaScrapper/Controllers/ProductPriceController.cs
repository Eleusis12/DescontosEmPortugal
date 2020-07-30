using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebKuantoKustaScrapper.Enums;
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
		public async Task<ActionResult> IndexAsync(ProductPriceParams productPriceParams, int? page)

		{
			// Definimos que queremos apresentar 20 produtos por página (no máximo)
			int pageSize = 20;
			//ViewData["CurrentFilterChoice"] = string.Empty;

			// Filtering
			TempData["CurrentSearch"] = productPriceParams.Q;
			TempData["CurrentCategory"] = productPriceParams.Categoria;

			// Sorting
			ViewData["CurrentSort"] = productPriceParams.SortBy;
			TempData["PopularitySortParm"] = productPriceParams.SortBy == null || productPriceParams.SortBy == EnumSortBy.PopularityDesc ? EnumSortBy.PopularityAsc : EnumSortBy.PopularityDesc;
			TempData["NameSortParm"] = productPriceParams.SortBy == EnumSortBy.NameDesc ? EnumSortBy.NameAsc : EnumSortBy.NameDesc;
			TempData["DateSortParm"] = productPriceParams.SortBy == EnumSortBy.DateAsc ? EnumSortBy.DateDesc : EnumSortBy.DateAsc;
			TempData["PriceSortParm"] = productPriceParams.SortBy == EnumSortBy.PriceAsc ? EnumSortBy.PriceDesc : EnumSortBy.PriceAsc;

			var categorias = LoadCategorySelect(productPriceParams);

			//if (productPriceParams.currentFilter != null)
			//{
			//	if (int.TryParse(productPriceParams.currentFilter, out int n) == true && categorias.Any(x => x.Id == n) && ViewData["CurrentFilterChoice"] as int? == 1)
			//	{
			//		productPriceParams.categoria = Convert.ToInt32(productPriceParams.currentFilter);
			//	}
			//	else
			//	{
			//		productPriceParams.q = productPriceParams.currentFilter;
			//	}
			//}

			// Guardar as escolhas efetuadas pelo utilizador para verificar numa próxima página.
			//TempData["CurrentFilter"] = productPriceParams.q ?? productPriceParams.categoria.ToString();

			// Obtém todos os produtos disponíveis na base de dados
			var produtos = _context.Product.Include(x => x.IdCategoriaNavigation).Include(x => x.IdPrecoNavigation).AsQueryable();

			//ViewData["PopularitySortParm"] = String.IsNullOrEmpty(productPriceParams.sortBy) ? "pop_desc" : "";
			//ViewData["NameSortParm"] = productPriceParams.sortBy == "Name" ? "name_desc" : "Name";
			//ViewData["DateSortParm"] = productPriceParams.sortBy == "Date" ? "date_desc" : "Date";

			// Se todos os argumentos são nulos, significa que o utilizador pretende ver todos os produtos.
			if (IsAllNullOrEmpty(productPriceParams))
			{
				// Ordena de acordo com a popularidade dos produtos
				produtos = produtos.OrderBy(s => s.Popularidade);

				// Retorna todos os produtos
				return View(await PaginatedListProducts<Product>.CreateAsync(
				produtos.AsNoTracking(), page ?? 1, pageSize));
			}

			// Se o utilizador pretende ver os produtos com preço mínimo histórico
			if (productPriceParams.LowestPriceEver == true)
			{
				// Produtos com os preços mais baixos
				produtos = produtos.Where(x => x.IdPrecoNavigation.PrecoMaisBaixoFlag == true && x.IdPrecoNavigation.NewProduct == false);
			}
			if (productPriceParams.SelectMinRange != null)
			{
				produtos = produtos.Where(x => x.IdPrecoNavigation.PrecoAtual >= productPriceParams.SelectMinRange);
			}

			if (productPriceParams.SelectMaxRange != null)
			{
				produtos = produtos.Where(x => x.IdPrecoNavigation.PrecoAtual <= productPriceParams.SelectMinRange);
			}

			if (!String.IsNullOrEmpty(productPriceParams.Q))
			{
				produtos = produtos.Where(s => s.Nome.Contains(productPriceParams.Q)
									   || s.Marca.Contains(productPriceParams.Q)
									   || s.IdCategoriaNavigation.Nome.Contains(productPriceParams.Q));
			}
			else if (productPriceParams.Categoria > 0)
			{
				produtos = produtos.Where(s => s.IdCategoria == productPriceParams.Categoria);
			}

			//if (productPriceParams.page >= 1)
			//{
			//	string filterChoice = ViewData["CurrentFilterChoice"] as string;

			//	if (filterChoice == "Categoria")
			//	{
			//	}
			//	else if (filterChoice == "Search")
			//	{
			//	}
			//	else
			//	{
			//		throw new ArgumentOutOfRangeException();
			//	}
			//}

			produtos = productPriceParams.SortBy switch
			{
				EnumSortBy.PopularityAsc => produtos.OrderBy(s => s.Popularidade),
				EnumSortBy.PopularityDesc => produtos.OrderByDescending(s => s.Popularidade),
				EnumSortBy.NameAsc => produtos.OrderBy(s => s.Nome),
				EnumSortBy.NameDesc => produtos.OrderByDescending(s => s.Nome),
				EnumSortBy.DateAsc => produtos.OrderBy(s => s.IdPrecoNavigation.DataPrecoMaisBaixo),
				EnumSortBy.DateDesc => produtos.OrderByDescending(s => s.IdPrecoNavigation.DataPrecoMaisBaixo),
				EnumSortBy.PriceAsc => produtos.OrderBy(s => s.IdPrecoNavigation.PrecoAtual),
				EnumSortBy.PriceDesc => produtos.OrderByDescending(s => s.IdPrecoNavigation.PrecoAtual),
				EnumSortBy.PercentageAsc => throw new NotImplementedException(),
				EnumSortBy.PercentageDesc => throw new NotImplementedException(),
				EnumSortBy.DropAsc => throw new NotImplementedException(),
				EnumSortBy.DropDesc => throw new NotImplementedException(),
				EnumSortBy.NewestAsc => throw new NotImplementedException(),
				EnumSortBy.NewestDesc => throw new NotImplementedException(),
				_ => produtos.OrderBy(s => s.Popularidade)
			};

			// Retorna para o utilizador
			return View(await PaginatedListProducts<Product>.CreateAsync(
				produtos.AsNoTracking(), page ?? 1, pageSize));
		}

		// Carrega o select, o que nos permite, ver quais
		private List<Categoria> LoadCategorySelect(ProductPriceParams productPriceParams)
		{
			var categoryQuery = from m in _context.Product
								orderby m.IdCategoriaNavigation.Nome
								select m.IdCategoriaNavigation.Nome;

			var Categories = _context.Categoria.ToList();

			//TempData["CurrentFilter"] = productPriceParams.q;
			//TempData["Categories"] = productPriceParams.categoria;

			// Obtém as Categorias das pesquisas efetuadas anteriormente
			var selectedCategory = Categories.Where(x => x.Id == productPriceParams.Categoria).FirstOrDefault();
			if (selectedCategory != null)
			{
				ViewData["Categories"] = new SelectList(Categories, "Id", "Nome", selectedCategory.Id);
			}
			else
			{
				ViewData["Categories"] = new SelectList(Categories, "Id", "Nome");
			}

			return Categories;
		}

		// Verifica se todas as propriedades do objecto são nulas

		public static bool IsAllNullOrEmpty(object obj)
		{
			if (Object.ReferenceEquals(obj, null))
				return true;

			return obj.GetType().GetProperties()
				.All(x => IsNullOrEmpty(x.GetValue(obj)));
		}

		private static bool IsNullOrEmpty(object value)
		{
			if (Object.ReferenceEquals(value, null))
				return true;

			var type = value.GetType();
			return type.IsValueType
				&& Object.Equals(value, Activator.CreateInstance(type));
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
			//	{
			//label = product.Nome,
			//data = dataArray,
			//backgroundColor = new string[] { "rgba(105, 0, 132, .2)" },
			//borderColor = new string[] { "rgba(200, 99, 132, .7)" },
			//borderWidth = 2
			//	}
			//};

			chart.type = "line";
			options.responsive = true;
			chart.options = options;
			chart.data = data;

			chartJS.Chart = chart;

			return View(chartJS);
		}
	}
}