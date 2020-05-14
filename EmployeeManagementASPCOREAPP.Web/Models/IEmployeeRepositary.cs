using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementASPCOREAPP.Web.Models
{
    public interface  IEmployeeRepositary
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployee();
        Employee  Add(Employee emp);
        Employee Update(Employee employeeChanges);
        Employee Delete(Employee emp);
    }
}
