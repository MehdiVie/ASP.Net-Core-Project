using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarayeAzadi.Application.Common.Interfaces;
using BarayeAzadi.Application.Common.Utility;
using BarayeAzadi.Domain.Entities;

namespace BarayeAzadi.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();

                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "admin@mehdi.com",
                        Email = "admin@mehdi.com",
                        Name = "Mehdi Salimi",
                        NormalizedUserName = "ADMIN@MEHDI.COM",
                        NormalizedEmail = "ADMIN@MEHDI.COM",
                        PhoneNumber = "123432"
                    },"Admin@123");

                    ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u=>u.Email== "admin@mehdi.com");
                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
