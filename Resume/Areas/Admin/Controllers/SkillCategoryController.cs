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

        public async Task<IActionResult> Index()
        {
            return View(await _context.SkillCategories.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }


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
