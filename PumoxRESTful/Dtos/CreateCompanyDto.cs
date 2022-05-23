using System.ComponentModel.DataAnnotations;
using PumoxRESTful.Models;

namespace PumoxRESTful.Dtos
{
    public class CreateCompanyDto
    {
        [Required(ErrorMessage = "Nazwa firmy jest wymagana.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Rok założenia firmy jest wymagany.")]
        [Range(1300,2200, ErrorMessage = "Podano nieprawidłowy rok założenia firmy. Wprowadź właściwe dane.")]
        public int EstablishmentYear { get; set; }

        [Required(ErrorMessage = "Musisz uwzględnić kolekcję pracowników. Kolekcja może być pusta.")]
        public virtual ICollection<CreateEmployeeDto> Employees { get; set; }
    }
}
