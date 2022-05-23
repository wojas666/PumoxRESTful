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
                Employees = company.Employees,
                EstablishmentYear = company.EstablishmentYear,
                Name = company.Name
            };
        }
    }
}
