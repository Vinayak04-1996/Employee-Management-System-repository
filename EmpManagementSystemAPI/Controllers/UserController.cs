using EmpManagementSystemAPI.EmployeeService;
using EmpManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
             _user = user;
        }

        //// GET: api/<UserController>
        //[HttpGet("GetUserList")]
        //public List<User> GetAllList()
        //{
        //    var users = _user.GetAllUser().ToList();
        //    return users;
        //}

        //// GET api/<UserController>/5
        //[HttpGet("GetUserById/{id}")]
        //public User GetUserById(int id)
        //{
        //    return _user.GetUserById(id);
        //}

        // POST api/<UserController>
        [HttpPost("AddUser")]
        public string AddUser([FromBody] User user)
        {
            try
            {
                var response = _user.AddUser(user);
                if (response != null)
                {
                    return "Add user successfully.";
                }
                else
                {
                    return "Failed to add user.";
                }
                
            }
            catch (Exception ex)
            {
                return "Internal server error";
            }
        }

        public User GetUserByName(string UserName,string Password) 
        { 
            return _user.GetUserByName(UserName,Password);
        }
    }
}
