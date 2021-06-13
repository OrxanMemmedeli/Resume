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
    public class ActionNameController : Controller
    {
        private readonly ResumeContext _context;

        public ActionNameController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ActiomNames.OrderBy(x => x.Name).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] ActiomNames actiomNames)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actiomNames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actiomNames);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiomNames = await _context.ActiomNames.FindAsync(id);
            if (actiomNames == null)
            {
                return NotFound();
            }
            return View(actiomNames);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] ActiomNames actiomNames)
        {
            if (id != actiomNames.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actiomNames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiomNamesExists(actiomNames.ID))
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
            return View(actiomNames);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiomNames = await _context.ActiomNames
                .FirstOrDefaultAsync(m => m.ID == id);
            if (actiomNames == null)
            {
                return NotFound();
            }

            return View(actiomNames);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actiomNames = await _context.ActiomNames.FindAsync(id);
            _context.ActiomNames.Remove(actiomNames);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiomNamesExists(int id)
        {
            return _context.ActiomNames.Any(e => e.ID == id);
        }
    }
}
