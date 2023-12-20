using ASP.NETMVCSchool.Services;
using ASP.NETMVCSchool.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NETMVCSchool.Controllers
{
    [Authorize(Roles = "TeacherRole,AdminRole,StudentRole")]
    public class GradesController : Controller
    {
        public GradeService service;

        public GradesController(GradeService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allGrades = await service.GetAllAsync();
            return View(allGrades);
        }

        [Authorize(Roles = "TeacherRole,AdminRole")]
        public async Task<IActionResult> Create()
        {
            var gradesDropdownsData = await service.GetNewGradesDropdownsValues();
            ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "TeacherRole,AdminRole")]
        public async Task<IActionResult> Create(GradesViewModel newGrade)
        {
            if (!ModelState.IsValid)
            {
                var gradesDropdownsData = await service.GetNewGradesDropdownsValues();
                ViewBag.Students = new SelectList(gradesDropdownsData.Students,"Id", "LastName");
                ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects,"Id", "Name");
                return View(newGrade);
            }
            await service.CreateAsync(newGrade);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "TeacherRole,AdminRole")]
        public async Task<IActionResult> Edit(int id)
        {
            var gradeToEdit = await service.GetByIdAsync(id);
            if (gradeToEdit == null)
            {
                return View("NotFound");
            }
            var response = new GradesViewModel()
            {
                Id = gradeToEdit.Id,
                Date = gradeToEdit.Date,
                Mark = gradeToEdit.Mark,
                StudentId = gradeToEdit.Student.Id,
                SubjectId = gradeToEdit.Subject.Id,
                Topic = gradeToEdit.Topic
            };
            var gradesDropdownsData = await service.GetNewGradesDropdownsValues();
            ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        [Authorize(Roles = "TeacherRole,AdminRole")]
        public async Task<IActionResult> Edit(int id, GradesViewModel updatedGrade)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateAsync(id, updatedGrade);
                return RedirectToAction("Index"); 
            }
            else
            {
                var gradesDropdownsData = await service.GetNewGradesDropdownsValues();
                ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
                ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
                return View();
            }
        }

        [Authorize(Roles = "TeacherRole,AdminRole")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
