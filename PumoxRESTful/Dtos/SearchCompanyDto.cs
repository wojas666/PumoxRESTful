using PumoxRESTful.Models;

namespace PumoxRESTful.Dtos
{
    public class SearchCompanyDto
    {
        public string? Keyword { get; set; }

        public DateTime? EmployeeDateOfBirthFrom { get; set; }

        public DateTime? EmployeeDateOfBirthTo { get; set; }

        public ICollection<JobTitle>? EmployeeJobTitles { get; set; }

    }

}
