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
    public class BlogController : Controller
    {
        private readonly ResumeContext _context;
        CurrentUser currentUser = new CurrentUser();
        public BlogController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Blog", "Index");
            if (roleStatus)
            {
                var resumeContext = _context.Blogs.Include(b => b.BlogCategory);
                return View(await resumeContext.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Blog", "Create");
            if (roleStatus)
            {
                ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "ID");
                return View();
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Text,FotoURL,Datetime,BlogCategoryID")] Blog blog)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Blog", "Create");
            if (roleStatus)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(blog);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Category", blog.BlogCategoryID);
                return View(blog);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Blog", "Edit");
            if (roleStatus)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blog = await _context.Blogs.FindAsync(id);
                if (blog == null)
                {
                    return NotFound();
                }
                ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Category", blog.BlogCategoryID);
                return View(blog);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Text,FotoURL,Datetime,BlogCategoryID")] Blog blog)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Blog", "Edit");
            if (roleStatus)
            {
                if (id != blog.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(blog);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BlogExists(blog.ID))
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
                ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Category", blog.BlogCategoryID);
                return View(blog);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Blog", "Delete");
            if (roleStatus)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var blog = await _context.Blogs
                    .Include(b => b.BlogCategory)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (blog == null)
                {
                    return NotFound();
                }

                return View(blog);
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
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Blog", "Delete");
            if (roleStatus)
            {
                var blog = await _context.Blogs.FindAsync(id);
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.ID == id);
        }
    }
}
