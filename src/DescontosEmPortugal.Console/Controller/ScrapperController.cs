using Colorful;
using CommandLine;
using DescontosEmPortugal.Console.Models;
using DescontosEmPortugal.Core;
using DescontosEmPortugal.Core.Enums;
using DescontosEmPortugal.Core.Scrapper;
using DescontosEmPortugal.Library.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DescontosEmPortugal.Console.Controller
{
	public class ScrapperController
	{
		public Scrapper Scrapper { get; set; }

		public ScrapperController()
		{
			Scrapper = new Scrapper();
			Scrapper.Notifier += scrapper_Notifier;

			StyleConsole();

			Parser.Default.ParseArguments<Options>(Program.Arguments)
			  .WithParsed(Run)
			  .WithNotParsed(HandleParseError);
		}

		internal async Task Start()
		{
			Program.V_View.ShowTitle();
			await Scrapper.StartScrapping();
		}

		private void StyleConsole()
		{
			// Define o aspeto da aplicação
			StyleSheet styleSheet = new StyleSheet(Color.White);
			string target = @"\[(.*?)\]";
			styleSheet.AddStyle(target, Color.Gray);
			styleSheet.AddStyle(@"\[(INFO)\]", Color.Blue);
			styleSheet.AddStyle(@"\[(WARNING)\]", Color.Yellow);
			styleSheet.AddStyle(@"\[(CRITICAL)\]", Color.Red);

			// Define o tamanho da janela da aplicação
			System.Console.BufferWidth = 150;
			System.Console.SetWindowSize(System.Console.BufferWidth, 50);

			// Prepara o Model
			Program.M_Options.StyleConsole = styleSheet;
		}

		private void scrapper_Notifier(Core.Enums.StatusApplication status, string message)
		{
			Program.Messages.CurrentStatus = status;
			AdjustMessage();

			switch (status)
			{
				case Core.Enums.StatusApplication.Started:
					Program.V_View.DisplayMessage("INFO", Program.Messages.Message + " " + message);
					break;

				case Core.Enums.StatusApplication.StartedScrapping:
					Program.V_View.DisplayMessage("INFO", Program.Messages.Message + " " + message);

					break;

				case Core.Enums.StatusApplication.NoWebsiteFound:
					Program.V_View.DisplayMessage("WARNING", Program.Messages.Message + " " + message);
					InsertWebsiteToTrack();

					break;

				case StatusApplication.WebsitesFound:
					PrintLinksToBeParsed();
					break;

				case Core.Enums.StatusApplication.ConnectionLost:
					Program.V_View.DisplayMessage("CRITICAL", Program.Messages.Message + " " + message);

					break;

				case Core.Enums.StatusApplication.DataBaseUpdated:
					Program.V_View.DisplayMessage("INFO", Program.Messages.Message + " " + message);

					break;

				case Core.Enums.StatusApplication.FinishedScrapping:
					Program.V_View.DisplayMessage("INFO", Program.Messages.Message + " " + message);

					break;

				case Core.Enums.StatusApplication.Terminated:
					Program.V_View.DisplayMessage("INFO", Program.Messages.Message + " " + message);

					break;

				case StatusApplication.Other:
					Program.V_View.DisplayMessage("INFO", Program.Messages.Message + " " + message);
					break;

				default:
					break;
			}
		}

		private void InsertWebsiteToTrack()
		{
			// Variáveis locais
			string ch;
			int x;
			string link = string.Empty;

			//do
			//{
			//try
			//{
			x = Program.V_View.AskUserChar();
			ch = Char.ToString(Convert.ToChar(x));
			if (ch.ToLower() == "Y".ToLower())
			{
				// Inserir na base de dados
				do
				{
					// Criar detalhes do website para guardar na base de dados
					WebsiteDetails websiteDetails = new WebsiteDetails();

					Program.V_View.DisplayMessage("INFO", "Insira aqui o link(Pressiona ESC para cancelar): ");
					websiteDetails.WebsiteUrl = Program.V_View.ReadLineWithCancel();
					Colorful.Console.WriteLine();
					Program.V_View.DisplayMessage("INFO", "Insira o nome da Categoria associado ao link colocado acima: ");
					websiteDetails.Category = Program.V_View.ReadLineWithCancel();

					if (link != null)
					{
						AcessDataBase.InsertWebsiteDetailsIntoDataBase(websiteDetails);
						Program.V_View.DisplayMessage("INFO", "Link Foi adicionado com sucesso à base de dados");
					}
				} while (link != null);

				if (link == null && ((Scrapper.dt = AcessDataBase.GetAllUrl()) == null) && Scrapper.dt.Rows.Count == 0)
				{
					Program.V_View.DisplayMessage("CRITICAL", "Nenhum link foi adicionado para fazer a verificação");
					Program.V_View.DisplayMessage("INFO", "O programa vai encerrar");

					Thread.Sleep(1000);
					return;
				}
				else
				{
					PrintLinksToBeParsed();
				}
			}
			else if (ch.ToLower() == "N".ToLower())
			{
				Program.V_View.DisplayMessage("INFO", "O program vai encerrar");
				Thread.Sleep(1000);
				return;
			}
		}

		private void PrintLinksToBeParsed()
		{
			Program.V_View.DisplayMessage("INFO", "Obtidos com sucesso os links que serão submetidos ao scrapper");
			Program.V_View.DisplayMessage("INFO", $"Número de Links encontrados: { Scrapper.dt.Rows.Count}");
			Program.V_View.DrawTable(Scrapper.dt);
		}

		private void AdjustMessage()
		{
			Program.Messages.Message = Program.Messages.CurrentStatus switch
			{
				StatusApplication.Started => Program.Messages.Message = "Bem vindo ao Programa",
				StatusApplication.StartedScrapping => Program.Messages.Message = "Início do parsing",
				StatusApplication.NoWebsiteFound => Program.Messages.Message = "Não foi possível obter nenhum link",
				StatusApplication.ConnectionLost => Program.Messages.Message = "Conexão perdida",
				StatusApplication.DataBaseUpdated => Program.Messages.Message = "Adicionado à base de dados:",
				StatusApplication.FinishedScrapping => Program.Messages.Message = "Todas as páginas foram lidas referentes à categoria",
				StatusApplication.Terminated => Program.Messages.Message = "Programa vai se encerrar",
				StatusApplication.WebsitesFound => Program.Messages.Message = string.Empty,
				StatusApplication.Other => Program.Messages.Message = string.Empty,
				_ => throw new NotImplementedException(),
			};
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
			Program.V_View.DisplayMessage("INFO", $"Foram adicionados {contador} links a pesquisar à base de dados");
		}

		private static void HandleParseError(IEnumerable<Error> errs)
		{
			if (errs.IsVersion())
			{
				Program.V_View.DisplayMessage("", "Version Request");
				return;
			}

			if (errs.IsHelp())
			{
				Program.V_View.DisplayMessage("", "Help Request");
				return;
			}
			Program.V_View.DisplayMessage("", "Parser Fail");
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