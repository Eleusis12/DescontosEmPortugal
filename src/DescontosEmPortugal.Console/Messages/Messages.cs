using DescontosEmPortugal.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DescontosEmPortugal.Console.Messages
{
	public class Messages
	{
		private string message;

		public string Message
		{
			get
			{
				return message;
			}
			set
			{
				message = value;
			}
		}

		public StatusApplication CurrentStatus { get; set; }
	}
}