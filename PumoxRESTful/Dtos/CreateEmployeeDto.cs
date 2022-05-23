using System.ComponentModel.DataAnnotations;
using PumoxRESTful.Models;

namespace PumoxRESTful.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Imię pracownika jest wymagane.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko pracownika jest wymagane.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Nazwa stanowiska jest wymagana.")]
        public JobTitle JobTitle { get; set; }
    }
}
