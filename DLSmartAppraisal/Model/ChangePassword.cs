using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Model
{
    public class ChangePassword
    {
        public UserViewModel usermodel { get; set; }

        public string newPassword { get; set; }

        public string confirmPassword { get; set; }
    }
}
