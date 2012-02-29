using System;
using System.Linq;
using HomeworkSubmission.DAL;

namespace UserTestData
{
    class PopulateDB
    {
        static void Main(string[] args)
        {
            //should take less than 10 seconds for generating default data
            //400 Academy Students
            //30 Courses
            //15 Topics per Course
            //10 Courses per student

            GenerateTestData.PopulateStudents(400);
            GenerateTestData.PopulateCourses(30);
            GenerateTestData.PopulateCoursesWithTopics(15);
            GenerateTestData.AddCoursesToStudents(10);
            GenerateTestData.PopulateAdminUsers(3);

            //its good that we have a default user for testing
            GenerateTestData.AddUser("test123", "test123");
  



        }
    }
}                                                                          