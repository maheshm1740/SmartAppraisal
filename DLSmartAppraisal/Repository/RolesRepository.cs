
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
        private readonly RoleContext _context;

        public RolesRepository(RoleContext context)
        {
            _context = context;
        }

        public List<RoleDetails> GetRoles()
        {
            return _context.roles.ToList();
        }

        public int GetRoleId(string roleName)
        {
            var role = _context.roles.FirstOrDefault(r => r.RoleName == roleName);

            return role!=null ? role.RoleId : 0;
        }
    }
}
