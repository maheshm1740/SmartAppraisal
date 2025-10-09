using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Repository
{
    public class UserManagementRepository:IUserManagementRepositoryAdmin
    {
        UserContext userContext = new UserContext();

        public List<UserDetails> GetAllUserDetails()
        {
            return userContext.Users.ToList();
        }

        public UserDetails GetUserByUserId(string userId)
        {
            return userContext.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public string AddUserDetails(UserDetails userDetails)
        {
            try
            {
                userContext.Add(userDetails);
                userContext.SaveChanges();
                return "UserDetails Added";
            }
            catch
            {
                return "Failed to add userDetails";
            }
        }
    }
}
