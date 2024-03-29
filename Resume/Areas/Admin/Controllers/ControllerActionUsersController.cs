﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Areas.Admin.Models.ViewModels;
using Resume.Business.Control;
using Resume.Business.Tools;
using Resume.Models.Context;
using Resume.Models.Entities;

namespace Resume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ControllerActionUsersController : Controller
    {
        private readonly ResumeContext _context;
        CurrentUser currentUser = new CurrentUser();
        public ControllerActionUsersController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerActionUsers", "Index");
            if (roleStatus)
            {
                List<int> idList = new List<int>();
                var resumeContext = await _context.Users.Include(c => c.ControllerActionUsers).ToListAsync();
                var control = await _context.ControllerActionUsers.ToArrayAsync();
                foreach (var item in resumeContext)
                {
                    if (control.FirstOrDefault(x => x.UserID == item.ID) != null)
                    {
                        idList.Add(item.ID);
                    }
                }
                ViewBag.List = idList;
                return View(resumeContext);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        public async Task<IActionResult> Edit(string id)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "ControllerActionUsers", "Index");
            if (roleStatus)
            {
                if (id == null || IDAncryption.Decrypt(id) == "NotFound")
                {
                    return NotFound();
                }
                int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

                ActionCotrollerUserRelationship relationship = new ActionCotrollerUserRelationship();
                relationship.controllerActionUsers = await _context.ControllerActionUsers.Where(x => x.UserID == dID).ToListAsync();
                relationship.actionUsers = await _context.ActionUsers.Where(x => x.UserID == dID).ToListAsync();
                if (relationship == null)
                {
                    return NotFound();
                }
                ViewData["ControllerID"] = await _context.ControllerNames.ToListAsync();
                ViewData["ActionID"] = await _context.ActionNames.ToListAsync();
                ViewBag.userID = dID;
                return View(relationship);
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int UserID, int[] controllerID, int[] actionID)
        {
            bool roleStatus = RoleChecker.AuthorizeRoles(_context, currentUser.FindUser(_context, User.Identity.Name), "User", "Index");
            if (roleStatus)
            {
                List<ActionUser> ListCreateAction = new List<ActionUser>();
                List<ActionUser> ListRemoveAction = new List<ActionUser>();

                List<ControllerActionUser> listRemove = new List<ControllerActionUser>();
                List<ControllerActionUser> listCreate = new List<ControllerActionUser>();

                await CotrollerUserRelationship(UserID, controllerID, listRemove, listCreate);

                await ActionUserRelationship(UserID, actionID, ListCreateAction, ListRemoveAction);

                if (ModelState.IsValid)
                {
                    await _context.AddRangeAsync(listCreate);
                    await _context.AddRangeAsync(ListCreateAction);
                    _context.RemoveRange(listRemove);
                    _context.RemoveRange(ListRemoveAction);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                ViewData["ControllerID"] = await _context.ControllerNames.ToListAsync();
                ViewData["ActionID"] = await _context.ActionNames.ToListAsync();
                ViewBag.userID = UserID;
                return View();
            }
            else
            {
                return Redirect("/Account/Denied");
            }

        }

        private async Task ActionUserRelationship(int UserID, int[] actionID, List<ActionUser> ListCreateAction, List<ActionUser> ListRemoveAction)
        {
            var control = await _context.ActionUsers.Where(x => x.UserID == UserID).ToListAsync();

            foreach (var item in actionID)
            {
                ActionUser actionUser = new ActionUser();
                actionUser.UserID = UserID;
                actionUser.ActionID = item;
                if (control.FirstOrDefault(x => x.ActionID == actionUser.ActionID) == null)
                {
                    ListCreateAction.Add(actionUser);
                }

            }

            foreach (var item in control)
            {
                if (!actionID.Contains(item.ActionID))
                {
                    ListRemoveAction.Add(item);
                }
            }
        }

        private async Task CotrollerUserRelationship(int UserID, int[] controllerID, List<ControllerActionUser> listRemove, List<ControllerActionUser> listCreate)
        {
            var control = await _context.ControllerActionUsers.Where(x => x.UserID == UserID).ToListAsync();

            foreach (var item in controllerID)
            {
                ControllerActionUser controllerActionUser = new ControllerActionUser();
                controllerActionUser.UserID = UserID;
                controllerActionUser.ControllerID = item;
                if (control.FirstOrDefault(x => x.ControllerID == controllerActionUser.ControllerID) == null)
                {
                    listCreate.Add(controllerActionUser);
                }
            }

            foreach (var item in control)
            {
                if (!controllerID.Contains(item.ControllerID))
                {
                    listRemove.Add(item);
                }
            }
        }

        private bool ControllerActionUserExists(int id)
        {
            return _context.ControllerActionUsers.Any(e => e.ControllerID == id);
        }
    }
}
