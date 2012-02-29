using System;
using System.Linq;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Models
{
    public class TopicViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public CourseViewModel Course {get;set;}
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
  
        public TopicViewModel(int iD, string name, bool isActive,
            CourseViewModel course, DateTime? activeFrom, DateTime? activeTo)
        {
            this.ID = iD;
            this.Name = name;
            this.IsActive = isActive;
            this.Course = course;
            this.ActiveFrom = activeFrom;
            this.ActiveTo = activeTo;
        }


        public TopicViewModel(Topic topic)
        {
            this.ID = topic.ID;
            this.Name = topic.Name;
            this.IsActive = topic.IsActive;
            this.Course = new CourseViewModel(topic.Cours);
            this.ActiveFrom = topic.ActiveFrom;
            this.ActiveTo = topic.ActiveTo;
        }



    }
}