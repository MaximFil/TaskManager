using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;
        public UserController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = dbContext;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<TaskManager.Models.Task> tasks;
            if (userId != null)
            {
                tasks = _db.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.UserId == userId);
            }
            else
            {
                tasks = _db.Tasks.Include(t => t.Project).Include(t => t.Status).Include(t => t.User).Where(t => t.User.UserName== "Unassigned");
            }
            return View(tasks);
        }
    }
}
