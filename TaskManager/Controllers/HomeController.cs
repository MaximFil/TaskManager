using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;
using static TaskManager.Enums.Enum;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ModalSelectTask()
        {
            IEnumerable<Models.Task> Tasks = db.Tasks
                .Where(task => task.Status.Name == TaskStatuses.NotStarted.ToString());
            return View(Tasks);
        }
        [HttpPost]
        public RedirectResult ModalSelectTask(int id)
        {
            return RedirectPermanent("/Home/Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
