using FamilyTasks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTasks.Controllers
{
    public class TaskController : Controller
    {
        private readonly FamilyTaskContext _dbContext;
        public TaskController(FamilyTaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask(Guid? id)
        {
            ViewBag.Members = await _dbContext.Members.OrderBy(m=>m.FirstName).ToListAsync();
            if (id != null)
            {
                ViewData["TaskDetail"] = await _dbContext.Tasks.Include(t=>t.AssignedMember).Where(t => t.AssignedMemberId == id).OrderByDescending(t=>t.Id).ToListAsync();
            }
            else
            {
                ViewData["TaskDetail"] = await _dbContext.Tasks.Include(t => t.AssignedMember).OrderByDescending(t => t.Id).ToListAsync();
               
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(Models.Task task)
        {
            if(ModelState.IsValid)
            {
                task.IsComplete = false;
                await _dbContext.Tasks.AddAsync(task);
                await _dbContext.SaveChangesAsync();
            }
            TempData["Error"] = "false";
            TempData["Message"] = "Task has been added Successfully!";
            return RedirectToAction(nameof(CreateTask));
        }
      
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
             _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            TempData["Error"] = "false";
            TempData["Message"] = "Task Deleted Successfully!";
            return RedirectToAction(nameof(CreateTask));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleCompleteStatus(Guid taskId)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);
            if (task.IsComplete == true)
            {
                task.IsComplete = false;
            }
            else
            {
                task.IsComplete = true;
            }   
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> AssignTask()
        {
            ViewBag.Members = await _dbContext.Members.OrderBy(m => m.FirstName).ToListAsync();
            ViewBag.MembersNames = new SelectList(await _dbContext.Members.ToListAsync(), "Id", "FirstName");
            ViewBag.TaskLists = new MultiSelectList(await _dbContext.Tasks.Where(t=>t.AssignedMemberId==null).ToListAsync(), "Id", "Subject");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignTask(Guid MemberId, Guid[] TaskIds)
        {
            if (TaskIds.Count()!=0)
            {
                foreach (var taskId in TaskIds)
                {
                    var task= await _dbContext.Tasks.FindAsync(taskId);
                    task.AssignedMemberId = MemberId;
                    _dbContext.Tasks.Update(task);
                }
                await _dbContext.SaveChangesAsync();
            }
            TempData["Error"] = "false";
            TempData["Message"] = "Task has been Assigned!";
            return RedirectToAction(nameof(AssignTask));
        }

    }
}
