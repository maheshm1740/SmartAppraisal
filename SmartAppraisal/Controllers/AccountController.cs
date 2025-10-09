using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace SmartAppraisal.Controllers
{
    public class AccountController : Controller
    {
        BLlogin bl = new BLlogin();

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.UserId = HttpContext.Session.GetString("curUserId");
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (changePassword.usermodel == null)
            {
                ViewBag.Error = "User information missing.";
                return View(changePassword);
            }

            var userDetails = bl.AuthenticateUser(changePassword.usermodel);

            if (userDetails != null &&
                !string.IsNullOrEmpty(changePassword.newPassword) &&
                !string.IsNullOrEmpty(changePassword.confirmPassword))
            {
                if (changePassword.newPassword == changePassword.confirmPassword)
                {
                    bl.UpdatePassword(changePassword);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.Error = "Passwords do not match.";
                }
            }
            else
            {
                ViewBag.Error = "Invalid credentials or fields missing.";
            }

            return View(changePassword);
        }

    }
}
