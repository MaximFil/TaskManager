using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
