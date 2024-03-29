﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class UserController : Controller
    {
        private readonly ResumeContext _context;
        public UserController(ResumeContext context)
        {
            _context = context;
        }


        CurrentUser currentUser = new CurrentUser();


        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "User", "Index");
            if (roleStatus)
            {
                return View(await _context.Users.ToListAsync());
            }
            else
            {
                return Redirect("/Account/Denied");
            }
        }

        public IActionResult Create()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "User", "Create");
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
        public async Task<IActionResult> Create([Bind("ID,Email,Password,ConfirmPassword,Status")] User user)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "User", "Create");
            if (roleStatus)
            {
                var users = await _context.Users.SingleOrDefaultAsync(x => x.Email == user.Email);
                if (users != null)
                {
                    TempData["UserError"] = "Bu Email sistemdə qeydiyyatlıdır. Başqa Email adresi istifadə edin.";
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        user.Password = AncryptionAndDecryption.encodedata("encodedata" + AncryptionAndDecryption.encodedata(user.Password));
                        _context.Add(user);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(user);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "User", "Edit");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                var user = await _context.Users.FindAsync(dID);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Parol, string tekrar, string gmail, User user)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "User", "Edit");
            if (roleStatus)
            {
                if (id != user.ID)
                {
                    return NotFound();
                }
                if (gmail != user.Email)
                {
                    var users = await _context.Users.FirstOrDefaultAsync(x => x.Email == gmail);
                    if (users == null)
                    {
                        user.Email = gmail;
                        if (Parol != null)
                        {
                            user.Password.Equals(Parol);
                            user.ConfirmPassword.Equals(tekrar);
                        }
                        else
                        {
                            user.Password = AncryptionAndDecryption.decodedata(AncryptionAndDecryption.decodedata(user.Password).Replace("encodedata", ""));
                            user.ConfirmPassword = user.Password;
                        }

                        try
                        {
                            user.Password = AncryptionAndDecryption.encodedata("encodedata" + AncryptionAndDecryption.encodedata(user.Password));

                            _context.Update(user);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!UserExists(user.ID))
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
                    else
                    {
                        TempData["UserError"] = "Bu Email sistemdə qeydiyyatlıdır. Başqa Email adresi istifadə edin.";
                    }
                }
                else
                {
                    if (Parol != null)
                    {
                        user.Password.Equals(Parol);
                        user.ConfirmPassword.Equals(tekrar);
                    }
                    else
                    {
                        user.Password = AncryptionAndDecryption.decodedata(AncryptionAndDecryption.decodedata(user.Password).Replace("encodedata", ""));
                        user.ConfirmPassword = AncryptionAndDecryption.decodedata(AncryptionAndDecryption.decodedata(user.ConfirmPassword).Replace("encodedata", ""));
                    }

                    try
                    {
                        user.Password = AncryptionAndDecryption.encodedata("encodedata" + AncryptionAndDecryption.encodedata(user.Password));

                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.ID))
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

                return View(user);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
