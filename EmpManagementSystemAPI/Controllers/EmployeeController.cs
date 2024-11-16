using EmpManagementSystemAPI.EmployeeService;
using EmpManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpManagementSystemAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _empService;

        public EmployeeController(IEmployee empService)
        {
            _empService = empService;
        }

        // GET: api/Employee/GetEmployeeList
        [HttpGet("GetEmployeeList")]
        public List<Employee> GetEmployeeList()
        {
            try
            {
                List<Employee> employeeList = new List<Employee>();
                employeeList = _empService.GetEmployeeList().ToList();

                if (employeeList != null && employeeList.Count > 0)
                {
                    return employeeList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/Employee/GetEmployeeById/{id}
        [HttpGet("GetEmployeeById/{id}")]
        public Employee GetEmployeeById(int id)
        {
            try
            {
                var employeeData = _empService.GetEmployeeById(id);
                if (employeeData != null)
                {
                    return employeeData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/Employee/AddEmployee
        [HttpPost("AddEmployee")]
        public string AddEmployee([FromBody] Employee employee)
        {
            try
            {
                var response = _empService.AddEmployee(employee);
                return response;
            }
            catch (Exception ex)
            {
                return "Internal server error";
            }
        }

        // PUT: api/Employee/UpdateEmployee/{id}
        [HttpPut("UpdateEmployee/{id}")]
        public string UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                var response = _empService.UpdateEmployee(id, employee);
                return response;
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }
        }

        // DELETE: api/Employee/DeleteEmployee/{id}
        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var response = _empService.DeleteEmployee(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
