using Microsoft.EntityFrameworkCore;
using PumoxRESTful.DAL;
using PumoxRESTful.Models;

namespace PumoxRESTful.Repositories
{
    public class SqlDbCompaniesRepository : ICompaniesRepository
    {
        private readonly ApplicationContext _context;

        public SqlDbCompaniesRepository(ApplicationContext db)
        {
            _context = db;
        }

        public Company GetCompany(long id)
        {
            var company = _context.Companies.Where(x => x.Id == id).SingleOrDefault();

            if (company is not null)
            {
                company.Employees = _context.Employees.Where(x => x.Company == company).ToList();
            }

            return company;
        }

        public IEnumerable<Company> GetCompanies()
        {
            var results = _context.Companies.ToList();

            results = SetEmployeesToCompanies(results).ToList();

            return results;
        }

        public long CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();

            return company.Id;
        }

        public void UpdateCompany(Company company)
        {
            _context.Companies.Update(company);
            _context.SaveChanges();
        }

        public void DeleteCompany(long id)
        {
            var item = _context.Companies.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                _context.Companies.Remove(item);
                _context.SaveChanges();
            }
            else
                throw new Exception("Firma o podanym identyfikatorze nie isteniej.");
        }

        public ICollection<Company> searchCompanies(string? keyword, DateTime? employeeDateOfBirthFrom, DateTime? employeeDateOfBirthTo,
            ICollection<JobTitle>? employeeJobTitles)
        {
            List<Company> results = null;

            if (!String.IsNullOrEmpty(keyword))
            {
                results = _context.Employees
                    .Where(x => x.FirstName.Contains(keyword) || x.LastName.Contains(keyword) ||
                                x.Company.Name.Contains(keyword)).Select(x => x.Company).ToList();

                SetEmployeesToCompanies(results);
            }
            else
            {
                results = GetCompanies().ToList();
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

            return results.Distinct().ToList();
        }

        #region employees helpers method
        private ICollection<Company> SetEmployeesToCompanies(List<Company> companies)
        {
            for (int i = 0; i < companies.Count; i++)
            {
                companies[i].Employees = GetEmployeesForCompany(companies[i]);
            }

            return companies;
        }

        private ICollection<Employee> GetEmployeesForCompany(Company company)
        {
            return _context.Employees.Where(x => x.Company == company).ToList();
        }
        #endregion
    }
}
