using EmployeeManagementSystemAPP.Controllers;
using EmployeeManagementSystemAPP.Models;
using EmpManagementSystemAPI.EmployeeDbContext;
using EmpManagementSystemAPI.EmployeeService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

builder.Services.AddTransient<IEmployee, EmpService>();
builder.Services.AddTransient<IUser, UserService>();

builder.Services.AddTransient<EmpManagementSystemAPI.Controllers.EmployeeController>();
builder.Services.AddTransient<EmpManagementSystemAPI.Controllers.UserController>();
builder.Services.AddHttpClient();
builder.Services.AddTransient<LoginController>();

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Enables authentication
app.UseAuthorization();   // Enables authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
