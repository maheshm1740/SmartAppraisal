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
        private readonly UserContext _context;

        public AuthRepository(UserContext context)
        {
            _context = context;
        }

        public UserDetails AuthenticateUser(UserViewModel user)
        {
            if (user != null && !string.IsNullOrEmpty(user.UserId) && !string.IsNullOrEmpty(user.Password))
            {
                return _context.Users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);
            }
            return null;
        }

        public UserDetails UpdatePassword(ChangePassword changePassword)
        {
            if (changePassword != null && !string.IsNullOrEmpty(changePassword.newPassword))
            {   
                if (changePassword.newPassword!=changePassword.OldPassword && changePassword.newPassword == changePassword.confirmPassword)
                {                    
                    var userView = new UserViewModel
                    {
                        UserId = changePassword.UserId,
                        Password = changePassword.OldPassword
                    };

                    UserDetails authUser = AuthenticateUser(userView);

                    if (authUser != null)
                    {
                        authUser.Password = changePassword.newPassword;
                        authUser.PasswordChangeDate = DateTime.Now;
                        _context.SaveChanges();
                        return authUser;
                    }
                }
            }
            return null;
        }
    }
}
