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
    public class ActionNamesController : Controller
    {
        private readonly ResumeContext _context;
        CurrentUser currentUser = new CurrentUser();
        public ActionNamesController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ActionNames", "Index");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));
                ViewBag.Controller = id;
                var resumeContext = _context.ActionNames.Where(x => x.ControllerNamesID == dID).Include(a => a.ControllerNames);
                return View(await resumeContext.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        public IActionResult Create(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ActionNames", "Create");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));
                ViewBag.Controller = dID;
                return View();
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActionNames actionNames)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ActionNames", "Create");
            if (roleStatus)
            {
                actionNames.ID = 0;
                if (ModelState.IsValid)
                {
                    _context.Add(actionNames);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { id = IDAncryption.Encrypt(actionNames.ControllerNamesID.ToString()) });
                }
                ViewBag.Controller = actionNames.ControllerNamesID;
                return View(actionNames);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ActionNames", "Edit");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var actionNames = await _context.ActionNames.FindAsync(dID);
                if (actionNames == null)
                {
                    return NotFound();
                }
                ViewBag.Controller = actionNames.ControllerNamesID;
                return View(actionNames);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActionNames actionNames)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ActionNames", "Edit");
            if (roleStatus)
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
                    return RedirectToAction(nameof(Index), new { id = IDAncryption.Encrypt(actionNames.ControllerNamesID.ToString()) });
                }
                ViewBag.Controller = actionNames.ControllerNamesID;
                return View(actionNames);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Delete(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ActionNames", "Delete");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var actionNames = await _context.ActionNames
                    .Include(a => a.ControllerNames)
                    .FirstOrDefaultAsync(m => m.ID == dID);
                if (actionNames == null)
                {
                    return NotFound();
                }

                return View(actionNames);
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
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ActionNames", "Delete");
            if (roleStatus)
            {
                var actionNames = await _context.ActionNames.FindAsync(id);
                _context.ActionNames.Remove(actionNames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = IDAncryption.Encrypt(actionNames.ControllerNamesID.ToString()) });
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool ActionNamesExists(int id)
        {
            return _context.ActionNames.Any(e => e.ID == id);
        }
    }
}
