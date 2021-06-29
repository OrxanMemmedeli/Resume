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
    public class PortfolioController : Controller
    {
        private readonly ResumeContext _context;

        public PortfolioController(ResumeContext context)
        {
            _context = context;
        }
        CurrentUser currentUser = new CurrentUser();
        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Index");
            if (roleStatus)
            {
                var resumeContext = _context.Portfolios.Include(p => p.PortfolioCategory);
                return View(await resumeContext.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Details(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Details");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var portfolio = await _context.Portfolios
                    .Include(p => p.PortfolioCategory)
                    .FirstOrDefaultAsync(m => m.ID == dID);
                if (portfolio == null)
                {
                    return NotFound();
                }

                return View(portfolio);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Create");
            if (roleStatus)
            {
                ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "Category");
                return View();
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,InsertDate,EndDate,Client,SiteURL,Type,FotoURL,VideoURL,PortfolioCategoryID")] Portfolio portfolio)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Create");
            if (roleStatus)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(portfolio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "Category", portfolio.PortfolioCategoryID);
                return View(portfolio);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Edit");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var portfolio = await _context.Portfolios.FindAsync(dID);
                if (portfolio == null)
                {
                    return NotFound();
                }
                ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "Category", portfolio.PortfolioCategoryID);
                return View(portfolio);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,InsertDate,EndDate,Client,SiteURL,Type,FotoURL,VideoURL,PortfolioCategoryID")] Portfolio portfolio)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Edit");
            if (roleStatus)
            {
                if (id != portfolio.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(portfolio);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PortfolioExists(portfolio.ID))
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
                ViewData["PortfolioCategoryID"] = new SelectList(_context.PortfolioCategories, "ID", "Category", portfolio.PortfolioCategoryID);
                return View(portfolio);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Delete(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Delete");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var portfolio = await _context.Portfolios
                    .Include(p => p.PortfolioCategory)
                    .FirstOrDefaultAsync(m => m.ID == dID);
                if (portfolio == null)
                {
                    return NotFound();
                }

                return View(portfolio);
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
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Portfolio", "Delete");
            if (roleStatus)
            {
                var portfolio = await _context.Portfolios.FindAsync(id);
                _context.Portfolios.Remove(portfolio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.ID == id);
        }
    }
}
