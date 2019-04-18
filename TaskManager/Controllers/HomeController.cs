using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
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
            //List<string> tasksList = new List<string>();
            //var tasks = db.Tasks.ToList(); 
            //foreach(var t in tasks)
            //{
            //    tasksList.Add(t.Title);
            //}
            //return View(tasksList);
            IEnumerable<Models.Task> Tasks = db.Tasks.Where(task => task.Status.Name == ItemsStatus.NotStarted.ToString());
            return View(Tasks);
        }
        [HttpPost]
        public RedirectResult ModalSelectTask(int id=2)
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
