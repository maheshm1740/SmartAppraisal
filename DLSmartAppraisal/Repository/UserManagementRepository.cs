using DLSmartAppraisal.Abstract;
using DLSmartAppraisal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSmartAppraisal.Repository
{
    public class UserManagementRepository : IUserManagementRepositoryAdmin
    {
        private readonly UserContext _context;

        public UserManagementRepository(UserContext context)
        {
            _context = context;
        }

        public List<UserDetails> GetAllUserDetails()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public UserContext Get_context()
        {
            return _context;
        }

        public UserDetails GetUserByUserId(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public string AddUserDetails(UserDetails userDetails)
        {
            try
            {
                _context.Users.Add(userDetails);
                _context.SaveChanges();
                return "user added successfully";
            }
            catch (Exception ex)
            {
                return $"failed to add user: {ex.Message}";
            }
        }

        public List<UserDetails> GetUsersByRole(int roleId)
        {
            return _context.Users.Where(u => u.RoleId == roleId).OrderBy(u => u.Name).ToList();
        }
    }
}
