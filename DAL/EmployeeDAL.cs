using Dapper;
using Employee_Skill_Management.Security;
using Employee_Skill_Management.ViewModels;
using MySql.Data.MySqlClient;
using System.Data;

namespace Employee_Skill_Management.DAL
{
    public class EmployeeDAL
    {
        private IConfiguration _config;
        private string connectionString;
        
        public EmployeeDAL(IConfiguration config) {
            _config = config;
            connectionString = config.GetSection("ConnectionStrings")["SkillDbConnectionString"].ToString();

        }

        public bool ValidateUser(Login_User loginData)
        {
            byte[] salt = PasswordHasher.GenerateSalt();
            string hashedPassword = PasswordHasher.HashPassword(loginData.password, salt); ;
            string dbPassword = "";
            try
            {
                using (IDbConnection dbConnection = new MySqlConnection(connectionString))
                {
                    // Write your SQL query to retrieve the password hash based on the user ID
                    string query = "SELECT password FROM user_data WHERE user_id = @UserId";

                    // Execute the query and retrieve the password hash
                    dbPassword = dbConnection.QuerySingleOrDefault<string>(query, new { UserId = loginData.loginId });

                }

            }
            catch (Exception ex)
            {

            }
            bool passwordIsValid = PasswordHasher.VerifyPassword(loginData.password, hashedPassword, salt);

            if (passwordIsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<EmployeeDataTableRecord> GetAllEmps()
        {
            List<EmployeeDataTableRecord> result = null;
            try
            { 
                using var connection = new MySqlConnection(connectionString);

                result = connection.Query<EmployeeDataTableRecord>("USP_GetAllEmployees", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            
            return result;
        } 

        public string AddNewEmployee(Employee emp)
        {
            string result = "";
            try
            {
                using var connection = new MySqlConnection(connectionString);        
                
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@first_name", emp.first_name);
                parameters.Add("@emp_id", emp.emp_id);
                parameters.Add("@last_name", emp.last_name);
                parameters.Add("@surname", emp.surname);
                parameters.Add("@dob", emp.dob);
                parameters.Add("@doj", emp.doj);
                parameters.Add("@reporting_manager", emp.reporting_manager);
                parameters.Add("@gender", emp.gender);
                parameters.Add("@phone", emp.phone);
                parameters.Add("@personal_email", emp.personal_email);
                parameters.Add("@employee_email", emp.employee_email);
                parameters.Add("@address", emp.address);
                parameters.Add("@blood_group", emp.blood_group);
                parameters.Add("@about_me", emp.about_me);
                parameters.Add("@job_title", emp.job_title);
                parameters.Add("@job_title", emp.job_title);
                parameters.Add("@job_title", emp.job_title);
                parameters.Add("@job_title", emp.job_title);

                result = connection.ExecuteScalar<string>("USP_AddNewEmployee", param: parameters, commandType: CommandType.StoredProcedure);
                
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        public Employee GetLoggedUserData(string empId)
        {
            LoggedInEmployeeData loggedInEmployeeData = new LoggedInEmployeeData();
            Employee employee = null;
            IEnumerable<EmployeeDataTableRecord> allEmps = null;
            try
            {
                using (IDbConnection dbConnection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    dbConnection.Open();

                    // Execute the stored procedure
                    using (var result = dbConnection.QueryMultiple("USP_GetUserDetails", new { user_id = empId }, commandType: CommandType.StoredProcedure))
                    {
                        // Read the first result set (user data)
                        employee = result.Read<Employee>().FirstOrDefault();

                        //employee = employees != null && employees.GetEnumerator().MoveNext() ? (Employee)employees.FirstOrDefault() : null;

                        //// Read the second result set (employees data)
                        //if (!result.IsConsumed)
                        //{
                        //    allEmps = result.Read<EmployeeDataTableRecord>();
                        //}
                            
                        //loggedInEmployeeData.loggedUserData = employee;
                        //loggedInEmployeeData.allEmployees = (List<EmployeeDataTableRecord>)allEmps;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return employee;

        }
    }
}
