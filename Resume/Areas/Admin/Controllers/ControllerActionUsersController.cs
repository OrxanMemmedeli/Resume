using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

            var controllerActionUser = await _context.ControllerActionUsers.Where(x => x.UserID == userID).ToListAsync();
            if (controllerActionUser == null)
            {
                return NotFound();
            }
            ViewData["ControllerID"] = _context.ControllerNames.ToList();
            ViewBag.userID = userID;
            return View(controllerActionUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int UserID, int[] controllerID)
        {
            List<ControllerActionUser> listRemove = new List<ControllerActionUser>();
            List<ControllerActionUser> listCreate = new List<ControllerActionUser>();
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

            if (ModelState.IsValid)
            {
                await _context.AddRangeAsync(listCreate);
                _context.RemoveRange(listRemove);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ControllerID"] = _context.ControllerNames.ToList();
            ViewBag.userID = UserID;
            return View();
        }


        private bool ControllerActionUserExists(int id)
        {
            return _context.ControllerActionUsers.Any(e => e.ControllerID == id);
        }
    }
}
