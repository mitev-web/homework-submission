using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;
using HomeworkSubmission.MVC;
using HomeworkSubmission.DAL;

namespace HomeworkSubmission.MVC.Models
{
    public class UserModel : DAO
    {
        public MembershipUser CreateUser(string username, string password, string email)
        {
            User u = new User();

            u.UserName = username;
            u.Email = email;
            u.PasswordSalt = UserDAL.CreateSalt();
            u.Password = UserDAL.CreatePasswordHash(password, u.PasswordSalt);
            db.AddToUsers(u);
            db.SaveChanges();

            return CreateMembershipUser(u.UserName);
        }

        public static bool UserIsValid(string user, string pass)
        {
            return UserDAL.UserIsValid(user, pass);
        }

        public static string GetUserNameByEmail(string email)
        {
            return UserDAL.GetUserNameByEmail(email);
        }

        public MembershipUser CreateMembershipUser(string username)
        {
            var result = from u in DAO.db.Users where (u.UserName == username) select u;

            if (result.Count() != 0)
            {
                var dbuser = result.FirstOrDefault();

                string _username = dbuser.UserName;
                int _providerUserKey = dbuser.ID;
                string _email = dbuser.Email;
                string _passwordQuestion = "";
                string _comment = "";
                bool _isApproved = true;
                bool _isLockedOut = false;
                DateTime _creationDate = DateTime.Now;
                DateTime _lastLoginDate = DateTime.Now;
                DateTime _lastActivityDate = DateTime.Now;
                DateTime _lastPasswordChangedDate = DateTime.Now;
                DateTime _lastLockedOutDate = DateTime.Now;

                MembershipUser user = new MembershipUser("CustomMembershipProvider",
                    _username,
                    _providerUserKey,
                    _email,
                    _passwordQuestion,
                    _comment,
                    _isApproved,
                    _isLockedOut,
                    _creationDate,
                    _lastLoginDate,
                    _lastActivityDate,
                    _lastPasswordChangedDate,
                    _lastLockedOutDate);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}