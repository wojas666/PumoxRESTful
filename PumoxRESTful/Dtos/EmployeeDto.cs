using PumoxRESTful.Models;

namespace PumoxRESTful.Dtos
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }    

        public DateTime DateOfBirth { get; set; }

        public JobTitle JobTitle { get; set; }    
    }
}
