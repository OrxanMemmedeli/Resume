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
    public class ControllerNameController : Controller
    {
        private readonly ResumeContext _context;
        CurrentUser currentUser = new CurrentUser();
        public ControllerNameController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerName", "Index");
            if (roleStatus)
            {
                return View(await _context.ControllerNames.OrderBy(x => x.Name).ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerName", "Create");
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
        public async Task<IActionResult> Create([Bind("ID,Name")] ControllerNames controllerNames)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerName", "Create");
            if (roleStatus)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(controllerNames);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(controllerNames);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerName", "Edit");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] ControllerNames controllerNames)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerName", "Edit");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Delete(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerName", "Delete");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerName", "Delete");
            if (roleStatus)
            {
                var controllerNames = await _context.ControllerNames.FindAsync(id);
                _context.ControllerNames.Remove(controllerNames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool ControllerNamesExists(int id)
        {
            return _context.ControllerNames.Any(e => e.ID == id);
        }
    }
}
