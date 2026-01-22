using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SmartAppraisal.Controllers
{
    public class LoginController : Controller
    {
        private readonly BLlogin _login;

        private readonly RoleContext _role;

        private readonly BLAssessment _blAss;

        public LoginController(RoleContext role, BLlogin login, BLAssessment blass)
        {
            _role = role;
            _login = login;
            _blAss = blass;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel user)
        {
            if (!ModelState.IsValid)
            { 
                return View(); 
            }

            UserDetails userDetails = _login.AuthenticateUser(user);

            if(userDetails==null)
            {
                ViewBag.ErrorMessage = "Invalid userId or password";

                return View();
            }

            HttpContext.Session.SetString("curUser", userDetails.UserId);
            HttpContext.Session.SetString("curUserId", userDetails.UserId);
            HttpContext.Session.SetString("curUserName", userDetails.Name);

            var claims = new List<Claim>();
            
            claims.Add(new Claim("username", userDetails.UserId));
          
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userDetails.UserId));


             
            if(userDetails.RoleId>0)
            {
                var role = _role.roles.FirstOrDefault(r => r.RoleId == userDetails.RoleId);        

                if(role!=null && !string.IsNullOrEmpty(role.RoleName))               
                {   
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                    HttpContext.Session.SetString("curRole", role.RoleName);
                    HttpContext.Session.SetInt32("curRoleId", role.RoleId);
                }
            }

             
            var claimsIdetify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
             
            var authProperties = new AuthenticationProperties 
            {              
                IsPersistent = true,
                 
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)      
            };

              
            await HttpContext.SignInAsync(               
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdetify), authProperties);

            if (userDetails.PasswordChangeDate==null)
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            
            if(HttpContext.Session.GetString("curRole") == "Candidate")
            {
                var assessment = _blAss.GetUserActiveAssessments(userDetails.UserId);

                if(assessment != null)
                { 
                    var assessmentCompletion = _blAss.GetAssessmentCompletion(userDetails.UserId, assessment.AssesmentId);
                    
                    if (!assessmentCompletion.IsCompleted)
                    {
                        return RedirectToAction("Instruction", "Assessment");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
