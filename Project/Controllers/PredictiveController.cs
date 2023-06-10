using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Models.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.Controllers
{
    public class PredictiveController : Controller
    {
        private readonly ILogger<PredictiveController> _logger;
        private readonly string _filePath = "wwwroot/data/predictive.csv"; // đường dẫn đến file csv

        public PredictiveController(ILogger<PredictiveController> logger)
        {
            _logger = logger;
        }

        // GET: Predictive
        public IActionResult Index(int? page = 1)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int totalLines = System.IO.File.ReadLines(_filePath).Count() - 1;
            int totalPages = (totalLines % pageSize == 0) ? totalLines / pageSize : (totalLines / pageSize + 1);
            ViewData["page"] = pageNumber;
            ViewData["totalPage"] = totalPages;

            List<Predictive> predictives = new List<Predictive>();
            using (var reader = new StreamReader(_filePath))
            {
                int skipLines = (pageNumber - 1) * pageSize + 1;
                int count = 0;
                while (!reader.EndOfStream && count < pageSize)
                {
                    if (skipLines > 0)
                    {
                        reader.ReadLine();
                        skipLines--;
                        continue;
                    }

                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    predictives.Add(new Predictive
                    {
                        UDI = int.Parse(values[0]),
                        ProductId = values[1],
                        Type = values[2],
                        AirTemperature = float.Parse(values[3]),
                        ProcessTemperature = float.Parse(values[4]),
                        RotationalSpeed = float.Parse(values[5]),
                        Torque = float.Parse(values[6]),
                        ToolWear = float.Parse(values[7]),
                        Target = float.Parse(values[8]),
                        FailureType = values[9]
                    });

                    count++;
                }
            }

            return View(predictives);
        }

        // GET: Predictive/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Predictive/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Predictive predictive)
        {
            if (ModelState.IsValid)
            {
                int maxUDI = 0;
                using (var reader = new StreamReader(_filePath))
                {
                    var line1 = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        int udi = int.Parse(values[0]);
                        if (udi > maxUDI)
                        {
                            maxUDI = udi;
                        }
                    }
                }
                predictive.UDI = maxUDI + 1;
                var lines = new List<string>();
                using (var reader = new StreamReader(_filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
                lines.Add($"{predictive.UDI},{predictive.ProductId},{predictive.Type},{predictive.AirTemperature},{predictive.ProcessTemperature},{predictive.RotationalSpeed},{predictive.Torque},{predictive.ToolWear},{predictive.Target},{predictive.FailureType}");
                System.IO.File.WriteAllLines(_filePath, lines);
                return RedirectToAction(nameof(Index));
            }
            return View(predictive);
        }
        // GET: Predictive/Details/5
        public IActionResult Details(int id)
        {
            var predictive = GetPredictiveById(id);
            if (predictive == null)
            {
                return NotFound();
            }
            return View(predictive);
        }

        // GET: Predictive/Delete/5
        public IActionResult Delete(int id)
        {
            var predictive = GetPredictiveById(id);
            if (predictive == null)
            {
                return NotFound();
            }
            return View(predictive);
        }

        // POST: Predictive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(_filePath))
            {
                var line1 = reader.ReadLine();
                lines.Add(line1);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (int.Parse(values[0]) != id)
                    {
                        lines.Add(line);
                    }
                }
            }

            System.IO.File.WriteAllLines(_filePath, lines);
            return RedirectToAction(nameof(Index));
        }

        // GET: Predictive/Edit/5
        public IActionResult Edit(int id)
        {
            var predictive = GetPredictiveById(id);
            if (predictive == null)
            {
                return NotFound();
            }
            return View(predictive);
        }

        // POST: Predictive/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Predictive predictive)
        {
            if (id != predictive.UDI)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var lines = new List<string>();
                using (var reader = new StreamReader(_filePath))
                {
                    var line1 = reader.ReadLine();
                    lines.Add(line1);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (int.Parse(values[0]) == id)
                        {
                            lines.Add($"{predictive.UDI},{predictive.ProductId},{predictive.Type},{predictive.AirTemperature},{predictive.ProcessTemperature},{predictive.RotationalSpeed},{predictive.Torque},{predictive.ToolWear},{predictive.Target},{predictive.FailureType}");
                        }
                        else
                        {
                            lines.Add(line);
                        }
                    }
                }

                System.IO.File.WriteAllLines(_filePath, lines);
                return RedirectToAction(nameof(Index));
            }
            return View(predictive);
        }

        private Predictive GetPredictiveById(int id)
        {
            using (var reader = new StreamReader(_filePath))
            {
                var line1 = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (int.Parse(values[0]) == id)
                    {
                        return new Predictive
                        {
                            UDI = int.Parse(values[0]),
                            ProductId = values[1],
                            Type = values[2],
                            AirTemperature = float.Parse(values[3]),
                            ProcessTemperature = float.Parse(values[4]),
                            RotationalSpeed = float.Parse(values[5]),
                            Torque = float.Parse(values[6]),
                            ToolWear = float.Parse(values[7]),
                            Target = float.Parse(values[8]),
                            FailureType = values[9]
                        };
                    }
                }
            }
            return null;
        }
    }
}