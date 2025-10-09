using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartAppraisal.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        BLUserManagement userBl = new BLUserManagement();

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            List<UserDetails> details = userBl.AllUsers();
            return View(details);
        }

        [HttpGet]
        [Authorize(Roles ="Admin,HR")]
        public IActionResult CreateUser()
        {
            BLRoleManagement roleManagement = new BLRoleManagement();
            DesigManagement desig = new DesigManagement();

            var desigs = desig.GetDesignations();
            var roles = roleManagement.AllRoles();

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
            if (ModelState.IsValid)
            {
                var curUserId = HttpContext.Session.GetString("curUserId");
                var curRole = HttpContext.Session.GetString("curRole");
                
                string message = userBl.addUser(userDetails,curUserId,curRole);
                ViewBag.Message = message;
                return RedirectToAction("Index");
            }

            BLRoleManagement roleManagement = new BLRoleManagement();
            var roles = roleManagement.AllRoles();

            DesigManagement desig = new DesigManagement();
            var desigs = desig.GetDesignations();

            ViewBag.DesigList = desigs.Select(d => new SelectListItem
            {
                Text = d.DesgName,
                Value = d.DesgId.ToString(),
                Selected = (d.DesgId == userDetails.DesignationId)
            }).ToList();

            ViewBag.RoleList = roles.Select(r => new SelectListItem
            {
                Text = r.RoleName,
                Value = r.RoleId.ToString(),
                Selected = (r.RoleId == userDetails.RoleId)
            }).ToList();

            return View(userDetails);
        }
    }
}
