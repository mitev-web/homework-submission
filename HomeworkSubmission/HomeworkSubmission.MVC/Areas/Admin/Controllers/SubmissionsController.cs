using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.IO.Compression;
using Ionic.Zip;

namespace HomeworkSubmission.MVC.Areas.Admin.Controllers
{ 
    public class SubmissionsController : AdminController
    {


        [HttpGet]
        public ViewResult Index()
        {
            var submissions = db.Submissions.Include("Cours").Include("Student").Include("Topic");
            return View(submissions.ToList());
        }


        [HttpGet]
        public void GetAllForCourse(int courseID)
        {
            DateTime now = DateTime.Now;
            string date = now.Day + "." + now.Month +"." + now.Year;

            if (!Directory.Exists(Server.MapPath("~/Archive/") + "COURSE_" + courseID))
            {
                Directory.CreateDirectory(Server.MapPath("~/Archive/") + "COURSE_" + courseID);
            }

            string filePath = Server.MapPath("~/Archive/") + "COURSE_" + courseID + "\\" + date + ".zip";
            if (!System.IO.File.Exists(filePath)) {
            ZipFile zip = new ZipFile(filePath);
            string directory = Server.MapPath("~/Uploads/") + courseID + "_COURSE";
            zip.AddDirectory(directory);
            zip.Save();
            }

            string filename = "COURSE_" + courseID +"_"+date+".zip";
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
            Response.ContentType = "Application/zip";
            Response.WriteFile(filePath);
            Response.End();
        }




    }
}