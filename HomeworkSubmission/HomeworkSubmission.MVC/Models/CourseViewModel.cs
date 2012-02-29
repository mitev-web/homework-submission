using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Models
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public List<Topic> Topics { get; set; }

        public CourseViewModel(string name, int iD, bool isActive, List<Topic> topics)
        {
            this.Name = name;
            this.ID = iD;
            this.IsActive = isActive;
            this.Topics = topics;
        }

        public CourseViewModel(HomeworkSubmission.DAL.Cours course)
        {
            this.ID = course.ID;
            this.Name = course.Name;
            this.Topics = course.Topics.ToList();
            this.IsActive = course.IsActive;
        }
    }
}