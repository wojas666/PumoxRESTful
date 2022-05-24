using PumoxRESTful.Dtos;
using PumoxRESTful.Models;

namespace PumoxRESTful
{
    public static class Extensions
    {
        public static CompanyDto AsDto(this Company company)
        {
            return new CompanyDto()
            {
                Id = company.Id,
                Employees = company.Employees.AsDtos(),
                EstablishmentYear = company.EstablishmentYear,
                Name = company.Name
            };
        }

        public static ICollection<EmployeeDto> AsDtos(this ICollection<Employee> employees)
        {
            List<EmployeeDto> resultDtos = new List<EmployeeDto>();

            foreach (var item in employees)
            {
                resultDtos.Add(new EmployeeDto()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth,
                    JobTitle = item.JobTitle
                });
            }

            return resultDtos;
        }
    }
}
