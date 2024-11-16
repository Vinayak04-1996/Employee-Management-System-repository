using EmpManagementSystemAPI.Models;

namespace EmpManagementSystemAPI.EmployeeService
{
    public interface IUser
    {
        public User AddUser(User user);
        //public List<User> GetAllUser();
        //public User GetUserById(int id);
        public User GetUserByName(string username, string password);
    }
}
