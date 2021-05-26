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
    public class SkillCategoryController : Controller
    {
        private readonly ResumeContext _context;

        public SkillCategoryController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/SkillCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.SkillCategories.ToListAsync());
        }

        // GET: Admin/SkillCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillCategory = await _context.SkillCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skillCategory == null)
            {
                return NotFound();
            }

            return View(skillCategory);
        }

        // GET: Admin/SkillCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SkillCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Category,Description")] SkillCategory skillCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skillCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skillCategory);
        }

        // GET: Admin/SkillCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillCategory = await _context.SkillCategories.FindAsync(id);
            if (skillCategory == null)
            {
                return NotFound();
            }
            return View(skillCategory);
        }

        // POST: Admin/SkillCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category,Description")] SkillCategory skillCategory)
        {
            if (id != skillCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skillCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillCategoryExists(skillCategory.ID))
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
            return View(skillCategory);
        }

        // GET: Admin/SkillCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillCategory = await _context.SkillCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skillCategory == null)
            {
                return NotFound();
            }

            return View(skillCategory);
        }

        // POST: Admin/SkillCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skillCategory = await _context.SkillCategories.FindAsync(id);
            _context.SkillCategories.Remove(skillCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillCategoryExists(int id)
        {
            return _context.SkillCategories.Any(e => e.ID == id);
        }
    }
}
