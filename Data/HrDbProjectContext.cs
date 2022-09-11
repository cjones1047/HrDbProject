using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HrDbProject.Models;

namespace HrDbProject.Data
{
    public class HrDbProjectContext : DbContext
    {
        public HrDbProjectContext (DbContextOptions<HrDbProjectContext> options)
            : base(options)
        {
        }

        public DbSet<HrDbProject.Models.Employee> Employees { get; set; } = default!;
    }
}
