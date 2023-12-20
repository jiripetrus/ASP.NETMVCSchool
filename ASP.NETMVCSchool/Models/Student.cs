using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETMVCSchool.Models
{
    public class Student
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date of birth")]
        [Required(ErrorMessage = "Date field is required.")]
        public DateTime DateOfBirth { get; set; }


    }
}
