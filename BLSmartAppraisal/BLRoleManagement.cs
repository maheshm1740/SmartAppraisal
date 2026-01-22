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
    public class BLRoleManagement
    {
        private readonly IRoleRepository _repo;

        public BLRoleManagement(IRoleRepository repo)
        {
            _repo = repo;
        }

        public List<RoleDetails> AllRoles()
        {
            List<RoleDetails> roles = _repo.GetRoles();

            if (roles != null) return roles;

            return new List<RoleDetails>();
        }

        public int GetRoleIdByName(string roleName)
        {
            return _repo.GetRoleId(roleName);
        }
    }
}
