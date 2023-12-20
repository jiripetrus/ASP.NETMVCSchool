using ASP.NETMVCSchool.Models;
using ASP.NETMVCSchool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace ASP.NETMVCSchool.Controllers
{
    [Authorize(Roles = "AdminRole,TeacherRole,StudentRole")]
    public class SubjectsController : Controller
    {
        public SubjectService service;

        public SubjectsController(SubjectService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "AdminRole,TeacherRole")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "AdminRole,TeacherRole")]
        public async Task<IActionResult> Create(Subject newSubject)
        {
            if (ModelState.IsValid)
            {
                await service.CreateAsync(newSubject);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "AdminRole,TeacherRole")]
        public async Task<IActionResult> Index()
        {
            var allSubjects = await service.GetAllAsync();
            return View(allSubjects);
        }

        [Authorize(Roles = "AdminRole,TeacherRole")]
        public async Task<IActionResult> Edit(int id)
        {
            var subjectToEdit = await service.GetByIdAsync(id);
            if (subjectToEdit == null)
            {
                return View("NotFound");
            }
            return View(subjectToEdit);
        }

        [HttpPost]
        [Authorize(Roles = "AdminRole,TeacherRole")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateAsync(id, subject);
                return RedirectToAction("Index"); 
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "AdminRole,TeacherRole")]
        public async Task<IActionResult> Delete(int id)
        {
            var subjectToDelete = await service.GetByIdAsync(id);
            if (subjectToDelete == null)
            {
                return View("NotFound");
            }
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
