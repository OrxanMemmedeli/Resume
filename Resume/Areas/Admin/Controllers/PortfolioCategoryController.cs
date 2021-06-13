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
    public class PortfolioCategoryController : Controller
    {
        private readonly ResumeContext _context;

        public PortfolioCategoryController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/PortfolioCategoriy
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfolioCategories.ToListAsync());
        }

        // GET: Admin/PortfolioCategoriy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // GET: Admin/PortfolioCategoriy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PortfolioCategoriy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Category,Description")] PortfolioCategory portfolioCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolioCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioCategory);
        }

        // GET: Admin/PortfolioCategoriy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }
            return View(portfolioCategory);
        }

        // POST: Admin/PortfolioCategoriy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category,Description")] PortfolioCategory portfolioCategory)
        {
            if (id != portfolioCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioCategoryExists(portfolioCategory.ID))
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
            return View(portfolioCategory);
        }

        // GET: Admin/PortfolioCategoriy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // POST: Admin/PortfolioCategoriy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            _context.PortfolioCategories.Remove(portfolioCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioCategoryExists(int id)
        {
            return _context.PortfolioCategories.Any(e => e.ID == id);
        }
    }
}
