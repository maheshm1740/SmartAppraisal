using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartAppraisal.Controllers
{
    [Authorize]
    public class CompetencyController : Controller
    {
        private readonly BlCompetency _competencyBl;
        private readonly BLRoleManagement _roleBl;

        public CompetencyController(BlCompetency blCompetency, BLRoleManagement blRole)
        {
            _competencyBl = blCompetency;
            _roleBl = blRole;
        }


        [HttpGet]
        [Authorize(Roles ="Admin,HR")]
        public IActionResult Index()
        {
            var competencies = _competencyBl.GetAllCompetencies();
            return View(competencies);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCompetency()
        {
            var roles = _roleBl.AllRoles();
            ViewBag.RoleList = roles.Select(r => new SelectListItem
            {
                Text = r.RoleName,
                Value = r.RoleId.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCompetency(CompetencyDetails competency)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(competency);
                return View(competency);
            }
            var curUser = HttpContext.Session.GetString("curUserId");
            competency.CreatedBy = curUser;
            competency.CreatedDate = DateTime.Now;
            var result = _competencyBl.AddCompetency(competency);

            if (result.Contains("Successfully"))
            {
                TempData["Success"] = result;
                return RedirectToAction("Index");
            }

            ViewBag.Error = result;
            LoadDropdowns(competency);
            return View(competency);
        }

        private void LoadDropdowns(CompetencyDetails details)
        {
            ViewBag.RoleList = _roleBl.AllRoles()
                .Select(r => new SelectListItem
                {
                    Text = r.RoleName,
                    Value = r.RoleId.ToString(),
                    Selected = r.RoleId == details.RoleId
                }).ToList();
        }
    }
}
