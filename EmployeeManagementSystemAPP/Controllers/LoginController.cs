using EmployeeManagementSystemAPP.Models;
using EmpManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystemAPP.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmpManagementSystemAPI.Controllers.UserController _userController;
        private IConfiguration _config;

        public LoginController(EmpManagementSystemAPI.Controllers.UserController userController, IConfiguration config) 
        {
            _userController = userController;
            _config = config;
        }


        // GET: LoginController
        public ActionResult Login()
        {
            TempData["Login"] = "Login";
            return View(new Login());
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            var user = _userController.GetUserByName(login.UserName,login.Password);
            if (user != null)
            {
                var token = GenerateToken(user);
                Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // set to true for HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddHours(1)
                });
                ModelState.AddModelError(string.Empty, "Login successfully");
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return BadRequest("Invalid username or password");
            }
        }

        private string GenerateToken(User user)
        {
            // Creates a new symmetric security key from the JWT key specified in the app configuration.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            // Sets up the signing credentials using the above security key and specifying the HMAC SHA256 algorithm.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Defines a set of claims to be included in the token.
            var claims = new List<Claim>
            {
                // Custom claim using the user's ID.
                new Claim("Myapp_User_Id", user.Id.ToString()),
                // Standard claim for user identifier, using username.
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                // Standard claim for user's email.
                new Claim(ClaimTypes.Email, user.Email),
                // Standard JWT claim for subject, using user ID.
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            // Adds a role claim for each role associated with the user.
            //user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            // Creates a new JWT token with specified parameters including issuer, audience, claims, expiration time, and signing credentials.
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration set to 1 hour from the current time.
                signingCredentials: credentials);
            // Serializes the JWT token to a string and returns it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
