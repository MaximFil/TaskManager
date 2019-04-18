using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using static TaskManager.Enums.Enum;

namespace TaskManager
{
    public static class DefaultStatuses
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Statuses.Any())
            {
                var names = Enum.GetNames(typeof(TaskStatuses));
                context.Statuses.AddRange(names.Select(name =>
                    new Status
                    {
                        Name = name
                    }
                ));
                context.SaveChanges();
            }
        }
    }
}
