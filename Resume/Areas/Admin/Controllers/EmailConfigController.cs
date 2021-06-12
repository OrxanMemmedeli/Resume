using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Business.Tools;
using Resume.Models.Context;
using Resume.Models.Entities;

namespace Resume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailConfigController : Controller
    {
        private readonly ResumeContext _context;

        public EmailConfigController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailConfigs.FirstOrDefaultAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmailConfig emailConfig, string NewPassword)
        {
            if (id != emailConfig.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    emailConfig.Password = AncryptionAndDecryption.encodedata("encodedata" + AncryptionAndDecryption.encodedata(NewPassword));
                }

                try
                {
                    _context.Update(emailConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailConfigExists(emailConfig.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["EmailConfig"] = "Məlumatlara dəyişiklik edildi";
                return RedirectToAction(nameof(Index));
            }
            return View(emailConfig);
        }

        private bool EmailConfigExists(int id)
        {
            return _context.EmailConfigs.Any(e => e.ID == id);
        }
    }
}
