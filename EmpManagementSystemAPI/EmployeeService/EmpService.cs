using EmpManagementSystemAPI.EmployeeDbContext;
using EmpManagementSystemAPI.Models;

namespace EmpManagementSystemAPI.EmployeeService
{
    public class EmpService : IEmployee
    {
        private readonly EmpDbContext _empDbContext;
        public EmpService(EmpDbContext empDbContext) 
        { 
            _empDbContext = empDbContext;
        }
        public string AddEmployee(Employee employee)
        {
            employee.IsActive = true;
            _empDbContext.Employees.Add(employee);
            _empDbContext.SaveChanges();

            return "Add employee succesfully";
        }

        public string DeleteEmployee(int id)
        {
            var existingEmployee = _empDbContext.Employees.Where(x=>x.Id == id).FirstOrDefault();

            if (existingEmployee != null)
            {
                existingEmployee.IsActive = false;
                _empDbContext.Employees.Update(existingEmployee);
                _empDbContext.SaveChanges();

                return "Deleted employee successfully";
            }
            else 
            {
                return "Failed to delete employee";
            }
        }

        public Employee GetEmployeeById(int id)
        {
            var existingEntity = _empDbContext.Employees.FirstOrDefault(x=>x.Id == id);
            if (existingEntity != null)
            { 
                return existingEntity;
            }
            else
            {
                return null;
            }
        }

        public List<Employee> GetEmployeeList()
        {
            var employeeList = _empDbContext.Employees.Where(x=>x.IsActive == true).ToList();
            return employeeList;
        }

        public string UpdateEmployee(int id, Employee employee)
        {
            var existingrecord = _empDbContext.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (existingrecord != null)
            {
                existingrecord.Name = employee.Name;
                existingrecord.Designation = employee.Designation;
                existingrecord.Address = employee.Address;
                existingrecord.Gender = employee.Gender;
                existingrecord.Salary = employee.Salary;
                existingrecord.Email = employee.Email;
                existingrecord.IsActive = employee.IsActive;
                _empDbContext.Employees.Update(existingrecord);
                _empDbContext.SaveChanges();
                return "update data successfully";
            }
            else
            {
                return "Failed to update data";
            }
        }
    }
}
