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
    public class HideTableController : Controller
    {
        private readonly ResumeContext _context;

        public HideTableController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/HideTable
        public async Task<IActionResult> Index()
        {
            return View(await _context.HideTables.ToListAsync());
        }

        // GET: Admin/HideTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hideTable = await _context.HideTables
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hideTable == null)
            {
                return NotFound();
            }

            return View(hideTable);
        }

        // GET: Admin/HideTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HideTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Admin/HideTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hideTable = await _context.HideTables.FindAsync(id);
            if (hideTable == null)
            {
                return NotFound();
            }
            return View(hideTable);
        }

        // POST: Admin/HideTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Admin/HideTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hideTable = await _context.HideTables
                .FirstOrDefaultAsync(m => m.ID == id);
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
