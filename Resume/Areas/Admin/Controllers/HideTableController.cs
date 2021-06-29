using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Business.Control;
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
        CurrentUser currentUser = new CurrentUser();
        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "HideTable", "Index");
            if (roleStatus)
            {
                return View(await _context.HideTables.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "HideTable", "Create");
            if (roleStatus)
            {
                return View();
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TableName,Status")] HideTable hideTable)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "HideTable", "Create");
            if (roleStatus)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(hideTable);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(hideTable);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "HideTable", "Edit");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TableName,Status")] HideTable hideTable)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "HideTable", "Edit");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        public async Task<IActionResult> Delete(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "HideTable", "Delete");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "HideTable", "Delete");
            if (roleStatus)
            {
                var hideTable = await _context.HideTables.FindAsync(id);
                _context.HideTables.Remove(hideTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool HideTableExists(int id)
        {
            return _context.HideTables.Any(e => e.ID == id);
        }
    }
}
