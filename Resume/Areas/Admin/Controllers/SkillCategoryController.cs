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
    public class SkillCategoryController : Controller
    {
        private readonly ResumeContext _context;

        public SkillCategoryController(ResumeContext context)
        {
            _context = context;
        }
        CurrentUser currentUser = new CurrentUser();
        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "SkillCategory", "Index");
            if (roleStatus)
            {
                return View(await _context.SkillCategories.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "SkillCategory", "Create");
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
        public async Task<IActionResult> Create([Bind("ID,Category,Description")] SkillCategory skillCategory)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "SkillCategory", "Create");
            if (roleStatus)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(skillCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(skillCategory);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "SkillCategory", "Edit");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var skillCategory = await _context.SkillCategories.FindAsync(dID);
                if (skillCategory == null)
                {
                    return NotFound();
                }
                return View(skillCategory);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category,Description")] SkillCategory skillCategory)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "SkillCategory", "Edit");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        public async Task<IActionResult> Delete(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "SkillCategory", "Delete");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var skillCategory = await _context.SkillCategories
                    .FirstOrDefaultAsync(m => m.ID == dID);
                if (skillCategory == null)
                {
                    return NotFound();
                }

                return View(skillCategory);
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
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "SkillCategory", "Delete");
            if (roleStatus)
            {
                var skillCategory = await _context.SkillCategories.FindAsync(id);
                _context.SkillCategories.Remove(skillCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool SkillCategoryExists(int id)
        {
            return _context.SkillCategories.Any(e => e.ID == id);
        }
    }
}
