using System;
using System.Linq;

namespace HomeworkSubmission.DAL
{
    public class DAO
    {
        public static HomeworkSubmissionEntities db;

        static DAO()
        {
            
            db = new HomeworkSubmissionEntities();
        }
    }
}