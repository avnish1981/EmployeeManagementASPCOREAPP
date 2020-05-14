using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementASPCOREAPP.Web.Models
{
    public class SQLEmployeeRepository : IEmployeeRepositary
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context )
        {
            this.context = context;
        }
        public Employee  Add(Employee emp)
        {
            context.Employees.Add(emp);
             context.SaveChanges();
            return emp;
        }

        public Employee Delete(Employee empl)
        {
            Employee emp = context.Employees.Find(empl.Id);
            if(emp!=null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return  context.Employees.Find(Id);
         
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
