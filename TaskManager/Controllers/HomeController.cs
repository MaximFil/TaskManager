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
        ApplicationDbContext db = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ModalSelectTask()
        {
            //List<string> tasksList = new List<string>();
            //var tasks = db.taskContext.ToList(); 
            //foreach(var t in tasks)
            //{
            //    tasksList.Add(t.Title);
            //}
            //return View(tasksList);
            var tasks = db.taskContext.Where(p => p.StatusId == 1);
            IEnumerable<Models.Task> freeTasks = tasks;
            ViewBag.Tasks = tasks;
            return View();
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

        public void AddProject(Project project)
        {
            db.projectContext.Add(project);
            db.SaveChanges();
        }
        public void AddTask(Models.Task task)
        {
            db.taskContext.Add(task);
            db.SaveChanges();
        }
        public void AddStatus(Status status)
        {
            db.statusContext.Add(status);
            db.SaveChanges();
        }
        public void UpdateProject(Project project)
        {
            var updateProject = db.projectContext.Where(c => c.Id == project.Id).FirstOrDefault();
            updateProject.Name = project.Name;
            updateProject.DeadLine = project.DeadLine;
            db.SaveChanges();
        }
        public void UpdateTask(Models.Task task)
        {
            var updateTask = db.taskContext.Where(c => c.Id == task.Id).FirstOrDefault();
            updateTask.ProjectId = updateTask.ProjectId;
            updateTask.StatusId = updateTask.StatusId;
            updateTask.Title = updateTask.Title;
            updateTask.Description = updateTask.Description;
            updateTask.StartDate = updateTask.StartDate;
            updateTask.EndDate = updateTask.EndDate;
            db.SaveChanges();
        }
        public void UpdateStatus(Status status)
        {
            var updateStatus = db.statusContext.Where(c => c.Id == status.Id).FirstOrDefault();
            updateStatus.Name = status.Name;
            db.SaveChanges();
        }
        public void DeleteProject(int id)
        {
            var deleteProject = db.projectContext.Where(c => c.Id == id).FirstOrDefault();
            db.projectContext.Remove(deleteProject);
            db.SaveChanges();
        }
        public void DeleteTask(int id)
        {
            var deleteTask = db.taskContext.Where(c => c.Id == id).FirstOrDefault();
            db.taskContext.Remove(deleteTask);
            db.SaveChanges();
        }
        public void DeleteStatus(int id)
        {
            var deleteStatus = db.statusContext.Where(c => c.Id == id).FirstOrDefault();
            db.statusContext.Remove(deleteStatus);
            db.SaveChanges();
        }
    }
}
