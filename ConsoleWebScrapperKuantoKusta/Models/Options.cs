using Colorful;
using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Models
{
	public class Options
	{
		[Option('r', "filename", Required = false, HelpText = "Introduza caminho do ficheiro.")]
		public string filename { get; set; }

		[Option('a', "add", Required = false, HelpText = "Adiciona website para fazer o tracking.")]
		public IEnumerable<string> InputWebsites { get; set; }

		[Usage(ApplicationAlias = "ConsoleWebScrapperKuantoKusta")]
		public static IEnumerable<Example> Examples
		{
			get
			{
				return new List<Example>() {
		new Example("Faz o parsing às páginas do KuantoKusta ", new Options { filename = "https://www.kuantokusta.pt/informatica/Computadores/Portateis" })
	  };
			}
		}

		public StyleSheet StyleConsole { get; set; }
	}
}