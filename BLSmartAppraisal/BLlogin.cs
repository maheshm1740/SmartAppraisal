using DLSmartAppraisal.Model;
using DLSmartAppraisal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLSmartAppraisal
{
    public class BLlogin
    {
        private readonly AuthRepository _repo;

        public BLlogin(AuthRepository repo)
        {
            _repo = repo;
        }

       public UserDetails AuthenticateUser(UserViewModel user)
        {
            UserDetails authUser = _repo.AuthenticateUser(user);

            if (authUser == null)
            {
                return null;
            }

            return authUser;
        }

        public UserDetails UpdatePassword(ChangePassword changePassword)
        {
            if (changePassword != null && !string.IsNullOrEmpty(changePassword.newPassword))
            {
                if (changePassword.newPassword != changePassword.OldPassword && changePassword.newPassword == changePassword.confirmPassword)
                {
                    return _repo.UpdatePassword(changePassword);
                }
            }
            return null;
        }
    }
}
