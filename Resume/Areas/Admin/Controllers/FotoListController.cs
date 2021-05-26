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
    public class FotoListController : Controller
    {
        private readonly ResumeContext _context;

        public FotoListController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/FotoList
        public async Task<IActionResult> Index()
        {
            var resumeContext = _context.FotoLists.Include(f => f.Portfolio);
            return View(await resumeContext.ToListAsync());
        }

        // GET: Admin/FotoList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotoList = await _context.FotoLists
                .Include(f => f.Portfolio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fotoList == null)
            {
                return NotFound();
            }

            return View(fotoList);
        }

        // GET: Admin/FotoList/Create
        public IActionResult Create()
        {
            ViewData["PortfolioID"] = new SelectList(_context.Portfolios, "ID", "ID");
            return View();
        }

        // POST: Admin/FotoList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FotoURL,PortfolioID")] FotoList fotoList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fotoList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioID"] = new SelectList(_context.Portfolios, "ID", "ID", fotoList.PortfolioID);
            return View(fotoList);
        }

        // GET: Admin/FotoList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotoList = await _context.FotoLists.FindAsync(id);
            if (fotoList == null)
            {
                return NotFound();
            }
            ViewData["PortfolioID"] = new SelectList(_context.Portfolios, "ID", "ID", fotoList.PortfolioID);
            return View(fotoList);
        }

        // POST: Admin/FotoList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FotoURL,PortfolioID")] FotoList fotoList)
        {
            if (id != fotoList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotoList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotoListExists(fotoList.ID))
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
            ViewData["PortfolioID"] = new SelectList(_context.Portfolios, "ID", "ID", fotoList.PortfolioID);
            return View(fotoList);
        }

        // GET: Admin/FotoList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotoList = await _context.FotoLists
                .Include(f => f.Portfolio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (fotoList == null)
            {
                return NotFound();
            }

            return View(fotoList);
        }

        // POST: Admin/FotoList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotoList = await _context.FotoLists.FindAsync(id);
            _context.FotoLists.Remove(fotoList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotoListExists(int id)
        {
            return _context.FotoLists.Any(e => e.ID == id);
        }
    }
}
