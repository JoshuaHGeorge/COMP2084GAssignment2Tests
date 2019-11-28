using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084GAssignment2.Models;
using Microsoft.AspNetCore.Authorization;

namespace COMP2084GAssignment2.Controllers
{
    [Authorize]
    public class HomeworkController : Controller
    {
        private readonly PlannerContext _context;

        public HomeworkController(PlannerContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var plannerContext = _context.Homework.Include(h => h.Assignment).Include(h => h.Course);
            return View("Index", await plannerContext.ToListAsync());
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .Include(h => h.Assignment)
                .Include(h => h.Course)
                .FirstOrDefaultAsync(m => m.HomeworkId == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create()
        {
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "AssignmentId", "Name");
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Name");
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeworkId,CourseId,AssignmentId,Due,Description")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "AssignmentId", "Name", homework.AssignmentId);
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Name", homework.CourseId);
            return View(homework);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "AssignmentId", "Name", homework.AssignmentId);
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Name", homework.CourseId);
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HomeworkId,CourseId,AssignmentId,Due,Description")] Homework homework)
        {
            if (id != homework.HomeworkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.HomeworkId))
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
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "AssignmentId", "Name", homework.AssignmentId);
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Name", homework.CourseId);
            return View(homework);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .Include(h => h.Assignment)
                .Include(h => h.Course)
                .FirstOrDefaultAsync(m => m.HomeworkId == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homework = await _context.Homework.FindAsync(id);
            _context.Homework.Remove(homework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(int id)
        {
            return _context.Homework.Any(e => e.HomeworkId == id);
        }
    }
}
