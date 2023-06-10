using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Models.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Project.Controllers
{
    public class MachineController : Controller
    {
        private readonly ILogger<MachineController> _logger;
        private readonly string _filePath = "wwwroot/data/machinedata.csv"; // đường dẫn đến file csv

        public MachineController(ILogger<MachineController> logger)
        {
            _logger = logger;
        }

        // GET: Machine
        public IActionResult Index(int? page = 1)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;
            int totalLines = System.IO.File.ReadLines(_filePath).Count() - 1;
            int totalPages = (totalLines % pageSize == 0) ? totalLines / pageSize : (totalLines / pageSize + 1);
            ViewData["page"] = pageNumber;
            ViewData["totalPage"] = totalPages;

            List<Machine> Machines = new List<Machine>();
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
                    Machines.Add(new Machine
                    {
                        Date = DateTime.ParseExact(values[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Location = int.Parse(values[1]),
                        MinTemp = values[2],
                        MaxTemp = values[3],
                        Leakage = values[4],
                        Evaporation = values[5],
                        Electricity = values[6],
                        Parameter1Dir = values[7],
                        Parameter1Speed = values[8],
                        Parameter2_9am = values[9],
                        Parameter2_3pm = values[10],
                        Parameter3_9am = values[11],
                        Parameter3_3pm = values[12],
                        Parameter4_9am = values[13],
                        Parameter4_3pm = values[14],
                        Parameter5_9am = values[15],
                        Parameter5_3pm = values[16],
                        Parameter6_9am = values[17],
                        Parameter6_3pm = values[18],
                        Parameter7_9am = values[19],
                        Parameter7_3pm = values[20],
                        Failure_today = values[21],
                        RISK_MM = float.Parse(values[22]),
                        Fail_tomorrow = values[23],
                    });
                    count++;
                }
            }
            return View(Machines);
            //using (var dbContext = new ManageContext())
            //{
            //    dbContext.Machines.AddRange(Machines);
            //    dbContext.SaveChanges();
            //}

            //using (var dbContext = new ManageContext())
            //{
            //    var machines = dbContext.Machines.Take(10).ToList();
            //    return View(machines);
            //}
        }

        // GET: Machine/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Machine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Machine Machine)
        {
            if (ModelState.IsValid)
            {
                var lines = new List<string>();
                using (var reader = new StreamReader(_filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }

                lines.Add($"{Machine.Date.ToString("yyyy-MM-dd")},{Machine.Location},{Machine.MinTemp},{Machine.MaxTemp},{Machine.Leakage},{Machine.Evaporation},{Machine.Electricity},{Machine.Parameter1Dir},{Machine.Parameter1Speed}," +
                    $"{Machine.Parameter2_9am},{Machine.Parameter2_3pm}," +
                    $"{Machine.Parameter3_9am},{Machine.Parameter3_3pm}," +
                    $"{Machine.Parameter4_9am},{Machine.Parameter4_3pm}," +
                    $"{Machine.Parameter5_9am},{Machine.Parameter5_3pm}," +
                    $"{Machine.Parameter6_9am},{Machine.Parameter6_3pm}," +
                    $"{Machine.Parameter7_9am},{Machine.Parameter7_3pm},{Machine.Failure_today}, {Machine.RISK_MM}, {Machine.Fail_tomorrow}");

                System.IO.File.WriteAllLines(_filePath, lines);
                return RedirectToAction(nameof(Index));
            }
            return View(Machine);
        }
    }
}

