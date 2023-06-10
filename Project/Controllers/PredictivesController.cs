using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Models.Entities;

namespace Project.Controllers
{
    public class PredictivesController : Controller
    {
        private readonly ManageContext _context;
        private readonly ILogger<PredictiveController> _logger;
        private readonly string _filePath = "wwwroot/data/predictive.csv"; // đường dẫn đến file csv

        public PredictivesController(ManageContext context, ILogger<PredictiveController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Predictives
        public async Task<IActionResult> Index(int? page = 1)
        {
            //List<Predictive> predictives = new List<Predictive>();
            //using (var reader = new StreamReader(_filePath))
            //{
            //    var line1 = reader.ReadLine();
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var values = line.Split(',');
            //        var predictive = new Predictive
            //        {
            //            UDI = int.Parse(values[0]),
            //            ProductId = values[1],
            //            Type = values[2],
            //            AirTemperature = string.IsNullOrEmpty(values[3]) ? null : (float?)float.Parse(values[3]),
            //            ProcessTemperature = string.IsNullOrEmpty(values[4]) ? null : (float?)float.Parse(values[4]),
            //            RotationalSpeed = string.IsNullOrEmpty(values[5]) ? null : (float?)float.Parse(values[5]),
            //            Torque = string.IsNullOrEmpty(values[6]) ? null : (float?)float.Parse(values[6]),
            //            ToolWear = string.IsNullOrEmpty(values[7]) ? null : (float?)float.Parse(values[7]),
            //            Target = string.IsNullOrEmpty(values[8]) ? null : (float?)float.Parse(values[8]),
            //            FailureType = values[9]
            //        };
            //        predictives.Add(predictive);
            //    }
            //}
            //using (var dbContext = new ManageContext())
            //{
            //    dbContext.Predictives.AddRange(predictives);
            //    dbContext.SaveChanges();
            //}
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            int count = await _context.Predictives.CountAsync();
            int totalPage = (count % pageSize == 0) ? (count / pageSize) : (count / pageSize + 1);
            ViewData["page"] = pageNumber;
            ViewData["totalPage"] = totalPage;

            var predictives = await _context.Predictives
                .OrderBy(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(predictives);
        }

        // GET: Predictives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Predictives == null)
            {
                return NotFound();
            }

            var predictive = await _context.Predictives
                .FirstOrDefaultAsync(m => m.Id == id);
            if (predictive == null)
            {
                return NotFound();
            }

            return View(predictive);
        }

        // GET: Predictives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Predictives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UDI,ProductId,Type,AirTemperature,ProcessTemperature,RotationalSpeed,Torque,ToolWear,Target,FailureType")] Predictive predictive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(predictive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(predictive);
        }

        // GET: Predictives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Predictives == null)
            {
                return NotFound();
            }

            var predictive = await _context.Predictives.FindAsync(id);
            if (predictive == null)
            {
                return NotFound();
            }
            return View(predictive);
        }

        // POST: Predictives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UDI,ProductId,Type,AirTemperature,ProcessTemperature,RotationalSpeed,Torque,ToolWear,Target,FailureType")] Predictive predictive)
        {
            if (id != predictive.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(predictive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredictiveExists(predictive.Id))
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
            return View(predictive);
        }

        // GET: Predictives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Predictives == null)
            {
                return NotFound();
            }

            var predictive = await _context.Predictives
                .FirstOrDefaultAsync(m => m.Id == id);
            if (predictive == null)
            {
                return NotFound();
            }

            return View(predictive);
        }

        // POST: Predictives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Predictives == null)
            {
                return Problem("Entity set 'ManageContext.Predictives'  is null.");
            }
            var predictive = await _context.Predictives.FindAsync(id);
            if (predictive != null)
            {
                _context.Predictives.Remove(predictive);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredictiveExists(int id)
        {
          return (_context.Predictives?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
