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
        private readonly IUserManagementRepositoryAdmin _repo;

        public BLUserManagement(IUserManagementRepositoryAdmin repo)
        {
            _repo = repo;
        }

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
            if (string.IsNullOrEmpty(userId)) return null;

            return _repo.GetUserByUserId(userId);
        }
        public string addUser(UserDetails user, string curId, string curRole)
        {
            if (user == null) return "invalid user data.";

            UserDetails curUser = GetUserByUserId(curId);

            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;
            user.PasswordChangeDate = null;
            user.CreatedBy = $"{curUser?.Name ?? "System"}({curRole})";
            user.ModifiedBy = "System";

            return _repo.AddUserDetails(user);
        }

        public List<UserDetails> GetAllcandidates(int roleId)
        {
            if (roleId <= 0) return null;

            return _repo.GetUsersByRole(roleId);
        }
    }
}
