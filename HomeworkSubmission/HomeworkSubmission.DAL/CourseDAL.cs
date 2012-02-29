using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HomeworkSubmission.DAL
{
    public class CourseDAL : DAO
    {
        /// <summary>
        /// Gets the course name by ID.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string GetNameByID(int ID)
        {
            return db.Courses.FirstOrDefault(x => x.ID == ID).Name;
        }

        /// <summary>
        /// Gets all Courses
        /// </summary>
        /// <returns>Collection of Courses</returns>
        public static IEnumerable<Cours> GetAll()
        {
          return  db.Courses;
        }

        /// <summary>
        /// Gets all active courses.
        /// </summary>
        /// <returns>collection of courses</returns>
        public static IEnumerable<Cours> GetAllActive(IEnumerable<Cours> courses)
        {
            return db.Courses.Where(x => x.IsActive == true);
        }

        /// <summary>
        /// Gets topics for this course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        public static IEnumerable<Topic> GetTopicsByID(int ID)
        {
            return db.Courses.FirstOrDefault(x => x.ID == ID).Topics;
        }

        /// <summary>
        /// Gets Course by ID.
        /// </summary>
        /// <param name="coursID">The course ID.</param>
        /// <returns>The Course with the same ID</returns>
        public static Cours GetByID(int courseID)
        {
            return db.Courses.FirstOrDefault(x => x.ID == courseID);
        }

        public static void AddStudentToCourse(Student student, Cours course)
        {
            course.Students.Add(student);
            db.SaveChanges();
        }

        /// <summary>
        /// Creates new Cours
        /// </summary>
        /// <param name="FirstName">FirstName</param>
        /// <param name="LastName">LastName</param>
        public static void Create(string name, bool isActive)
        {
            Cours c = new Cours();
            c.Name = name;
            c.IsActive = isActive;

            db.AddToCourses(c);
            db.SaveChanges();
        }

        /// <summary>
        /// Updates the specified Cours.
        /// </summary>
        /// <param name="Cours">The Course.</param>
        /// <param name="name">Course's Name</param>
        /// <param name="isActive">The Course's state</param>
        public static void Update(Cours course, string name, bool isActive)
        {
            course.Name = name;
            course.IsActive = isActive;
            db.SaveChanges();
        }

        /// <summary>
        /// Addtoes the student.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        public static void AddtoStudent(Student student, Cours course)
        {
            if (!course.Students.Contains(student))
                course.Students.Add(student);
    
            db.SaveChanges();
        }

        /// <summary>
        /// Removes the Cours by ID.
        /// </summary>
        /// <param name="CoursID">The Cours ID.</param>
        public static void RemoveByID(int courseID)
        {
            object courseForDeletion;

            EntityKey CoursKey = new EntityKey("WebCalendarEntities.Courss", "ID", courseID);

            if (db.TryGetObjectByKey(CoursKey, out courseForDeletion))
            {
                try
                {
                    db.DeleteObject(courseForDeletion);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                                                                      "The Course with an ID of '{0}' could not be deleted.\n" +
                                                                      "Make sure that any related objects are already deleted.\n",
                        CoursKey.EntityKeyValues[0].Value), ex);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                                  "The Course with an ID of '{0}' could not be found.\n" +
                                                                  "Make sure that Cours exists.\n",
                    CoursKey.EntityKeyValues[0].Value));
            }
        }
    }
}