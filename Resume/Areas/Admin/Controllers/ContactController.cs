using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Models.Context;
using Resume.Models.Entities;
using Resume.Models.ViewModels;

namespace Resume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly ResumeContext _context;

        public ContactController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string category)
        {
            var messages = new List<Contact>();

            if (category == null)
            {
                category = "Inbox";
            }

            switch (category)
            {
                case "Inbox":
                    messages = await _context.Contacts.Where(s => s.Respons == false && s.Status == true).OrderByDescending(x => x.InsertDate).ToListAsync();
                    break;
                case "Read":
                    messages = await _context.Contacts.Where(s => s.Respons == false && s.Status == false).OrderByDescending(x => x.InsertDate).ToListAsync();
                    break;
                case "Answered":
                    messages = await _context.Contacts.Where(s => s.Respons == true && s.Status == false).OrderByDescending(x => x.InsertDate).ToListAsync();
                    break;
            }
            return View(messages);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contact)
        {
            Contact _contact = contact;
            _contact.Status = true;
            _contact.InsertDate = DateTime.Now;
            _contact.ResponseDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(_contact);
                await _context.SaveChangesAsync();
                TempData["MessageSuccess"] = "Mesajınız göndərilmişdir";
                return Redirect("~/");
            }
            return Redirect("~/");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Readed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var message = await _context.Contacts.FirstOrDefaultAsync(x => x.ID == id);
            message.Status = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Unread(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var message = await _context.Contacts.FirstOrDefaultAsync(x => x.ID == id);
            message.Status = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}
