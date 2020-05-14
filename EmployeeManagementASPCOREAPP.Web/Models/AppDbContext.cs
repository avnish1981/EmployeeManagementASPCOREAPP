﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementASPCOREAPP.Web.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions )
            : base(dbContextOptions)
        {

        }
        public DbSet<Employee > Employees { get; set; }

        //for seeding data to Employee table...
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
                
          
        }
    }
}
