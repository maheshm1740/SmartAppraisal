using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using DLSmartAppraisal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLSmartAppraisal
{
    public class BLUserManagement
    {
        IUserManagementRepositoryAdmin _repo = new UserManagementRepository();

        public List<UserDetails> AllUsers()
        {
            List<UserDetails> userDetails = _repo.GetAllUserDetails();

            if(userDetails!=null)
            {
                return userDetails;
            }

            return new List<UserDetails>();
        }
        public UserDetails GetUserByUserId(string userId)
        {
            if (userId != null)
            {
                return _repo.GetUserByUserId(userId);
            }

            return null;
        }
        public string addUser(UserDetails user, string curId, string curRole)
        {
            UserDetails curUser = GetUserByUserId(curId);

            user.CreatedDate = DateTime.Now;
            user.ModeifiedDate = DateTime.Now;
            user.PasswordChangeDate = null;
            user.CreatedBy = $"{curUser.Name} ({curRole})";
            user.ModeifiedBy = "Dummy";
           return _repo.AddUserDetails(user);
        }
    }
}
