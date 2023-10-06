﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH01.Data;
using TH01.Models;

namespace TH01.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;
        public LearnerController(SchoolContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var learners = db.Learners.Include(m => m.Major).ToList();
            return View(learners);
        }
        public IActionResult Create()
        {
            //dùng 1 trong 2 cách để tạo SelectList gửi về View qua ViewBag để
            //hiển thị danh sách chuyên ngành (Majors)
            //var majors = new List<SelectListItem>(); //cách 1
            //foreach (var item in db.Majors)
            //{
            //    majors.Add(new SelectListItem
            //    {
            //        Text = item.MajorName,

            //        Value = item.MajorID.ToString()
            //    });

            //}
            //ViewBag.MajorID = majors;
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); //cách 2
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")]Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //lại dùng 1 trong 2 cách tạo SelectList gửi về View để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }
        public IActionResult Edit(int id)
        {
            if(id==null||db.Learners==null)
                return NotFound();
            var learner = db.Learners.Find(id);
            if(learner==null)
                return NotFound();
            ViewBag.MajorId=new SelectList(db.Majors,"MajorID","MajorName",learner.MajorID);
            return View(learner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if(id!=learner.LearnerID)
                return NotFound();
            if(ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExits(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorId = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
                return View(learner);
        }
        private bool LearnerExits(int id)
        {   
            return (db.Learners?.Any(e => e.MajorID == id)).GetValueOrDefault();
        }
        public IActionResult Delete(int id)
        {
            if (id == null || db.Learners == null)
                return NotFound();
            var learner = db.Learners.Include(l=>l.Major).Include(e=>e.Enrollments).FirstOrDefault(m=>m.LearnerID==id);
            if (learner == null)
                return NotFound();
            if (learner.Enrollments.Count > 0)
                return Content("This learner can`t be delete");
            return View(learner);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
                return Problem("Enity set 'Learner' is null");
            var learner = db.Learners.Find(id);
            if(learner != null)
                db.Learners.Remove(learner);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
            }
}
