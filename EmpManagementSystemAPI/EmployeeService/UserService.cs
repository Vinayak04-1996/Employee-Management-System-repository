using EmpManagementSystemAPI.EmployeeDbContext;
using EmpManagementSystemAPI.Models;

namespace EmpManagementSystemAPI.EmployeeService
{
    public class UserService : IUser
    {
        private readonly EmpDbContext _empDbContext;
        public UserService(EmpDbContext empDbContext)
        {
            _empDbContext = empDbContext;
        }
        public User AddUser(User user)
        {
            _empDbContext.Users.Add(user);
            _empDbContext.SaveChanges();
            return user;
        }

        //public List<User> GetAllUser()
        //{
        //    var _userList  =_empDbContext.Users.ToList();
        //    if (_userList.Count > 0)
        //    {
        //        return _userList;
        //    }
        //    else
        //    {
        //        return new List<User>();
        //    }
        //}

        //public User GetUserById(int id)
        //{
        //    var user = _empDbContext.Users.FirstOrDefault(x => x.Id == id);
        //    if (user == null) {
        //        return null;
        //    }
        //    else
        //    {
        //        return user;
        //    }
        //}

        public User GetUserByName(string username, string password)
        {
            var userDetails = _empDbContext.Users.
                                Where(x=>x.Email == username && x.Password == password)
                                .FirstOrDefault();
            if (userDetails == null) 
            {
                return null;
            }
            else
            {
                return userDetails;
            }
        }
    }
}
