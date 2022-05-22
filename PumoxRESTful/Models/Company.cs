using System.ComponentModel.DataAnnotations;

namespace PumoxRESTful.Models
{
    public class Company
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
