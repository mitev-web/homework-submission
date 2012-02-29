using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Models
{
    public class StudentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AcademyID { get; set; }
        public List<CourseViewModel> Courses { get; set; }
        public List<Submission> Submissions { get; set; }



        public StudentViewModel(string firstName, string lastName, string academyID, List<CourseViewModel> courses, List<Submission> submissions)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AcademyID = academyID;
            this.Courses = courses;
            this.Submissions = submissions;
        }

        public StudentViewModel(HomeworkSubmission.DAL.Student student)
        {
            this.FirstName = student.FirstName;
            this.LastName = student.LastName;
            this.AcademyID = student.AcademyID;
            this.Courses = new List<CourseViewModel>();
            foreach (Cours course in student.Courses)
            {
                this.Courses.Add(new CourseViewModel(course));
            }
            this.Submissions = student.Submissions.ToList();
        }
    }
}