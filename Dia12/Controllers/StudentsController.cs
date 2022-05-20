using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dia12.Data;
using Dia12.UnitsOfWork;
using Dia12.Models;

namespace Dia12.Controllers
{
    public class StudentsController : Controller
    {

        UnitOfWork unitOfWork;
        public StudentsController(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var student = await unitOfWork.StudentRepo.GetAll();
              return View(student);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || unitOfWork.StudentRepo == null)
            {
                return NotFound();
            }

            var student = await unitOfWork.StudentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,DateOfBirth,Photo,Height,Weight")] Student student)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.StudentRepo.Insert(student);
                unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || unitOfWork.StudentRepo == null)
            {
                return NotFound();
            }

            var student = await unitOfWork.StudentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,DateOfBirth,Photo,Height,Weight")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await unitOfWork.StudentRepo.Update(student);
                    unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || unitOfWork.StudentRepo == null)
            {
                return NotFound();
            }

            var student = await unitOfWork.StudentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (unitOfWork.StudentRepo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await unitOfWork.StudentRepo.GetById(id);
            if (student != null)
            {
                await unitOfWork.StudentRepo.Delete(id);
            }

            unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            //return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
            return true;
        }
        //protected override void Dispose(bool disposing)
        //{
        //    unitOfWork.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
