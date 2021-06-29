using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Business.Control;
using Resume.Models.Context;
using Resume.Models.Entities;

namespace Resume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogCategoryController : Controller
    {
        private readonly ResumeContext _context;
        CurrentUser currentUser = new CurrentUser();
        public BlogCategoryController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "BlogCategory", "Index");
            if (roleStatus)
            {
                return View(await _context.BlogCategories.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "BlogCategory", "Create");
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
        public async Task<IActionResult> Create([Bind("ID,Category")] BlogCategory blogCategory)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "BlogCategory", "Create");
            if (roleStatus)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(blogCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(blogCategory);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "BlogCategory", "Edit");
            if (roleStatus)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blogCategory = await _context.BlogCategories.FindAsync(id);
                if (blogCategory == null)
                {
                    return NotFound();
                }
                return View(blogCategory);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category")] BlogCategory blogCategory)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "BlogCategory", "Edit");
            if (roleStatus)
            {
                if (id != blogCategory.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(blogCategory);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BlogCategoryExists(blogCategory.ID))
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
                return View(blogCategory);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "BlogCategory", "Delete");
            if (roleStatus)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blogCategory = await _context.BlogCategories
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (blogCategory == null)
                {
                    return NotFound();
                }

                return View(blogCategory);
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
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "BlogCategory", "Delete");
            if (roleStatus)
            {
                var blogCategory = await _context.BlogCategories.FindAsync(id);
                _context.BlogCategories.Remove(blogCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool BlogCategoryExists(int id)
        {
            return _context.BlogCategories.Any(e => e.ID == id);
        }
    }
}
