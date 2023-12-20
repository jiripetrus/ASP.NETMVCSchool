using ASP.NETMVCSchool.Models;

namespace ASP.NETMVCSchool.ViewModels
{
    public class GradesDropdownsViewModel
    {
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public GradesDropdownsViewModel()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }
    }
}