using Employee_Skill_Management.DAL;
using Employee_Skill_Management.Security;
using Employee_Skill_Management.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Skill_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeDAL empDal;

        public EmployeeController(IConfiguration config) 
        { 
            empDal = new EmployeeDAL(config);
        }
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(Login_User loginData)
        {
            Employee loggedInEmployeeData = null;
            var isValid = empDal.ValidateUser(loginData);
            if (isValid)
            {
                loggedInEmployeeData = empDal.GetLoggedUserData(loginData.loginId);
                return Ok(new { status = 1, message = "success", data = loggedInEmployeeData });
            }
            else
            { 
                return BadRequest(new { status = 0, message = "invalid user", data = new Employee() });
            }
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            List<EmployeeDataTableRecord> allEmps = null;
            allEmps = empDal.GetAllEmps();
            if (allEmps != null)
            {
                return Ok(new { status = 1, message = "success", data = allEmps });
            }
            else
            {
                return BadRequest(new { status = 0, message = "no data", data = allEmps });
            }
            
        }
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee(Employee emp)
        {

            var result = empDal.AddNewEmployee(emp);
            return result.ToLower() == "success" ? Ok(emp) : BadRequest(result);
        }

        [HttpPost]
        [Route("/hash")]
        public IActionResult HashedPassword(string pwd)
        {
            string password = pwd;
            byte[] salt = PasswordHasher.GenerateSalt();
            string hashedPassword = PasswordHasher.HashPassword(password, salt);
            return Ok(hashedPassword);
        }
    }
}
