using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Colorful;
using CommandLine;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.AcessDataBase;
using WebScrapper.Helpers;
using WebScrapper.Models;

namespace ConsoleWebScrapperKuantoKusta
{
	internal class Program
	{
		public static WebScrapper.Models.Options M_Options { get; private set; }

		private static async Task Main(string[] args)
		{
			StyleSheet styleSheet = new StyleSheet(Color.White);
			string target = @"\[(.*?)\]";
			styleSheet.AddStyle(target, Color.Gray);
			styleSheet.AddStyle(@"\[(INFO)\]", Color.Blue);
			styleSheet.AddStyle(@"\[(WARNING)\]", Color.Yellow);
			styleSheet.AddStyle(@"\[(CRITICAL)\]", Color.Red);

			M_Options = new WebScrapper.Models.Options();
			M_Options.StyleConsole = styleSheet;

			Colorful.Console.WriteAscii("KuantoKusta Scrapper", Color.FromArgb(244, 212, 255));
			Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Bem vindo ao Programa", styleSheet);

			Parser.Default.ParseArguments<Options>(args)
			  .WithParsed(Run)
			  .WithNotParsed(HandleParseError);

			Colorful.Console.ReadKey();

			//}

			WebScrappingPage scrappingPage = new WebScrappingPage();

			Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Vamos proceder ao ínicio da operação de obter os produtos e os respetivos preços", styleSheet);

			DataTable dt = AcessDataBase.GetAllUrl();

			if (dt != null)
			{
				if (dt.Rows.Count == 0)
				{
					Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [WARNING] Não foi possível obter nenhum link", styleSheet);
					Colorful.Console.WriteStyled($"[{DateTime.Now:HH:mm:ss}] Pretende adicionar algum link através da consola? (Y/N): ", styleSheet);

					// Variáveis locais
					string ch;
					int x;
					string link = string.Empty;

					//do
					//{
					x = Colorful.Console.Read();

					//try
					//{
					ch = Char.ToString(Convert.ToChar(x));
					if (ch.ToLower() == "Y".ToLower())
					{
						// Inserir na base de dados
						do
						{
							// Criar detalhes do website para guardar na base de dados
							WebsiteDetails websiteDetails = new WebsiteDetails();

							Colorful.Console.WriteStyled($"[{DateTime.Now:HH:mm:ss}] Insira aqui o link (Pressiona ESC para cancelar): ", styleSheet);
							websiteDetails.WebsiteUrl = ReadLineWithCancel();
							Colorful.Console.WriteLine();
							Colorful.Console.WriteStyled($"[{DateTime.Now:HH:mm:ss}] Insira o nome da Categoria associado ao link colocado acima: ", styleSheet);
							websiteDetails.Category = Colorful.Console.ReadLine();

							if (link != null)
							{
								AcessDataBase.InsertWebsiteDetailsIntoDataBase(websiteDetails);
								Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Link Foi adicionado com sucesso à base de dados", styleSheet);
							}
						} while (link != null);

						if (link == null && ((dt = AcessDataBase.GetAllUrl()) == null) && dt.Rows.Count == 0)
						{
							Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [CRITICAL] Nenhum link foi adicionado para fazer a verificação", styleSheet);
							Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] O programa vai encerrar", styleSheet);
							Thread.Sleep(1000);
							return;
						}
						else
						{
							PrintLinksToBeParsed(styleSheet, dt);
						}
					}
					else if (ch.ToLower() == "N".ToLower())
					{
						Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] O programa vai encerrar", styleSheet);
						Thread.Sleep(1000);
						return;
					}
					//}
					//catch (OverflowException e)
					//{
					//	Colorful.Console.WriteLine("{0} Value read = {1}.", e.Message, x);
					//	ch = Char.MinValue;
					//	Colorful.Console.WriteLine(m1);
					//}
					//} while (ch != '+');
				}
				else
				{
					PrintLinksToBeParsed(styleSheet, dt);
				}
			}

			// Funcao local que imprime os links obtidos
			static void PrintLinksToBeParsed(StyleSheet styleSheet, DataTable dt)
			{
				Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Obtidos com sucesso os links que serão submetidos ao scrapper", styleSheet);
				Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Número de Links encontrados: {dt.Rows.Count}", styleSheet);

				var table = new ConsoleTable("ID", "WebsiteURL", "Categoria");

				foreach (DataRow dataRow in dt.Rows)
				{
					table.AddRow(dataRow.ItemArray);
				}
				table.Write();
				Colorful.Console.WriteLine();
			}

			int numberOfElementsInsertedIntoTable = 0;

			foreach (DataRow row in dt.Rows)
			{
				int SearchID = Int32.Parse(row["ID_Pesquisa"].ToString());
				string siteUrl = row["SiteUrl"].ToString();
				string Categoria = row["Nome"].ToString();

				Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Preparar para fazer o parsing dos produtos dentro da categoria: {Categoria}", styleSheet);

				List<ProductDetails> productDetails = await scrappingPage.ScrapeWebsiteAsync(siteUrl);

				Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Todas as páginas foram lidas referentes à categoria {Categoria}", Program.M_Options.StyleConsole);

				foreach (var produto in productDetails)
				{
					if (AcessDataBase.InsertProductDetailsIntoDataBase(produto, SearchID))
					{
						numberOfElementsInsertedIntoTable++;
						Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Produto com nome: {produto.ProductName} foi adicionado à base de dados", styleSheet);
					}
				}
			}
			Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Atualizado a base de dados com sucesso. Foram inseridos {numberOfElementsInsertedIntoTable} produtos na tabela de dados", styleSheet);
		}

		private static void ReadWebsiteDetailsFromFile(string filePath)
		{
			string[] lines = System.IO.File.ReadAllLines(filePath);

			int contador = 0;
			foreach (string line in lines)
			{
				// Use a tab to indent each line of the file.

				String[] parts = line.Split(';');

				//Console.WriteLine(line);

				WebsiteDetails websiteDetails = new WebsiteDetails();
				websiteDetails.WebsiteUrl = parts[0];
				websiteDetails.Category = parts[1];
				if ((AcessDataBase.InsertWebsiteDetailsIntoDataBase(websiteDetails) == true))
				{
					contador++;
				}
			}
			Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [INFO] Foram adicionados {contador} links a pesquisar à base de dados", Program.M_Options.StyleConsole);
		}

		private static string ReadLineWithCancel()
		{
			string result = null;

			StringBuilder buffer = new StringBuilder();

			//The key is read passing true for the intercept argument to prevent
			//any characters from displaying when the Escape key is pressed.
			ConsoleKeyInfo info = Colorful.Console.ReadKey(true);
			while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
			{
				Colorful.Console.Write(info.KeyChar);
				buffer.Append(info.KeyChar);
				info = Colorful.Console.ReadKey(true);
			}

			if (info.Key == ConsoleKey.Enter)
			{
				result = buffer.ToString();
			}

			return result;
		}

		private static void HandleParseError(IEnumerable<Error> errs)
		{
			if (errs.IsVersion())
			{
				Colorful.Console.WriteLine("Version Request");
				return;
			}

			if (errs.IsHelp())
			{
				Colorful.Console.WriteLine("Help Request");
				return;
			}
			Colorful.Console.WriteLine("Parser Fail");
		}

		private static void Run(Options opts)
		{
			if (opts.InputWebsites != null)
			{
				foreach (string website in opts.InputWebsites)
				{
					// Verificar se estamos perante um website
					Uri uriResult;
					bool result = Uri.TryCreate(website, UriKind.Absolute, out uriResult)
						&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

					if (result == true && website.Contains("kuantokusta"))
					{
						WebsiteDetails websiteDetails = new WebsiteDetails();
						websiteDetails.WebsiteUrl = website;
						websiteDetails.Category = website.Substring(website.LastIndexOf('/') + 1);
						AcessDataBase.InsertWebsiteDetailsIntoDataBase(websiteDetails);
					}
				}
			}

			if (File.Exists(opts.filename))
			{
				ReadWebsiteDetailsFromFile(opts.filename);
			}
		}
	}
}