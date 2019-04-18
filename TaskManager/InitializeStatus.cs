using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager
{
    public static class InitializeStatus
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(
                    new Status
                    {
                        Name = ItemsStatus.NotStarted.ToString()
                    },
                    new Status
                    {
                        Name = ItemsStatus.InProgress.ToString()
                    },
                    new Status
                    {
                        Name = ItemsStatus.Completed.ToString()
                    }
                ); ;
                context.SaveChanges();
            }
        }
    }

    public enum ItemsStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}
