using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {

            return View(await _context.Infos.FirstOrDefaultAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, Info info)
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

        private void AddFotoFile(Info info)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(companyInfo.image.FileName);
            string extension = Path.GetExtension(info.Foto.FileName);
            info.FotoURL = /*fileName +*/ DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Upload/Images/", info.FotoURL);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                info.Foto.CopyTo(fileStream);
            }
        }


        private bool InfoExists(int id)
        {
            return _context.Infos.Any(e => e.ID == id);
        }
    }
}
