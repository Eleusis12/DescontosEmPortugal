using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DescontosEmPortugal.Core.Delegates;
using DescontosEmPortugal.Core.Helpers;
using DescontosEmPortugal.Library.Classes;
using DescontosEmPortugal.Library.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DescontosEmPortugal.Core.Scrapper
{
	public class KuantoKustaScrapper
	{
		public int NumberPage { get; set; } = 1;

		public event TaskInProgressHandler Inform;

		public async Task<List<ProductDetails>> ScrapeWebsiteAsync(string siteUrl)
		{
			List<ProductDetails> products = new List<ProductDetails>();

			if (siteUrl.Contains("?sort="))
			{
				Inform?.Invoke($"[{DateTime.Now:HH:mm:ss}] [INFO] A ordenar os produtos do mais popular para menos popular:");
				//Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] A ordenar os produtos do mais popular para menos popular:", Program.M_Options.StyleConsole);
				siteUrl = siteUrl.Substring(0, siteUrl.IndexOf("?"));
			}
			//Use the default configuration for AngleSharp
			var config = Configuration.Default.WithDefaultLoader();
			//Create a new context for evaluating webpages with the given config
			var context = BrowsingContext.New(config);

			//Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Começa", Program.M_Options.StyleConsole);
			Inform?.Invoke("Começa");

			IDocument document = await context.OpenAsync(siteUrl);

			//var butttonToSort = document.QuerySelector(".select-dropdown").ChildNodes.OfType<IHtmlAnchorElement>().FirstOrDefault();
			//string buttonToSortValue = butttonToSort.ChildNodes.OfType<IHtmlSpanElement>().Select(m => m.TextContent).FirstOrDefault();

			// Se não está ordenado do mais popular para o menos popular

			//butttonToSort.DoClick();
			//var sortByPopularity = document.QuerySelector(".select-dropdown").ChildNodes.GetElementsByTagName("dd").FirstOrDefault().GetElementsByTagName("ul").FirstOrDefault().ChildNodes.OfType<IHtmlAnchorElement>().FirstOrDefault();
			//string html = sortByPopularity.InnerHtml;

			// Enquanto podemos navegar entre as páginas

			int counterPopularity = 1;
			while (true)
			{
				//Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Página: {NumberPage}", Program.M_Options.StyleConsole);
				Inform?.Invoke($"Página: {NumberPage}");

				// DIV completo para cada produto
				Inform?.Invoke(document.ToHtml().ToString());
				IHtmlCollection<IElement> pricesListItemsLinq = document.QuerySelectorAll("div.product-item");

				foreach (var div in pricesListItemsLinq)
				{
					ProductDetails product = new ProductDetails();

					var itemName = div.QuerySelector("a.product-item-name");

					// Nome do Produto
					product.ProductName = itemName.GetAttribute("title");

					// Link do produto no Kuanto Kusta
					if (itemName.GetAttribute("href") == "#")
					{
						string onClickAttribute = itemName.GetAttribute("onclick");
						string encodedURL = onClickAttribute.Substring(onClickAttribute.IndexOf(@"'"), onClickAttribute.LastIndexOf(@"'") - onClickAttribute.IndexOf(@"'"));
						string decodedURL = Uri.UnescapeDataString(encodedURL);
						product.WebsiteUrl = decodedURL.Substring(1, decodedURL.IndexOf(@",") - 2);
					}
					else
					{
						product.WebsiteUrl = "https://www.kuantokusta.pt" + itemName.GetAttribute("href");
					}

					// ID do produto (KuantoKusta)
					product.ProductId = itemName.ChildNodes.OfType<AngleSharp.Html.Dom.IHtmlSpanElement>().FirstOrDefault().GetAttribute("data-id");

					// Link da Imagem de Perfil
					product.ImageLink = div.QuerySelector("img.img-responsive").GetAttribute("src");
					if (product.ImageLink.Contains("kuantokusta") == false)
					{
						product.ImageLink = div.QuerySelector("img.img-responsive").GetAttribute("data-src");
					}

					// Marca
					product.Brand = div.QuerySelector("div.product-item-value").ChildNodes.OfType<AngleSharp.Html.Dom.IHtmlAnchorElement>().Select(m => m.TextContent).FirstOrDefault();

					// Node do Preco
					var priceNode = div.QuerySelector("a.product-item-price");

					// Conteúdo to preco para formatar
					var priceTextContent = priceNode.ChildNodes.OfType<AngleSharp.Html.Dom.IHtmlSpanElement>().Select(m => m.TextContent).FirstOrDefault();

					// Preco formatado em string
					string priceString = priceTextContent.Substring(0, priceTextContent.IndexOf("€"));

					// Preco em double
					product.CurrentPrice = ConvertToDouble.StringPriceToDouble(priceString);

					product.Popularity = counterPopularity++;

					products.Add(product);
				}
				//Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Lidos {products.Count} produtos", Program.M_Options.StyleConsole);
				Inform?.Invoke($"Lidos {products.Count} produtos");

				if ((document = await NavigateToNextPage(document)) == null)
				{
					NumberPage = 1;
					return products;
				}

				// Temos todos os produtos da página para formatar para dados funcionais
			}
		}

		public async Task<IDocument> NavigateToNextPage(IDocument document)
		{
			// Obter todos os botões de navegação
			IHtmlCollection<IElement> buttonsToNav = document.QuerySelectorAll("li.pagination-item");

			// Obter o último
			IElement buttonNextPage = buttonsToNav.Last();

			// Obter o elemento ancora dentro do botao de navegação
			IHtmlAnchorElement buttonGoNextPage = buttonNextPage.ChildNodes.OfType<IHtmlAnchorElement>().FirstOrDefault();

			// Vamos tentar retirar o número da página e colocá-la numa varíavel

			try
			{
				string nextWebsiteLinkPage = buttonGoNextPage.GetAttribute("href");

				string toBeSearched = "pag=";

				string numberPageString = nextWebsiteLinkPage.Substring(nextWebsiteLinkPage.IndexOf(toBeSearched) + toBeSearched.Length);

				if (numberPageString != string.Empty)
				{
					if (Convert.ToInt32(numberPageString) > NumberPage)
					{
						NumberPage = Convert.ToInt32(numberPageString);
					}

					// Para evitar um eterno loop.
					else
					{
						return null;
					}
				}
			}
			catch (FormatException ex)
			{
				_ = new LogWriter("format", ex.Message.ToString());
			}

			// Navegar para a página
			return await (buttonGoNextPage?.NavigateAsync());
		}
	}
}