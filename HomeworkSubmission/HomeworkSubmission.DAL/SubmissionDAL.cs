using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeworkSubmission.DAL
{
    public class SubmissionDAL : DAO
    {
        /// <summary>
        /// Gets all Submissions
        /// </summary>
        /// <returns>Collection of Submissions</returns>
        public static IEnumerable<Submission> GetAll()
        {
            return db.Submissions;
        }

        /// <summary>
        /// Gets all Submissions by student ID.
        /// </summary>
        /// <param name="studentID">The student ID.</param>
        /// <returns></returns>
        public static IEnumerable<Submission> GetAllByStudentID(int studentID)
        {
            return db.Submissions.Where(x => x.StudentID == studentID);
        }

        /// <summary>
        /// Gets Submission by ID.
        /// </summary>
        /// <param name="coursID">The Submission ID.</param>
        /// <returns>The Submission with the same ID</returns>
        public static Submission GetByID(int courseID)
        {
            Submission course = (from c in db.Submissions
                                 where c.ID == courseID
                                 select c).
            FirstOrDefault();

            return course;
        }

        /// <summary>
        /// Creates the specified Submission.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <param name="topic">The topic.</param>
        /// <param name="uploadDate">The upload date.</param>
        /// <param name="Extension">Type of the MIME.</param>
        /// <param name="fileData">The file data.</param>
        public static void Create(Student student, Topic topic, Cours course, DateTime uploadDate, string Extension, string filePath)
        {
            Submission s = new Submission();
            s.Student = student;
            s.Cours = course;
            s.Topic = topic;
            s.UploadDate = uploadDate;
            s.Extension = Extension;
            s.FilePath = filePath.Trim();
            db.Submissions.AddObject(s);
            db.SaveChanges();
        }

        /// <summary>
        /// Check if submission exists
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static bool Exists(string filePath)
        {
            if (db.Submissions.Count(x => x.FilePath == filePath) > 0)
            {
                return true;
            }else
            {
                return false;
        	}
        }

        /// <summary>
        /// Updates the specified submission.
        /// </summary>
        /// <param name="submission">The submission.</param>
        /// <param name="uploadDate">The upload date.</param>
        /// <param name="fileType">Type of the file.</param>
        public static void Update(Submission submission, DateTime uploadDate, string fileType)
        {
            submission.UploadDate = uploadDate;
            submission.Extension = fileType;
            db.SaveChanges();
        }

        /// <summary>
        /// Gets submission the by file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static Submission GetByFilePath(string filePath)
        {
            return db.Submissions.FirstOrDefault(x => x.FilePath == filePath);
        }

        /// <summary>
        /// Adds the submissions.
        /// </summary>
        /// <param name="submissions">The submissions.</param>
        public static void AddSubmissions(List<Submission> submissions)
        {
            foreach (Submission submission in submissions)
            {
                db.AddToSubmissions(submission);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Adds submission to topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="submission">The submission.</param>
        public static void AddToTopic(Topic topic, Submission submission)
        {
            submission.Topic = topic;
            db.SaveChanges();
        }
    }
}