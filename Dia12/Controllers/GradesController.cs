using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dia12.Data;
using Dia12.Models;
using Dia12.UnitsOfWork;

namespace Dia12.Controllers
{
    public class GradesController : Controller
    {

        private UnitOfWork unitOfWork;
        public GradesController(ApplicationDbContext context)
        {
            unitOfWork =new UnitOfWork(context);
        }
        

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            var grade = await unitOfWork.GradeRepo.GetAll();
              return View(grade);
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || unitOfWork.GradeRepo == null)
            {
                return NotFound();
            }

            var grade = await unitOfWork.GradeRepo.GetById(id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,GradeName,Section")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.GradeRepo.Insert(grade);
                 unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || unitOfWork.GradeRepo == null)
            {
                return NotFound();
            }

            var grade = await unitOfWork.GradeRepo.GetById(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,GradeName,Section")] Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await unitOfWork.GradeRepo.Insert(grade);
                    unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
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
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || unitOfWork.GradeRepo == null)
            {
                return NotFound();
            }

            var grade = await unitOfWork.GradeRepo.GetById(id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (unitOfWork.GradeRepo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Grades'  is null.");
            }
            var grade = await unitOfWork.GradeRepo.GetById(id);
            if (grade != null)
            {
                await unitOfWork.GradeRepo.Delete(id);
            }
            
            unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
