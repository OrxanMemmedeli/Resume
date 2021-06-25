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
    public class SosialController : Controller
    {
        private readonly ResumeContext _context;

        public SosialController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sosials.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Icon,PageURL")] Sosial sosial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sosial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sosial);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sosial = await _context.Sosials.FindAsync(id);
            if (sosial == null)
            {
                return NotFound();
            }
            return View(sosial);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Icon,PageURL")] Sosial sosial)
        {
            if (id != sosial.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sosial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SosialExists(sosial.ID))
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
            return View(sosial);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sosial = await _context.Sosials
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sosial == null)
            {
                return NotFound();
            }

            return View(sosial);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sosial = await _context.Sosials.FindAsync(id);
            _context.Sosials.Remove(sosial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SosialExists(int id)
        {
            return _context.Sosials.Any(e => e.ID == id);
        }
    }
}
