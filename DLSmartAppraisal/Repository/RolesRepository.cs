
using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Repository
{
    public class RolesRepository:IRoleRepository
    {
        RoleContext context = new RoleContext();

        public List<RoleDetails> GetRoles()
        {
            return context.roles.ToList();
        }

        public int GetRoleId(string roleName)
        {
            var role = context.roles.FirstOrDefault(r => r.RoleName == roleName);

            return role!=null ? role.RoleId : 0;
        }
    }
}
