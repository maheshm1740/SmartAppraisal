using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmartAppraisal.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly BLUserManagement _userBl;
        private readonly BLRoleManagement _roleBl;
        private readonly DesigManagement _desigBl;

        public UserManagementController( BLUserManagement userBl, BLRoleManagement roleBl, DesigManagement desigBl)
        {
            _userBl = userBl;
            _roleBl = roleBl;
            _desigBl = desigBl;
        }

        [Authorize(Roles ="Admin, HR")]
        public IActionResult Index()
        {
            List<UserDetails> details = _userBl.AllUsers();
            return View(details);
        }

        [HttpGet]
        [Authorize(Roles ="Admin,HR")]
        public IActionResult CreateUser()
        {

            var desigs = _desigBl.GetDesignations();
            var roles = _roleBl.AllRoles();

            ViewBag.DesigList = desigs.Select(d => new SelectListItem
            {
                Text=d.DesgName,
                Value=d.DesgId.ToString()
            }).ToList();

            ViewBag.RoleList = roles.Select(r => new SelectListItem
            {
                Text = r.RoleName,
                Value = r.RoleId.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, HR")]
        public IActionResult CreateUser(UserDetails userDetails)
        {
            if(!ModelState.IsValid)
            {
                LoadDropdowns(userDetails);
                return View(userDetails);
            }

            var curUserId = HttpContext.Session.GetString("curUserId");
            var curRole = HttpContext.Session.GetString("curRole");

            string message = _userBl.addUser(userDetails, curRole, curUserId);
            TempData["Message"] = message;

            return RedirectToAction("Index");
        }

        private void LoadDropdowns(UserDetails userDetails)
        {
            ViewBag.DesigList = _desigBl.GetDesignations()
                .Select(d => new SelectListItem
                {
                    Text = d.DesgName,
                    Value = d.DesgId.ToString(),
                    Selected = d.DesgId == userDetails.DesignationId
                }).ToList();

            ViewBag.RoleList = _roleBl.AllRoles()
                .Select(r => new SelectListItem
                {
                    Text = r.RoleName,
                    Value = r.RoleId.ToString(),
                    Selected = r.RoleId == userDetails.RoleId
                }).ToList();
        }

        public IActionResult DisplayUser()
        {
            var cur = HttpContext.Session.GetString("curUserId");

            var user = _userBl.GetUserByUserId(cur);

            return View(user);
        }
    }
}
