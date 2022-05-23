using PumoxRESTful.Models;

namespace PumoxRESTful.Dtos
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
