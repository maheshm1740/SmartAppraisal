using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Abstract
{
    public interface IUserManagementRepositoryAdmin
    {
        List<UserDetails> GetAllUserDetails();

        string AddUserDetails(UserDetails userDetails);

        UserDetails GetUserByUserId(string userId);
    }
}
