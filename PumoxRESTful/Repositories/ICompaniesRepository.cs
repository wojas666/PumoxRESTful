using PumoxRESTful.Models;

namespace PumoxRESTful.Repositories
{
    public interface ICompaniesRepository
    {
        Company GetCompany(long id);

        IEnumerable<Company> GetCompanies();

        long CreateCompany(Company company);

        void UpdateCompany(Company company);

        void DeleteCompany(long id);

        ICollection<Company> searchCompanies(string? keyword, DateTime? employeeDateOfBirthFrom,
            DateTime? employeeDateOfBirthTo, ICollection<JobTitle>? employeeJobTitles);
    }
}
