using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PumoxRESTful.Models;

namespace PumoxRESTful.Dtos
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }    

        public DateTime DateOfBirth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public JobTitle JobTitle { get; set; }    
    }
}
