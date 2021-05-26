using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public InfoController(ResumeContext context)
        {
            _context = context;
        }

        // GET: Admin/Info
        public async Task<IActionResult> Index()
        {
            return View(await _context.Infos.ToListAsync());
        }

        // GET: Admin/Info/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var info = await _context.Infos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // GET: Admin/Info/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Surname,Speciality,Email,Telephone,Adress,BirthDate,Profil,FotoURL,Coordinates")] Info info)
        {
            if (ModelState.IsValid)
            {
                _context.Add(info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }

        // GET: Admin/Info/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var info = await _context.Infos.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        // POST: Admin/Info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname,Speciality,Email,Telephone,Adress,BirthDate,Profil,FotoURL,Coordinates")] Info info)
        {
            if (id != info.ID)
            {
                return NotFound();
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

        // GET: Admin/Info/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var info = await _context.Infos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // POST: Admin/Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var info = await _context.Infos.FindAsync(id);
            _context.Infos.Remove(info);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoExists(int id)
        {
            return _context.Infos.Any(e => e.ID == id);
        }
    }
}
