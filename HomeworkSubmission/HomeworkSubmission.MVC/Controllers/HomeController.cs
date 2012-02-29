using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeworkSubmission.DAL;
using HomeworkSubmission.MVC.Models;

namespace HomeworkSubmission.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Response.Redirect("Home/Home?AcademyID=10_DADA");
            ViewBag.RandomID = StudentDAL.GetRandomStudent().AcademyID;

            return View();
        }



        public ActionResult Home(int courses=0, string academyID="")
        {
            try
            {
                Student student;
                if (academyID.Length != 0)
                {
                    student = StudentDAL.GetByAcademyID(academyID);
                }
                else
                {
                    student = StudentDAL.GetByAcademyID(Session["AcademyID"].ToString());
                }
                ViewBag.Hello = "Hello " + student.FirstName + " " + student.LastName;
                ViewBag.CourseID = courses;

                if (courses == 0)
                {
                    courses = student.Courses.FirstOrDefault().ID;
                }

                List<Topic> topics = TopicDAL.GetAllActive(CourseDAL.GetTopicsByID(courses)).ToList();
                List<TopicViewModel> topicViewModels = new List<TopicViewModel>();

                foreach (Topic topic in topics)
                    topicViewModels.Add(new TopicViewModel(topic));

                int counter = 0;
                foreach (TopicViewModel tvm in topicViewModels)
                    tvm.Name = (++counter).ToString() + ". " + tvm.Name;
                
                //topics data for dropdown
                ViewBag.Topics = topicViewModels;

                //loading student to pass to view
                StudentViewModel studentViewModel = new StudentViewModel(student);

                Session["academyID"] = studentViewModel.AcademyID;
                return View(studentViewModel);
            }
            catch (Exception)
            {
                ViewBag.Message = "Invalid Academy ID";
                return View("Index");
            }
        }
    }
}