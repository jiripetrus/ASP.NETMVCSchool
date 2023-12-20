using ASP.NETMVCSchool.Models;
using ASP.NETMVCSchool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETMVCSchool.Controllers
{
    [Authorize(Roles = "AdminRole")]
    public class StudentsController : Controller
    {
        public StudentService service;

        public StudentsController(StudentService service)
        {
            this.service = service;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student newStudent)
        {
            if (ModelState.IsValid)
            {
                await service.CreateAsync(newStudent);
                return RedirectToAction("Index"); 
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Index()
        {
            var allStudents = await service.GetAllAsync();
            return View(allStudents);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var studentToEdit = await service.GetByIdAsync(id);
            if (studentToEdit == null)
            {
                return View("NotFound");
            }
            return View(studentToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, DateOfBirth")] Student student)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateAsync(id, student);
                return RedirectToAction("Index"); 
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var studentToDelete = await service.GetByIdAsync(id);
            if (studentToDelete == null)
            {
                return View("NotFound");
            }
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
