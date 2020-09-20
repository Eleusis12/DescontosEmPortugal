using DescontosEmPortugal.Console.Controller;
using DescontosEmPortugal.Console.Messages;
using DescontosEmPortugal.Console.Models;
using DescontosEmPortugal.Console.Views;
using System;
using System.Threading.Tasks;

namespace DescontosEmPortugal.Console
{
	public static class Program
	{
		public static Options M_Options { get; set; }
		public static ScrapperController C_Application { get; set; }
		public static UserInterface V_View { get; set; }

		public static string[] Arguments { get; set; }

		public static Messages.Messages Messages { get; set; }

		private static async Task Main(string[] args)
		{
			Messages = new Messages.Messages();
			Arguments = args;

			//MVC
			M_Options = new Options();
			V_View = new UserInterface();
			C_Application = new ScrapperController();
			await C_Application.Start();
		}
	}
}