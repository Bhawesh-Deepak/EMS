using EMS.Core.Entities.Survey;
using EMS.Core.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Common
{
    public class EMSContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=89.163.218.70\\MSSQLSERVER2017; Database= BIS_Store1; User Id=igl;Password = Manoj@12345");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Options> Options { get; set; }
    }
}
