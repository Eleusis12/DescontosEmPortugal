using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DescontosEmPortugal.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DescontosEmPortugal.Web.Controllers
{
	public class WebsitesController : Controller
	{
		private readonly ProdutosContext _context;

		public WebsitesController(ProdutosContext context)
		{
			_context = context;
		}

		// GET: Websites
		public async Task<IActionResult> Index()
		{
			return View(await _context.Website.ToListAsync());
		}

		// GET: Websites/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var website = await _context.Website
				.FirstOrDefaultAsync(m => m.IdWebsite == id);
			if (website == null)
			{
				return NotFound();
			}

			return View(website);
		}

		// GET: Websites/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Websites/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("IdWebsite,SiteUrl")] Website website)
		{
			if (ModelState.IsValid)
			{
				_context.Add(website);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(website);
		}

		// GET: Websites/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var website = await _context.Website.FindAsync(id);
			if (website == null)
			{
				return NotFound();
			}
			return View(website);
		}

		// POST: Websites/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("IdWebsite,SiteUrl")] Website website)
		{
			if (id != website.IdWebsite)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(website);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!WebsiteExists(website.IdWebsite))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(website);
		}

		// GET: Websites/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var website = await _context.Website
				.FirstOrDefaultAsync(m => m.IdWebsite == id);
			if (website == null)
			{
				return NotFound();
			}

			return View(website);
		}

		// POST: Websites/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var website = await _context.Website.FindAsync(id);
			_context.Website.Remove(website);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool WebsiteExists(int id)
		{
			return _context.Website.Any(e => e.IdWebsite == id);
		}
	}
}