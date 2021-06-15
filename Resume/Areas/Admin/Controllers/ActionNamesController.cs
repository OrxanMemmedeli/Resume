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
    public class ActionNamesController : Controller
    {
        private readonly ResumeContext _context;

        public ActionNamesController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Controller = id;
            var resumeContext = _context.ActionNames.Where(x => x.ControllerNamesID == id).Include(a => a.ControllerNames);
            return View(await resumeContext.ToListAsync());
        }


        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Controller = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ControllerNamesID")] ActionNames actionNames)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actionNames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Controller = actionNames.ControllerNamesID;
            return View(actionNames);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionNames = await _context.ActionNames.FindAsync(id);
            if (actionNames == null)
            {
                return NotFound();
            }
            ViewBag.Controller = actionNames.ControllerNamesID;
            return View(actionNames);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ControllerNamesID")] ActionNames actionNames)
        {
            if (id != actionNames.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actionNames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionNamesExists(actionNames.ID))
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
            ViewBag.Controller = actionNames.ControllerNamesID;
            return View(actionNames);
        }

        // GET: Admin/ActionNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionNames = await _context.ActionNames
                .Include(a => a.ControllerNames)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (actionNames == null)
            {
                return NotFound();
            }

            return View(actionNames);
        }

        // POST: Admin/ActionNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actionNames = await _context.ActionNames.FindAsync(id);
            _context.ActionNames.Remove(actionNames);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActionNamesExists(int id)
        {
            return _context.ActionNames.Any(e => e.ID == id);
        }
    }
}
