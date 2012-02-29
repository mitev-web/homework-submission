using System;
using System.Collections.Generic;
using System.Linq;
using HomeworkSubmission.DAL;

namespace UserTestData
{
    class GenerateTestData
    {
        public static void PopulateAdminUsers(int numberOfUsers)
        {
            for (int i = 0; i < numberOfUsers; i++)
            {
                string username = Faker.NameFaker.FirstName() + Faker.StringFaker.Alpha(3) + Faker.NumberFaker.Number(20);

                User newUser = UserDAL.Create(username, username, Faker.StringFaker.Alpha(3) + Faker.InternetFaker.Email(), Faker.NameFaker.FirstName(), Faker.NameFaker.LastName());
            }
        }

        /// <summary>
        /// Creates a single admin user.
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="pass">password</param>
        public static void AddUser(string user, string pass)
        {
            UserDAL.Create(user, pass,
                Faker.StringFaker.Alpha(3) + Faker.InternetFaker.Email(),
                Faker.NameFaker.FirstName(), Faker.NameFaker.LastName());
        }

        /// <summary>
        /// Adds the courses to students.
        /// </summary>
        /// <param name="numberCourses">The number courses.</param>
        public static void AddCoursesToStudents(int numberCourses)
        {
            List<Student> students = StudentDAL.GetAll().ToList();

            foreach (Student student in students)
            {
                foreach (Cours course in GetRandomCourse(numberCourses))
                {
                    CourseDAL.AddtoStudent(student, course);
                }
            }
        }
  
        /// <summary>
        /// Gets random courses.
        /// </summary>
        /// <param name="numberCourses">The number courses.</param>
        /// <returns></returns>
        private static List<Cours> GetRandomCourse(int numberCourses)
        {
            List<Cours> courses = CourseDAL.GetAll().ToList();
            List<Cours> randomCourses = new List<Cours>();

            Random rand = new Random();
            for (int i = 0; i < numberCourses; i++)
            {
                Cours nextRandomCourse = courses[rand.Next(0, courses.Count - 1)];
                if (!randomCourses.Contains(nextRandomCourse))
                {
                    randomCourses.Add(nextRandomCourse);
                }
            }

            return randomCourses;
        }

        /// <summary>
        /// Populates the courses with students.
        /// </summary>
        /// <param name="numberStudents">The number students.</param>
        public static void PopulateCoursesWithStudents(int numberStudents)
        {
            if (numberStudents > StudentDAL.GetAll().Count())
            {
                Console.WriteLine("not enough students to populate courses");
                return;
            }
            List<Cours> courses = CourseDAL.GetAll().ToList();

            foreach (Cours course in courses)
            {
                foreach (Student student in GetRandomStudents(numberStudents))
                {
                    CourseDAL.AddtoStudent(student, course);
                }
            }
        }

        /// <summary>
        /// Gets the random students.
        /// </summary>
        /// <param name="numberStudents">The number students.</param>
        /// <returns></returns>
        public static List<Student> GetRandomStudents(int numberStudents)
        {
            List<Student> students = StudentDAL.GetAll().ToList();
            List<Student> randomStudents = new List<Student>();

            Random rand = new Random();
            for (int i = 0; i < numberStudents; i++)
            {
                Student nextRandomStudent = students[rand.Next(0, numberStudents)];
                if (!randomStudents.Contains(nextRandomStudent))
                {
                    randomStudents.Add(nextRandomStudent);
                }
            }

            return randomStudents;
        }

        /// <summary>
        /// Populates the students.
        /// </summary>
        /// <param name="numberStudents">The number students.</param>
        public static void PopulateStudents(int numberStudents)
        {
            for (int i = 0; i < numberStudents; i++)
            {
                string firstName = Faker.NameFaker.FirstName();
                string lastName = Faker.NameFaker.LastName();
                string email = Faker.StringFaker.Alpha(3) + Faker.InternetFaker.Email();

                Student student = StudentDAL.Create(firstName, lastName, email);
                StudentDAL.AddAcademyID(student);
            }
        }

        /// <summary>
        /// Populates the courses.
        /// </summary>
        /// <param name="numberCourses">The number courses.</param>
        public static void PopulateCourses(int numberCourses)
        {
            for (int i = 0; i < numberCourses; i++)
            {
                string name = UppercaseFirst(Faker.CompanyFaker.BS());
                bool isActive = true;

                CourseDAL.Create(name, isActive);
            }
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static List<Topic> GenerateTopics(int numberTopics)
        {
            List<Topic> topics = new List<Topic>();

            for (int i = 0; i < numberTopics; i++)
            {
                Topic t = new Topic();
                DateTime startDate = Faker.DateTimeFaker.DateTimeBetweenMonths(2, 3);
                DateTime endDate = Faker.DateTimeFaker.DateTimeBetweenMonths(3, 5);
                bool isActive = true;
                string name = UppercaseFirst(Faker.CompanyFaker.BS());

                t.ActiveFrom = startDate;
                t.ActiveTo = endDate;
                t.IsActive = isActive;
                t.Name = name;

                topics.Add(t);
            }
            return topics;
        }

        /// <summary>
        /// Populates the courses with topics.
        /// </summary>
        /// <param name="numberTopics">The number of topics.</param>
        public static void PopulateCoursesWithTopics(int numberTopics)
        {
            List<Cours> courses = HomeworkSubmission.DAL.CourseDAL.GetAll().ToList();
            foreach (Cours course in courses)
            {
                foreach (Topic topic in GenerateTopics(numberTopics))
                {
                    TopicDAL.AddToCourse(topic, course);
                }
            }
        }
    }
}