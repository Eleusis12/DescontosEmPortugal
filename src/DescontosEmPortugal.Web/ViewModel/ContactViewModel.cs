using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DescontosEmPortugal.Web.ViewModel
{
	public class ContactViewModel
	{
		[Display(Name = "Primeiro Nome")]
		[Required(ErrorMessage = "O campo Primeiro Nome é necessário.")]
		public string FirstName { get; set; }

		[Display(Name = "Último Nome")]
		[Required(ErrorMessage = "O campo Último Nome é necessário.")]
		public string LastName { get; set; }

		public string Subject { get; set; }

		[Required]
		public string Country { get; set; }

		[Display(Name = "Endereço Email")]
		[Required(ErrorMessage = "O campo Endereço Email é necessário")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Display(Name = "Mensagem")]
		[Required(ErrorMessage = "O campo Mensagem é necessário.")]
		public string Message { get; set; }
	}
}