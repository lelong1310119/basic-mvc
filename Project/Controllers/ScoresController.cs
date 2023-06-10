using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;

namespace Project.Controllers
{
    public class ScoresController : Controller
    {
        private readonly ManageContext _context;

        public ScoresController(ManageContext context)
        {
            _context = context;
        }

        // GET: Scores
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") is null)
            {
                return RedirectToAction("Index", "Login");
                // session không tồn tại, thực hiện các xử lý cần thiết ở đây
            }
            var manageContext = _context.Scores.Include(s => s.Student).Include(s => s.SubjectClass);
            return View(await manageContext.ToListAsync());
        }

        // GET: Scores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scores == null)
            {
                return NotFound();
            }

            var score = await _context.Scores
                .Include(s => s.Student)
                .Include(s => s.SubjectClass)
                .FirstOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // GET: Scores/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["SubjectClassId"] = new SelectList(_context.SubjectClasses, "SubjectClassId", "SubjectClassId");
            return View();
        }

        // POST: Scores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,SubjectClassId,ScoreSubject,Date")] Score score)
        {
            if (ModelState.IsValid)
            {
                _context.Add(score);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", score.StudentId);
            ViewData["SubjectClassId"] = new SelectList(_context.SubjectClasses, "SubjectClassId", "SubjectClassId", score.SubjectClassId);
            return View(score);
        }

        // GET: Scores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scores == null)
            {
                return NotFound();
            }

            var score = await _context.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", score.StudentId);
            ViewData["SubjectClassId"] = new SelectList(_context.SubjectClasses, "SubjectClassId", "SubjectClassId", score.SubjectClassId);
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreId,StudentId,SubjectClassId,ScoreSubject,Date")] Score score)
        {
            if (id != score.ScoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(score.ScoreId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", score.StudentId);
            ViewData["SubjectClassId"] = new SelectList(_context.SubjectClasses, "SubjectClassId", "SubjectClassId", score.SubjectClassId);
            return View(score);
        }

        // GET: Scores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Scores == null)
            {
                return NotFound();
            }

            var score = await _context.Scores
                .Include(s => s.Student)
                .Include(s => s.SubjectClass)
                .FirstOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Scores == null)
            {
                return Problem("Entity set 'ManageContext.Scores'  is null.");
            }
            var score = await _context.Scores.FindAsync(id);
            if (score != null)
            {
                _context.Scores.Remove(score);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(int id)
        {
          return (_context.Scores?.Any(e => e.ScoreId == id)).GetValueOrDefault();
        }
    }
}
