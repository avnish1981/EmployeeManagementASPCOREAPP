using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementASPCOREAPP.Web.Models
{
    public class MockEmployeeRepositary : IEmployeeRepositary
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepositary()
        {
            _employeeList = new List<Employee>
            {
                new Employee{Id=1,Name="Avnish",Department="Developer" ,Email="avnish.choubey@gmail.com"},
                new Employee{Id=2,Name="Manish",Department="HR" ,Email="manish.choubey@gmail.com"}
            };
        }
        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee  Add(Employee emp)
        {
            emp.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(emp);
            return emp;
            
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee emp = _employeeList.FirstOrDefault(x => x.Id == employeeChanges.Id );
            if (emp != null)
            {
                emp.Name = employeeChanges.Name;
                emp.Email = employeeChanges.Email;
                emp.Department = employeeChanges.Department;
            }
            return emp;
        }

        public Employee Delete(Employee empl)
        {
           Employee emp =  _employeeList.FirstOrDefault(x => x.Id == empl.Id );
            if(emp!=null)
            {
                _employeeList.Remove(emp);
            }
            return emp;
            
        }
    }
}
