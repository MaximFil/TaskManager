using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaskManager.Data;
using TaskManager.Models;
using Xceed.Wpf.Toolkit;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            CreateWebHostBuilder(args).Build().Run();
            Console.ReadLine();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
