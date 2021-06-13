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
    public class ControllerActionUsersController : Controller
    {
        private readonly ResumeContext _context;

        public ControllerActionUsersController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/ControllerActionUsers
        public async Task<IActionResult> Index()
        {
            var resumeContext = _context.ControllerActionUsers.Include(c => c.ActiomNames).Include(c => c.ControllerNames).Include(c => c.User);
            return View(await resumeContext.ToListAsync());
        }

        // GET: Admin/ControllerActionUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controllerActionUser = await _context.ControllerActionUsers
                .Include(c => c.ActiomNames)
                .Include(c => c.ControllerNames)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ActionID == id);
            if (controllerActionUser == null)
            {
                return NotFound();
            }

            return View(controllerActionUser);
        }

        // GET: Admin/ControllerActionUsers/Create
        public IActionResult Create()
        {
            ViewData["ActionID"] = new SelectList(_context.ActiomNames, "ID", "Name");
            ViewData["ControllerID"] = new SelectList(_context.ControllerNames, "ID", "Name");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: Admin/ControllerActionUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ControllerID,ActionID,UserID")] ControllerActionUser controllerActionUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controllerActionUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionID"] = new SelectList(_context.ActiomNames, "ID", "Name", controllerActionUser.ActionID);
            ViewData["ControllerID"] = new SelectList(_context.ControllerNames, "ID", "Name", controllerActionUser.ControllerID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", controllerActionUser.UserID);
            return View(controllerActionUser);
        }

        // GET: Admin/ControllerActionUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controllerActionUser = await _context.ControllerActionUsers.FindAsync(id);
            if (controllerActionUser == null)
            {
                return NotFound();
            }
            ViewData["ActionID"] = new SelectList(_context.ActiomNames, "ID", "Name", controllerActionUser.ActionID);
            ViewData["ControllerID"] = new SelectList(_context.ControllerNames, "ID", "Name", controllerActionUser.ControllerID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", controllerActionUser.UserID);
            return View(controllerActionUser);
        }

        // POST: Admin/ControllerActionUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ControllerID,ActionID,UserID")] ControllerActionUser controllerActionUser)
        {
            if (id != controllerActionUser.ActionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controllerActionUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControllerActionUserExists(controllerActionUser.ActionID))
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
            ViewData["ActionID"] = new SelectList(_context.ActiomNames, "ID", "Name", controllerActionUser.ActionID);
            ViewData["ControllerID"] = new SelectList(_context.ControllerNames, "ID", "Name", controllerActionUser.ControllerID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", controllerActionUser.UserID);
            return View(controllerActionUser);
        }

        // GET: Admin/ControllerActionUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controllerActionUser = await _context.ControllerActionUsers
                .Include(c => c.ActiomNames)
                .Include(c => c.ControllerNames)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ActionID == id);
            if (controllerActionUser == null)
            {
                return NotFound();
            }

            return View(controllerActionUser);
        }

        // POST: Admin/ControllerActionUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controllerActionUser = await _context.ControllerActionUsers.FindAsync(id);
            _context.ControllerActionUsers.Remove(controllerActionUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControllerActionUserExists(int id)
        {
            return _context.ControllerActionUsers.Any(e => e.ActionID == id);
        }
    }
}
