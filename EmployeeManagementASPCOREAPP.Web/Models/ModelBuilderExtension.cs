using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementASPCOREAPP.Web.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(

               new Employee
               {
                   Id = 1,
                   Department = "HR",
                   Email = "avnish.choubey@gmail.com",
                   Name = "Avnish"
               },
           new Employee
           {
               Id = 2,
               Department = "Developer",
               Email = "manish.choubey@gmail.com",
               Name = "Manish"
           }



               );
        }
    }
}
