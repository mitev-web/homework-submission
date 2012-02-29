using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Areas.Admin.Controllers
{

    public class TopicsController : AdminController
    {
        private HomeworkSubmissionEntities db = new HomeworkSubmissionEntities();

      
        public ViewResult Index(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            ViewBag.Count = db.Topics.Count();
            ViewBag.Start = start;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.OrderBy = orderBy;
            ViewBag.Desc = desc;

            return View();
        }

        //
        // GET: /Topic/GridData/?start=0&itemsPerPage=20&orderBy=ID&desc=true

        public ActionResult GridData(int start = 0, int itemsPerPage = 20, string orderBy = "ID", bool desc = false)
        {
            Response.AppendHeader("X-Total-Row-Count", db.Topics.Count().ToString());
            ObjectQuery<Topic> topics = db.Topics.Include("Cours");
            topics = topics.OrderBy("it." + orderBy + (desc ? " desc" : ""));

            return PartialView(topics.Skip(start).Take(itemsPerPage));
        }

        //
        // GET: /Default5/RowData/5

        public ActionResult RowData(int id)
        {
            Topic topic = db.Topics.Single(t => t.ID == id);
            return PartialView("GridData", new Topic[] { topic });
        }

        //
        // GET: /Topic/Create

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Name");
            return PartialView("Edit");
        }

        //
        // POST: /Topic/Create

        [HttpPost]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.AddObject(topic);
                db.SaveChanges();
                return PartialView("GridData", new Topic[] { topic });
            }

            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Name", topic.CourseID);
            return PartialView("Edit", topic);
        }

        //
        // GET: /Topic/Edit/5

        public ActionResult Edit(int id)
        {
            Topic topic = db.Topics.Single(t => t.ID == id);
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Name", topic.CourseID);
            return PartialView(topic);
        }

        //
        // POST: /Topic/Edit/5

        [HttpPost]
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Attach(topic);
                db.ObjectStateManager.ChangeObjectState(topic, EntityState.Modified);
                db.SaveChanges();
                return PartialView("GridData", new Topic[] { topic });
            }

            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Name", topic.CourseID);
            return PartialView(topic);
        }


    }
}
