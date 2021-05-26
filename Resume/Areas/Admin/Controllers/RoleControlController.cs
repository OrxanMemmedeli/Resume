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
    public class RoleControlController : Controller
    {
        private readonly ResumeContext _context;

        public RoleControlController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/RoleControl
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoleControls.ToListAsync());
        }

        // GET: Admin/RoleControl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleControl = await _context.RoleControls
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roleControl == null)
            {
                return NotFound();
            }

            return View(roleControl);
        }

        // GET: Admin/RoleControl/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RoleControl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ControllerName,ActionName")] RoleControl roleControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleControl);
        }

        // GET: Admin/RoleControl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleControl = await _context.RoleControls.FindAsync(id);
            if (roleControl == null)
            {
                return NotFound();
            }
            return View(roleControl);
        }

        // POST: Admin/RoleControl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ControllerName,ActionName")] RoleControl roleControl)
        {
            if (id != roleControl.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleControlExists(roleControl.ID))
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
            return View(roleControl);
        }

        // GET: Admin/RoleControl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleControl = await _context.RoleControls
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roleControl == null)
            {
                return NotFound();
            }

            return View(roleControl);
        }

        // POST: Admin/RoleControl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleControl = await _context.RoleControls.FindAsync(id);
            _context.RoleControls.Remove(roleControl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleControlExists(int id)
        {
            return _context.RoleControls.Any(e => e.ID == id);
        }
    }
}
