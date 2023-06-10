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
    public class SubjectClassesController : Controller
    {
        private readonly ManageContext _context;

        public SubjectClassesController(ManageContext context)
        {
            _context = context;
        }

        // GET: SubjectClasses
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") is null)
            {
                return RedirectToAction("Index", "Login");
                // session không tồn tại, thực hiện các xử lý cần thiết ở đây
            }
            var manageContext = _context.SubjectClasses.Include(s => s.Lecturer).Include(s => s.Subject);
            return View(await manageContext.ToListAsync());
        }

        // GET: SubjectClasses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SubjectClasses == null)
            {
                return NotFound();
            }

            var subjectClass = await _context.SubjectClasses
                .Include(s => s.Lecturer)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.SubjectClassId == id);
            if (subjectClass == null)
            {
                return NotFound();
            }
            // Lấy danh sách Score có SubjectClassId bằng id
            var scores = await _context.Scores
                .Include(s => s.Student)
                .Where(s => s.SubjectClassId == id)
                .ToListAsync();

            // Thêm danh sách Score vào ViewData để hiển thị trong View
            ViewData["Scores"] = scores;

            return View(subjectClass);
        }

        // GET: SubjectClasses/Create
        public IActionResult Create()
        {
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: SubjectClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectClassId,SubjectId,LecturerId")] SubjectClass subjectClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", subjectClass.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", subjectClass.SubjectId);
            return View(subjectClass);
        }

        // GET: SubjectClasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SubjectClasses == null)
            {
                return NotFound();
            }

            var subjectClass = await _context.SubjectClasses.FindAsync(id);
            if (subjectClass == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", subjectClass.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", subjectClass.SubjectId);
            var scores = await _context.Scores
                .Include(s => s.Student)
                .Where(s => s.SubjectClassId == id)
                .ToListAsync();

            // Thêm danh sách Score vào ViewData để hiển thị trong View
            ViewData["Scores"] = scores;
            return View(subjectClass);
        }

        // POST: SubjectClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SubjectClassId,SubjectId,LecturerId")] SubjectClass subjectClass)
        {
            if (id != subjectClass.SubjectClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectClassExists(subjectClass.SubjectClassId))
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
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "LecturerId", "LecturerId", subjectClass.LecturerId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", subjectClass.SubjectId);
            return View(subjectClass);
        }

        // GET: SubjectClasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SubjectClasses == null)
            {
                return NotFound();
            }

            var subjectClass = await _context.SubjectClasses
                .Include(s => s.Lecturer)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.SubjectClassId == id);
            if (subjectClass == null)
            {
                return NotFound();
            }

            return View(subjectClass);
        }

        // POST: SubjectClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SubjectClasses == null)
            {
                return Problem("Entity set 'ManageContext.SubjectClasses'  is null.");
            }
            var subjectClass = await _context.SubjectClasses.FindAsync(id);
            if (subjectClass != null)
            {
                _context.SubjectClasses.Remove(subjectClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectClassExists(string id)
        {
          return (_context.SubjectClasses?.Any(e => e.SubjectClassId == id)).GetValueOrDefault();
        }
    }
}
