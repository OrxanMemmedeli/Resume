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
    public class EducationController : Controller
    {
        private readonly ResumeContext _context;
        CurrentUser currentUser = new CurrentUser();
        public EducationController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Education", "Index");
            if (roleStatus)
            {
                return View(await _context.Educations.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Education", "Create");
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
        public async Task<IActionResult> Create(Education education)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Education", "Create");
            if (roleStatus)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(education);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(education);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Education", "Edit");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var education = await _context.Educations.FindAsync(dID);
                if (education == null)
                {
                    return NotFound();
                }
                return View(education);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Education education)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Education", "Edit");
            if (roleStatus)
            {
                if (id != education.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(education);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EducationExists(education.ID))
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
                return View(education);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Delete(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Education", "Delete");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var education = await _context.Educations
                    .FirstOrDefaultAsync(m => m.ID == dID);
                if (education == null)
                {
                    return NotFound();
                }

                return View(education);
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
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Education", "Delete");
            if (roleStatus)
            {
                var education = await _context.Educations.FindAsync(id);
                _context.Educations.Remove(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.ID == id);
        }
    }
}
