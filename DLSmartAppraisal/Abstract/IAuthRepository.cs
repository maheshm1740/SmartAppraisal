using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Abstract
{
    public interface IAuthRepository
    {
        UserDetails AuthenticateUser(UserViewModel user);

        UserDetails UpdatePassowrd(ChangePassword changePassword);
    }
}
