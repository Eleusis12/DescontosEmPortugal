using Colorful;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace DescontosEmPortugal.Console.Views
{
	public class UserInterface
	{

		public UserInterface()
		{
		}

		internal void ShowTitle()
		{
			Colorful.Console.WriteAscii("KuantoKusta Scrapper", Color.FromArgb(244, 212, 255));
		}

		internal void DisplayMessage(object typeOfMessage, string message)
		{
			Colorful.Console.WriteLineStyled($"[{DateTime.Now:HH:mm:ss}] [{typeOfMessage}] {message}", Program.M_Options.StyleConsole);
		}

		internal int AskUserChar()
		{
			return Colorful.Console.Read();
		}

		internal string ReadLineWithCancel()
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

		internal void DrawTable(DataTable dt)
		{
			var table = new ConsoleTable("ID", "WebsiteURL", "Categoria");

			foreach (DataRow dataRow in dt.Rows)
			{
				table.AddRow(dataRow.ItemArray);
			}
			table.Write();
			Colorful.Console.WriteLine();
		}
	}
}