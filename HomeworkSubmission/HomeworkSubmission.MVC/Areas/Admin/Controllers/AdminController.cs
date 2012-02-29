using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public HomeworkSubmissionEntities db = new HomeworkSubmissionEntities();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}