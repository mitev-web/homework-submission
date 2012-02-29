using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;

namespace HomeworkSubmission.DAL
{
    public class StudentDAL : DAO
    {
        /// <summary>
        /// Gets all Students
        /// </summary>
        /// <returns>Collection of Students</returns>
        public static IEnumerable<Student> GetAll()
        {
            db = new HomeworkSubmissionEntities();
            return db.Students;
        }

        /// <summary>
        /// Gets Student by ID.
        /// </summary>
        /// <param name="StudentID">The Student ID.</param>
        /// <returns>The Student with the same ID</returns>
        public static Student GetByID(int ID)
        {
            return db.Students.FirstOrDefault(x => x.ID == ID);
        }

        /// <summary>
        /// Gets the student by academy ID.
        /// </summary>
        /// <param name="academyID">The academy ID.</param>
        /// <returns></returns>
        public static Student GetByAcademyID(string academyID)
        {
            return db.Students.FirstOrDefault(x => x.AcademyID == academyID);
        }

        /// <summary>
        /// Adds student to course.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        public static void AddToCourse(Student student, Cours course)
        {
            student.Courses.Add(course);
            db.SaveChanges();
        }

        /// <summary>
        /// Creates the specified first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static Student Create(string firstName, string lastName, string email)
        {
            Student c = new Student();
            c.FirstName = firstName;
            c.LastName = lastName;
            c.Email = email;

            db.AddToStudents(c);
            db.SaveChanges();

            return c;
        }

        /// <summary>
        /// Gets the random student.
        /// </summary>
        /// <returns></returns>
        public static Student GetRandomStudent()
        {
            Random rand = new Random();
            int studentNumber = rand.Next(0, GetAll().Count() - 1);
            return GetByID(studentNumber);
        }

        /// <summary>
        /// Adds the academy ID.
        /// </summary>
        /// <param name="student">The student.</param>
        public static void AddAcademyID(Student student)
        {
            string code = (student.ID + "_" + student.FirstName.Substring(0, 2) +
                          student.LastName.Substring(0, 2)).ToUpper();
            student.AcademyID = code;
            db.SaveChanges();
        }

        /// <summary>
        /// Updates the specified Student.
        /// </summary>
        /// <param name="Student">The Student.</param>
        /// <param name="firstName">Student's first Name</param>
        /// <param name="lastName">The Student's last Name</param>
        public static void Update(Student student, string firstName, string lastName)
        {
            student.FirstName = firstName;
            student.LastName = lastName;
            db.SaveChanges();
        }

        /// <summary>
        /// Removes the Student by ID.
        /// </summary>
        /// <param name="StudentID">The Student ID.</param>
        public static void RemoveByID(int StudentID)
        {
            object StudentForDeletion;

            EntityKey StudentKey = new EntityKey("WebCalendarEntities.Students", "ID", StudentID);

            if (db.TryGetObjectByKey(StudentKey, out StudentForDeletion))
            {
                try
                {
                    db.DeleteObject(StudentForDeletion);
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                                                                      "The Student with an ID of '{0}' could not be deleted.\n" +
                                                                      "Make sure that any related objects are already deleted.\n",
                        StudentKey.EntityKeyValues[0].Value), ex);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                                                                  "The Student with an ID of '{0}' could not be found.\n" +
                                                                  "Make sure that Student exists.\n",
                    StudentKey.EntityKeyValues[0].Value));
            }
        }
    }
}