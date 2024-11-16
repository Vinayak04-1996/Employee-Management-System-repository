using EmpManagementSystemAPI.EmployeeService;
using EmpManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace EmployeeManagementSystemAPP.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmpManagementSystemAPI.Controllers.EmployeeController _employeeController;
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly HttpClient _httpClient;
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee,EmpManagementSystemAPI.Controllers.EmployeeController employeeController,
            IHttpClientFactory httpClientFactory) 
        { 
            _employee = employee;
            _employeeController = employeeController;
            _httpClientFactory = httpClientFactory;
        }

        public ActionResult Index()
        {
            try
            {
                var token = Request.Cookies["jwtToken"]; // If token is stored in cookies
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }
                else
                {
                    var client = _httpClientFactory.CreateClient();

                    // Add the JWT token to the Authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    //// Call the Employee API
                    var response = _employeeController.GetEmployeeList();
                    return View(response);
                }
            }
            catch (Exception ex) 
            { 
                throw ex;
            }
            
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                string msg = _employeeController.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Employee employee = _employeeController.GetEmployeeById(id);
                return View(employee);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,Designation,Address,Gender,Salary,Email,IsActive")] Employee employee)
        {
            try
            {
                var response = _employeeController.UpdateEmployee(id, employee);
                TempData["Message"] = response;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                var deleteEmployee = _employeeController.DeleteEmployee(id);
                return Json(new { reponse = deleteEmployee });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
