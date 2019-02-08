using Microsoft.EntityFrameworkCore;
using MyPayTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPayTracker.Data
{
    public class TimeSheetDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
         

        public TimeSheetDbContext(DbContextOptions<TimeSheetDbContext> options) 
                    : base(options)
        {
        }
    }
}
