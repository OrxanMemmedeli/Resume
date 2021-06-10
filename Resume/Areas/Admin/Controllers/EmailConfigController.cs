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

        // GET: Admin/EmailConfig
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailConfigs.ToListAsync());
        }

        // GET: Admin/EmailConfig/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailConfig = await _context.EmailConfigs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (emailConfig == null)
            {
                return NotFound();
            }

            return View(emailConfig);
        }

        // GET: Admin/EmailConfig/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/EmailConfig/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Gmail,Password")] EmailConfig emailConfig)
        {

            if (ModelState.IsValid)
            {
                emailConfig.Password = AncryptionAndDecryption.encodedata("encodedata" + AncryptionAndDecryption.encodedata(emailConfig.Password));

                _context.Add(emailConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailConfig);
        }

        // GET: Admin/EmailConfig/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailConfig = await _context.EmailConfigs.FindAsync(id);
            if (emailConfig == null)
            {
                return NotFound();
            }
            return View(emailConfig);
        }

        // POST: Admin/EmailConfig/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Gmail,Password")] EmailConfig emailConfig)
        {
            if (id != emailConfig.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            return View(emailConfig);
        }

        // GET: Admin/EmailConfig/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailConfig = await _context.EmailConfigs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (emailConfig == null)
            {
                return NotFound();
            }

            return View(emailConfig);
        }

        // POST: Admin/EmailConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailConfig = await _context.EmailConfigs.FindAsync(id);
            _context.EmailConfigs.Remove(emailConfig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailConfigExists(int id)
        {
            return _context.EmailConfigs.Any(e => e.ID == id);
        }
    }
}
