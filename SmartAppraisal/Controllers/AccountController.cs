using BLSmartAppraisal;
using DLSmartAppraisal.Model;
using Microsoft.AspNetCore.Mvc;

namespace SmartAppraisal.Controllers
{
    public class AccountController : Controller
    {
        private readonly BLlogin _login;

        public AccountController(BLlogin login)
        {
            _login = login;
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.UserId = HttpContext.Session.GetString("curUserId");
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword model)
        {
            ViewBag.UserId = model.UserId;

            if (string.IsNullOrEmpty(model.UserId))
            {
                ViewBag.ErrorMessage = "User information missing.";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.OldPassword))
            {
                ViewBag.ErrorMessage = "Old password is required.";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.newPassword) || string.IsNullOrEmpty(model.confirmPassword))
            {
                ViewBag.ErrorMessage = "New password and confirm password are required.";
                return View(model);
            }

            if (model.newPassword != model.confirmPassword)
            {
                ViewBag.ErrorMessage = "New password and confirm password do not match.";
                return View(model);
            }

            if (model.newPassword == model.OldPassword)
            {
                ViewBag.ErrorMessage = "New password cannot be the same as old password.";
                return View(model);
            }

            var result = _login.UpdatePassword(model);

            if (result != null)
            {
                ViewBag.SuccessMessage = "Password changed successfully! Redirecting to login...";
                ViewBag.Redirect = true;
                return View(model);
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials. Please check your old password.";
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Index", "Login");
        }
    }
}
