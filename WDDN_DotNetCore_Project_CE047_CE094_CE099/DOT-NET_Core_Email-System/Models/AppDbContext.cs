using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOT_NET_Core_Email_System.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_Core_Email_System.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }
        public DbSet<DbUser> User { get; set; }
        public DbSet<DbEmail> Emails { get; set; }
        public DbSet<SignUpViewModel> SignUpViewModel { get; set; }
        public DbSet<LoginViewModel> LoginViewModel { get; set; }
        public DbSet<UpdateProfileViewModel> UpdateProfileViewModel { get; set; }
    }
}
