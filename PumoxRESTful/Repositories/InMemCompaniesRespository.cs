using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using PumoxRESTful.Models;

namespace PumoxRESTful.Repositories
{
    public class InMemCompaniesRespository : ICompaniesRepository
    {
        private readonly List<Company> _companies = new ()
        {
            new Company() { Id = 1, Employees = new List<Employee>(), EstablishmentYear = 1999, Name = "Test" },
            new Company() { Id = 2, Employees = new List<Employee>(), EstablishmentYear = 2020, Name = "Example Company"}
        };

        public long CreateCompany(Company company)
        {
            company.Id = GetNewId();
            _companies.Add(company);

            return company.Id;
        }

        private long GetNewId()
        {
            var lastItem = _companies.OrderByDescending(x => x.Id).FirstOrDefault();

            if (lastItem is null) return 0;
            else return lastItem.Id+1;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _companies;
        }

        public Company GetCompany(long id)
        {
            return _companies.Where(x => x.Id == id).SingleOrDefault();
        }

        public void UpdateCompany(Company company)
        {
            var index = _companies.FindIndex(existingCompany => existingCompany.Id == company.Id);
            _companies[index] = company;
        }

        public void DeleteCompany(long id)
        {
            var index = _companies.FindIndex(existingCompany => existingCompany.Id == id);
            _companies.RemoveAt(index);
        }

        public ICollection<Company> searchCompanies(string? keyword, DateTime? employeeDateOfBirthFrom, DateTime? employeeDateOfBirthTo,
            ICollection<JobTitle>? employeeJobTitles)
        {
            ICollection<Company> results = GetCompanies().ToList();
            
            if (!String.IsNullOrEmpty(keyword))
            {
                results = results
                    .Where(x => x.Employees.Any(y => y.FirstName.Contains(keyword) || y.LastName.Contains(keyword))
                                || x.Name.Contains(keyword)).ToList();
            }

            if (employeeDateOfBirthFrom is not null)
            {
                DateTime dateFrom = (DateTime)employeeDateOfBirthFrom;
                results = results.Where(x => x.Employees.Any(y => y.DateOfBirth >= dateFrom)).ToList();
            }

            if (employeeDateOfBirthTo is not null)
            {
                DateTime dateTo = (DateTime)employeeDateOfBirthTo;
                results = results.Where(x => x.Employees.Any(y => y.DateOfBirth <= dateTo)).ToList();
            }

            if (employeeJobTitles is not null)
            {
                List<JobTitle> jobTitle = employeeJobTitles.ToList();
                results = results.Where(x => x.Employees.Any(y => employeeJobTitles.Contains(y.JobTitle))).ToList();
            }

            return results;
        }
    }
}
