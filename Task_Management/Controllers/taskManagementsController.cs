using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Task_Management.Data;
using Task_Management.Models;

namespace Task_Management.Controllers
{
    public class taskManagementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public taskManagementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: taskManagements
        public async Task<IActionResult> Index()
        {
           var taskList= await _context.tasks.ToListAsync();
            return View(taskList);
        }

        // GET: taskManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskManagement = await _context.tasks
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (taskManagement == null)
            {
                return NotFound();
            }

            return View(taskManagement);
        }

        // GET: taskManagements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: taskManagements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,Title,Description,Status,Position,Column")] taskManagement taskManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskManagement);
        }

        // GET: taskManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskManagement = await _context.tasks.FindAsync(id);
            if (taskManagement == null)
            {
                return NotFound();
            }
            return View(taskManagement);
        }

        // POST: taskManagements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,Title,Description,Status,Position,Column")] taskManagement taskManagement)
        {
            if (id != taskManagement.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!taskManagementExists(taskManagement.TaskID))
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
            return View(taskManagement);
        }

        // GET: taskManagements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskManagement = await _context.tasks
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (taskManagement == null)
            {
                return NotFound();
            }

            return View(taskManagement);
        }

        // POST: taskManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskManagement = await _context.tasks.FindAsync(id);
            if (taskManagement != null)
            {
                _context.tasks.Remove(taskManagement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // this method will update the position of the element on the basis of drag and drop
        [HttpPost]
        public async Task<IActionResult> UpdateTaskOrderAndStatus(List<int> taskIds ,string status)
        {
           
            foreach (var taskId in taskIds)
            {
                var task = _context.tasks.FirstOrDefault(t => t.TaskID == taskId);
                if (task != null)
                {
                    task.Status = status;
                    task.Position = taskIds.IndexOf(taskId) + 1;
                    _context.tasks.Update(task);
                    
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
        private bool taskManagementExists(int id)
        {
            return _context.tasks.Any(e => e.TaskID == id);
        }
    }
}
