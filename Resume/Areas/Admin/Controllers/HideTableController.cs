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
    public class HideTableController : Controller
    {
        private readonly ResumeContext _context;

        public HideTableController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.HideTables.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TableName,Status")] HideTable hideTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hideTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hideTable);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

            var hideTable = await _context.HideTables.FindAsync(dID);
            if (hideTable == null)
            {
                return NotFound();
            }
            return View(hideTable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TableName,Status")] HideTable hideTable)
        {
            if (id != hideTable.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hideTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HideTableExists(hideTable.ID))
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
            return View(hideTable);
        }

 
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

            var hideTable = await _context.HideTables
                .FirstOrDefaultAsync(m => m.ID == dID);
            if (hideTable == null)
            {
                return NotFound();
            }

            return View(hideTable);
        }

        // POST: Admin/HideTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hideTable = await _context.HideTables.FindAsync(id);
            _context.HideTables.Remove(hideTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HideTableExists(int id)
        {
            return _context.HideTables.Any(e => e.ID == id);
        }
    }
}
