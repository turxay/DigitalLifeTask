using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTech.Areas.AdminDigitalLife.Models;
using TaskTech.Models;

namespace TaskTech.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Projects> Projeler { get; set; }
        public DbSet<Invoice> Invoicess { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
