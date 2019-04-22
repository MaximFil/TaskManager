using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace TaskManager
{
    public static class DefaultUser
    {
        public static void InitializeUser(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var user = new IdentityUser { Email = "Unassigned", UserName = "Unassigned" };
            userManager.CreateAsync(user, "1234.Qwer");
        }
    }
}
