using EmpManagementSystemAPI.Models;

namespace EmpManagementSystemAPI.EmployeeService
{
    public interface IEmployee
    {
        string AddEmployee(Employee employee);
        string UpdateEmployee(int id, Employee employee);
        Employee GetEmployeeById(int id);
        List<Employee> GetEmployeeList();
        string DeleteEmployee(int id);

    }
}
