using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SmartAppraisal.Controllers
{
    public class LoginController : Controller
    {
        BLlogin blLogin = new BLlogin();

        RoleContext _role = new RoleContext();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View();

            UserDetails userDetails = blLogin.AuthenticateUser(user);

            if(userDetails==null)
            {
                ViewBag.Error = "Invalid UserId or password";

                return View();
            }

            HttpContext.Session.SetString("curUser", userDetails.UserId);
             
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
            
            HttpContext.Session.SetString("curUserId", userDetails.UserId);

            if (userDetails.PasswordChangeDate==null)
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
