using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Business.Tools;
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

        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfolioCategories.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

            var portfolioCategory = await _context.PortfolioCategories.FindAsync(dID);
            if (portfolioCategory == null)
            {
                return NotFound();
            }
            return View(portfolioCategory);
        }

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

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

            var portfolioCategory = await _context.PortfolioCategories
                .FirstOrDefaultAsync(m => m.ID == dID);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

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
