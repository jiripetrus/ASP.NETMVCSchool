using System.ComponentModel.DataAnnotations;

namespace ASP.NETMVCSchool.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Subject may contain only letters")]
        public string Name { get; set; }
    }
}
