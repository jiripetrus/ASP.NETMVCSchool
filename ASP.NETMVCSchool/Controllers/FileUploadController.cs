using ASP.NETMVCSchool.Models;
using ASP.NETMVCSchool.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml;

namespace ASP.NETMVCSchool.Controllers
{
    public class FileUploadController : Controller
    {
        public StudentService service;

        public FileUploadController(StudentService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string filePath = "";
            if (file.Length > 0)
            {
                filePath = Path.GetFullPath(file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Close();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);
                    XmlElement root = xmlDoc.DocumentElement;
                    foreach (XmlNode node in root.SelectNodes("/students/student"))
                    {
                        Student s = new Student
                        {
                            FirstName = node.ChildNodes[0].InnerText,
                            LastName = node.ChildNodes[1].InnerText,
                            DateOfBirth = DateTime.Parse(node.ChildNodes[2].InnerText, CultureInfo.CreateSpecificCulture("cs-CZ"))
                        };
                        await service.CreateAsync(s);
                    }
                }
                return RedirectToAction("Index", "Students");
            }
            else return View("NotFound");
        }
    }
}
