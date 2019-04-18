using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
        public int? Progress { get; set; }
    }
}
