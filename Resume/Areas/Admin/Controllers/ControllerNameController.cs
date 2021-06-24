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
    public class ControllerNameController : Controller
    {
        private readonly ResumeContext _context;

        public ControllerNameController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ControllerNames.OrderBy(x => x.Name).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] ControllerNames controllerNames)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controllerNames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(controllerNames);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));


            var controllerNames = await _context.ControllerNames.FindAsync(dID);
            if (controllerNames == null)
            {
                return NotFound();
            }
            return View(controllerNames);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] ControllerNames controllerNames)
        {
            if (id != controllerNames.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controllerNames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControllerNamesExists(controllerNames.ID))
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
            return View(controllerNames);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

            var controllerNames = await _context.ControllerNames
                .FirstOrDefaultAsync(m => m.ID == dID);
            if (controllerNames == null)
            {
                return NotFound();
            }

            return View(controllerNames);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controllerNames = await _context.ControllerNames.FindAsync(id);
            _context.ControllerNames.Remove(controllerNames);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControllerNamesExists(int id)
        {
            return _context.ControllerNames.Any(e => e.ID == id);
        }
    }
}
