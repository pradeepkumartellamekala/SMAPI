namespace Employee_Skill_Management.ViewModels
{
    public class Employee
    {
        public int id { get; set; }
        public string emp_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string surname { get; set; }
        public DateTime dob { get; set; }
        public DateTime doj { get; set; }
        public string gender { get; set; }
        public string reporting_manager { get; set; }
        public string phone { get; set; }
        public string personal_email { get; set; }
        public string employee_email { get; set; }
        public string address { get; set; }
        public string blood_group { get; set; }
        public string job_title { get; set; }
        public string about_me { get; set; }
        public int role_id { get; set; }
    }
    public class LoggedInEmployeeData 
    {
        public Employee loggedUserData { get; set; }
        public List<EmployeeDataTableRecord> allEmployees { get; set; }
    }
    public class EmployeeSkill
    {
        public long id { get; set; }
        public string emp_id { get; set; }
        public string skill_name { get; set; }
        public DateOnly added_on { get; set; }
    }
    public class UserData
    {
        public string user_id { get; set; }
        public string password { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
    }
    public class ReportingData
    {
        public long id { get; set; }
        public DateOnly reporting_to { get; set; }
        public DateOnly reporting_from { get; set; }
        public string client { get; set; }
        public string emp_id { get; set; }
        public string reporting_manager { get; set; }

    }
}