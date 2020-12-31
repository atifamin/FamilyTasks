using FamilyTasks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTasks.Controllers
{
    
    public class MemberController : Controller
    {
        private readonly FamilyTaskContext _dbContext;
        public MemberController (FamilyTaskContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        [HttpGet]
        public IActionResult AddMember()
        {
            ViewBag.Members = _dbContext.Members.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMember(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _dbContext.Members.AddAsync(member);
                    await _dbContext.SaveChangesAsync();
                    TempData["Error"] = "false";
                    TempData["Message"] = "Record Created.";
                }
                else
                {
                    TempData["Error"] = "true";
                    TempData["Message"] = "Invalid Data";
                }
                return RedirectToAction(nameof(AddMember));
            }
            catch(Exception exc)
            {
                TempData["Error"] = "true";
                TempData["Message"] = exc;
                return RedirectToAction(nameof(AddMember));
            }
        }

    }
}
