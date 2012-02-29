using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Areas.Admin.Controllers
{
    public class CoursesController : AdminController
    {
        //
        // GET: /Admin/Courses/
        public ViewResult Index()
        {
            return View(db.Courses.ToList());
        }

        //
        // GET: /Admin/Courses/Details/5

        public ViewResult Details(int id)
        {
            Cours cours = db.Courses.Single(c => c.ID == id);
            return View(cours);
        }

        //
        // GET: /Admin/Courses/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Courses/Create

        [HttpPost]
        public ActionResult Create(Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.AddObject(cours);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(cours);
        }
        
        //
        // GET: /Admin/Courses/Edit/5
 
        public ActionResult Edit(int id)
        {
            Cours cours = new Cours();
            try
            {
                cours = db.Courses.Single(c => c.ID == id);
            }
            catch (Exception)
            {
            }
            return View(cours);
        }

        //
        // POST: /Admin/Courses/Edit/5

        [HttpPost]
        public ActionResult Edit(Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Attach(cours);
                db.ObjectStateManager.ChangeObjectState(cours, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cours);
        }


    }
}