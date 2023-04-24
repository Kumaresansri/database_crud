using Database.Data;
using Database.Models;
using Microsoft.AspNetCore.Mvc;


using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Database.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public ResumeController( ResumeDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants =_context.Applicants.ToList();
            return View(applicants);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Applicant applicant=new Applicant();
            applicant.Experiences.Add(new Experience() { ExpericenceId=1});
            return View(applicant);
        }
     
        [HttpPost]
        public IActionResult Create(Applicant applicant) 
        {
            applicant.Experiences.RemoveAll(n => n.YearsWorked == null);


            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;


            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;
            if( applicant.ProfilePhoto != null)
            {
                string uploadsFloder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName=Guid.NewGuid().ToString()+"_" +applicant.ProfilePhoto.FileName;
                string filePath =Path.Combine(uploadsFloder,uniqueFileName);
                using (var fileStream =new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        public IActionResult Details(int Id)
        {
            Applicant applicant = _context.Applicants
                .Include(e => e.Experiences)
                .Where(a => a.Id == Id).FirstOrDefault();
            
            return View(applicant);
        }
        [HttpGet]
     public IActionResult Delete(int Id)
        {
            Applicant applicant=_context.Applicants
                .Include (e=>e.Experiences)
                .Where(a=>a.Id==Id).FirstOrDefault();
            return View(applicant);
        }
        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            _context.Attach(applicant);
            _context.Entry(applicant).State=EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Applicant applicant=_context.Applicants
                .Include(e=>e.Experiences)
                .Where(a=>a.Id==id).FirstOrDefault();
            return View(applicant);
        }
        [HttpPost]
        public IActionResult Edit(Applicant applicant)
        {
            applicant.Experiences.RemoveAll(n =>n.YearsWorked == null );
            string uniqueFileName=GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;
            _context.Attach(applicant);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
