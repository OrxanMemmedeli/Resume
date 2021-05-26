using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Models.Context;
using Resume.Models.Entities;

namespace Resume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly ResumeContext _context;

        public PortfolioController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/Portfolio
        public async Task<IActionResult> Index()
        {
            var resumeContext = _context.Portfolios.Include(p => p.PortfolioCategory);
            return View(await resumeContext.ToListAsync());
        }

        // GET: Admin/Portfolio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios
                .Include(p => p.PortfolioCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // GET: Admin/Portfolio/Create
        public IActionResult Create()
        {
            ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "ID");
            return View();
        }

        // POST: Admin/Portfolio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,InsertDate,EndDate,Client,SiteURL,Type,FotoURL,VideoURL,PortfolioCategoryID")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "ID", portfolio.PortfolioCategoryID);
            return View(portfolio);
        }

        // GET: Admin/Portfolio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "ID", portfolio.PortfolioCategoryID);
            return View(portfolio);
        }

        // POST: Admin/Portfolio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,InsertDate,EndDate,Client,SiteURL,Type,FotoURL,VideoURL,PortfolioCategoryID")] Portfolio portfolio)
        {
            if (id != portfolio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioExists(portfolio.ID))
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
            ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "ID", portfolio.PortfolioCategoryID);
            return View(portfolio);
        }

        // GET: Admin/Portfolio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios
                .Include(p => p.PortfolioCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // POST: Admin/Portfolio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.ID == id);
        }
    }
}
