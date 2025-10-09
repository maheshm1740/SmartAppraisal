using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Repository
{
    public class AuthRepository
    {
        UserContext userContext = new UserContext();

        public UserDetails AuthenticateUser(UserViewModel user)
        {
            if (user != null && !string.IsNullOrEmpty(user.UserId) && !string.IsNullOrEmpty(user.Password))
            {
                return userContext.Users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);
            }
            return null;
        }

        public UserDetails UpdatePassword(ChangePassword changePassword)
        {
            UserDetails authUser = AuthenticateUser(changePassword.usermodel);

            if(authUser !=null && changePassword.newPassword==changePassword.confirmPassword)
            {
                authUser.Password = changePassword.newPassword;
                authUser.PasswordChangeDate = DateTime.Now;
                userContext.SaveChanges();
            }

            return authUser;
        }
    }
}
