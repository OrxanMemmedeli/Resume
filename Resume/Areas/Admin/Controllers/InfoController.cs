using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Business.Control;
using Resume.Models.Context;
using Resume.Models.Entities;

namespace Resume.Areas.Admin
{
    [Area("Admin")]
    public class InfoController : Controller
    {
        private readonly ResumeContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public InfoController(ResumeContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        CurrentUser currentUser = new CurrentUser();
        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Info", "Index");
            if (roleStatus)
            {
                var info = await _context.Infos.FirstOrDefaultAsync();
                return View(info);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, Info info)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Info", "Index");
            if (roleStatus)
            {
                if (id != info.ID)
                {
                    return NotFound();
                }

                if (info.Foto != null)
                {
                    AddFotoFile(info);
                }


                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(info);
                        await _context.SaveChangesAsync();
                        TempData["InfoSuccess"] = "Məlumatlar yeniləndi";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!InfoExists(info.ID))
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
                return View(info);
            }
            else
            {
                return Redirect("/Account/Denied");
            }


        }

        private void AddFotoFile(Info info)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(info.Foto.FileName);

            string extension = Path.GetExtension(info.Foto.FileName);
            string newImageName = DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "\\Upload\\Images\\", newImageName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                info.FotoURL = "/Upload/Images/" + newImageName;
                info.Foto.CopyTo(fileStream);
            }
        }


        private bool InfoExists(int id)
        {
            return _context.Infos.Any(e => e.ID == id);
        }
    }
}
