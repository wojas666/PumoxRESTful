using System.ComponentModel.DataAnnotations;

namespace PumoxRESTful.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public JobTitle JobTitle { get; set; }

        public Company Company { get; set; }
    }

    public enum JobTitle
    {
        Administrator,
        Developer,
        Architect,
        Manager
    }
}
