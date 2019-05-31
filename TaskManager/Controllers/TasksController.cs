using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_userId != null)
            {
                ViewBag.check = true;
            }
            else { ViewBag.check = false; }
            var applicationDbContext = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }
        //Get:Tasks/UserIndex
        public IActionResult UserIndex()
        {
            IQueryable<TaskManager.Models.Task> tasks;
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_userId != null)
            {
                tasks = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.UserId == _userId);
            }
            else
            {
                tasks = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.User.UserName == "Unassigned");
            }
            return View(tasks);
            //var applicationDbContext = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Status)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["ProjectName"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["StatusName"] = new SelectList(_context.Statuses, "Id", "Name");
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName").Where(t => t.Text != "Admin@gmail.com").ToList();
            var variable = ViewData["UserName"];
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProjectId,StatusId,UserId")] Models.Task task)
        {
            //var variable = task.Status.Name;
            //var list = _context.Projects.Where(t=>t.Name=="Auction").Last().Id.ToString();
            //StreamWriter writer = new StreamWriter("D:\\file.txt",true);
            //writer.WriteLine(list);
            //writer.Close();
            //return RedirectToAction(nameof(Index));

            task.StartDate = DateTime.Now.Date;
            //   task.EndDate = Convert.ToDateTime(null); 
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", task.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", task.UserId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            //ViewBag.ProjectName = _context.Projects.Last().Name;
            //ViewBag.StatusName = _context.Statuses.Last().Name;
            //ViewBag.UserName = _context.Users.Last().UserName;
            //var prlast=_context.Projects.Last().Id.ToString();
            //var stlast = _context.Statuses.Last().Id.ToString();
            //var uslast = _context.Users.Last().Id.ToString();
            ViewData["ProjectName"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            ViewData["StatusName"] = new SelectList(_context.Statuses, "Id", "Name", task.StatusId);
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName", task.UserId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,StartDate,EndDate,ProjectId,StatusId,UserId")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", task.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", task.UserId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Status)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<RedirectResult> Finish(int? id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            task.StatusId = 3;
            task.EndDate = DateTime.Now.Date;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return RedirectPermanent("/Tasks/UserIndex");
        }

        [HttpGet]
        public IActionResult ProgressUserIndex()
        {
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.UserId == _userId).Where(t => t.StatusId == 2);
            return View(tasks);
        }

        public IActionResult NotStartedUserTasks()
        {
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.UserId == _userId).Where(t => t.StatusId == 1);
            return View(tasks);
        }

        public IActionResult FreeAllTasks()
        {
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.StatusId == 1);
            return View(tasks);
        }

        public IActionResult FreeTasks()
        {
            var tasks = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.User.UserName == "Unassigned");
            return View(tasks);
        }

        public IActionResult Appoint()
        {
            ViewData["UserName"] = new SelectList(_context.Users.Where(t => (t.UserName != "Unassigned" && t.UserName != "Admin@gmail.com")), "Id", "UserName");
            return View();
        }
        [HttpPost]
        public RedirectResult Appoint(Models.Task task)
        {
            return RedirectPermanent("/Tasks/FreeAllTasks");
        }

        public RedirectResult Start(int? id)
        {
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = _context.Tasks.FirstOrDefault(t=>t.Id==id);
            task.UserId = _userId;
            task.StatusId = 2;
            _context.Tasks.Update(task);
            _context.SaveChanges();
            return RedirectPermanent("/Tasks/FreeAllTasks");
        }
        public IActionResult InProgressTasks()
        {
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _context.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.StatusId!=3 && t.StatusId!=1 && t.UserId==_userId);
            return View(tasks);
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        //пользователь добавляет задачу
        public IActionResult UserCreate()
        {
            string _userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userProject = _context.Tasks.Where(t => t.User.Id == _userId).ToList();
            ViewData["ProjectName"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["StatusName"] = new SelectList(_context.Statuses, "Id", "Name");
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName").Where(t => t.Text == "Unassigned").ToList();
            var variable = ViewData["UserName"];
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate([Bind("Id,Title,Description,ProjectId,StatusId,UserId")] Models.Task task)
        {
            //var variable = task.Status.Name;
            //var list = _context.Projects.Where(t=>t.Name=="Auction").Last().Id.ToString();
            //StreamWriter writer = new StreamWriter("D:\\file.txt",true);
            //writer.WriteLine(list);
            //writer.Close();
            //return RedirectToAction(nameof(Index));

            task.StartDate = DateTime.Now.Date;
            //   task.EndDate = Convert.ToDateTime(null); 
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", task.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", task.UserId);
            return View(task);
        }
    }
}
