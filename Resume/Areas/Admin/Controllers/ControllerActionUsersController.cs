using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume.Areas.Admin.Models.ViewModels;
using Resume.Models.Context;
using Resume.Models.Entities;

namespace Resume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ControllerActionUsersController : Controller
    {
        private readonly ResumeContext _context;

        public ControllerActionUsersController(ResumeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> Edit(int? userID)
        {
            if (userID == null)
            {
                return NotFound();
            }
            ViewBag.userID = userID;
            ActionCotrollerUserRelationship relationship = new ActionCotrollerUserRelationship();
            relationship.controllerActionUsers = await _context.ControllerActionUsers.Where(x => x.UserID == userID).ToListAsync();
            relationship.actionUsers = await _context.ActionUsers.Where(x => x.UserID == userID).ToListAsync();
            if (relationship == null)
            {
                return NotFound();
            }
            ViewData["ControllerID"] = await _context.ControllerNames.ToListAsync();
            ViewData["ActionID"] = await _context.ActionNames.ToListAsync();
            ViewBag.userID = userID;
            return View(relationship);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int UserID, int[] controllerID, int[] actionID)
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
