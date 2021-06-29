using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Resume.Business.Control;
using Resume.Business.Tools;
using Resume.Models;
using Resume.Models.Context;
using Resume.Models.Entities;
using Resume.Models.ViewModels;

namespace Resume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly ResumeContext _context;
        private readonly GoogleConfigModel _googleConfig;
        CurrentUser currentUser = new CurrentUser();
        public ContactController(ResumeContext context, IOptions<GoogleConfigModel> googleConfig)
        {
            _context = context;
            _googleConfig = googleConfig.Value;
        }

        public async Task<IActionResult> Index(string category)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "Index");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "Details");
            if (roleStatus)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.ID == id);
                contact.Status = false;
                await _context.SaveChangesAsync();
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contact)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "Create");
            if (roleStatus)
            {
                var isValid = IsReCaptchValidV3(contact.captcha);
                contact = contact.ProtectForSQLInjection(contact);

                if (isValid)
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
                }

                return Redirect("~/");
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool IsReCaptchValidV3(string captchaResponse)
        {
            var result = false;
            var secretKey = _googleConfig.Secret;
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "Delete");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "Delete");
            if (roleStatus)
            {
                var contact = await _context.Contacts.FindAsync(id);
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Readed(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "Readed");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Unread(int? id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "Unread");
            if (roleStatus)
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
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(int messageID, ContactViewModel model)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "Contact", "SendEmail");
            if (roleStatus)
            {
                var email = await _context.EmailConfigs.FirstOrDefaultAsync();
                email.Password = AncryptionAndDecryption.decodedata(AncryptionAndDecryption.decodedata(email.Password).Replace("encodedata", ""));

                MailMessage message = new MailMessage(new MailAddress(email.Gmail.Trim(), email.Gmail.Trim()), new MailAddress(model.Email.Trim()));
                message.IsBodyHtml = true;
                message.Subject = model.Subject;
                message.Body = model.Message;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryFormat = SmtpDeliveryFormat.SevenBit,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email.Gmail.Trim(), email.Password.Trim())
                };

                try
                {
                    smtp.Send(message);
                    TempData["Message"] = "Mesaj göndərildi";
                    var mainMessage = await _context.Contacts.FindAsync(messageID);
                    mainMessage.Respons = true;
                    mainMessage.ResponseDate = DateTime.Now;
                    mainMessage.ResponsMessage = model.Message;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Mesaj göndərilmədi. Xəta mətni: " + ex;
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}
