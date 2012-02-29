using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult index(int id)
        {
            ViewBag.Topics = SubmissionDAL.GetAllByStudentID(33);


            return View();
        }

    }
}
