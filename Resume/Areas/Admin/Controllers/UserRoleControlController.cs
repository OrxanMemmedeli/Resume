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
    public class UserRoleControlController : Controller
    {
        private readonly ResumeContext _context;

        public UserRoleControlController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/UserRoleControl
        public async Task<IActionResult> Index()
        {
            var resumeContext = _context.UserRoleControls.Include(u => u.RoleControl).Include(u => u.User);
            return View(await resumeContext.ToListAsync());
        }

        // GET: Admin/UserRoleControl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleControl = await _context.UserRoleControls
                .Include(u => u.RoleControl)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userRoleControl == null)
            {
                return NotFound();
            }

            return View(userRoleControl);
        }

        // GET: Admin/UserRoleControl/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(_context.RoleControls, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: Admin/UserRoleControl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,RoleID")] UserRoleControl userRoleControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRoleControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(_context.RoleControls, "ID", "ID", userRoleControl.RoleID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", userRoleControl.UserID);
            return View(userRoleControl);
        }

        // GET: Admin/UserRoleControl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleControl = await _context.UserRoleControls.FindAsync(id);
            if (userRoleControl == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(_context.RoleControls, "ID", "ID", userRoleControl.RoleID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", userRoleControl.UserID);
            return View(userRoleControl);
        }

        // POST: Admin/UserRoleControl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,RoleID")] UserRoleControl userRoleControl)
        {
            if (id != userRoleControl.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRoleControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleControlExists(userRoleControl.UserID))
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
            ViewData["RoleID"] = new SelectList(_context.RoleControls, "ID", "ID", userRoleControl.RoleID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", userRoleControl.UserID);
            return View(userRoleControl);
        }

        // GET: Admin/UserRoleControl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleControl = await _context.UserRoleControls
                .Include(u => u.RoleControl)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userRoleControl == null)
            {
                return NotFound();
            }

            return View(userRoleControl);
        }

        // POST: Admin/UserRoleControl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRoleControl = await _context.UserRoleControls.FindAsync(id);
            _context.UserRoleControls.Remove(userRoleControl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleControlExists(int id)
        {
            return _context.UserRoleControls.Any(e => e.UserID == id);
        }
    }
}
