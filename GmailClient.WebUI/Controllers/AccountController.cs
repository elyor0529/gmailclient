using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.UI;
using BotDetect.Web.UI.Mvc;
using GmailClient.Framework.Helpers;
using GmailClient.WebUI.BLL.Managers;
using GmailClient.WebUI.BLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GmailClient.WebUI.ViewModels;
using GmailClient.WebUI.BLL;
using System.Web.Security;
using GmailClient.WebUI.BLL.Controllers;

namespace GmailClient.WebUI.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
      
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
         
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!ValidateGmail(model.Email, model.Password))
            {
                ModelState.AddModelError("", "Gmail account doesn't exist");

                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            switch (result)
            {
                case SignInStatus.Success:
                    {
                        Session[Settings.KEYS.PSW_SESSION_KEY] = model.Password;

                        return RedirectToLocal(returnUrl);
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Incorrect CAPTCHA code!")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!ValidateGmail(model.Email, model.Password))
                {
                    ModelState.AddModelError("", "Gmail account doesn't exist");

                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);

                    MvcCaptcha.ResetCaptcha("SampleCaptcha");

                    Session[Settings.KEYS.PSW_SESSION_KEY] = model.Password;

                    return RedirectToAction("Index", "Mailbox");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Remove(Settings.KEYS.PSW_SESSION_KEY);

            return RedirectToAction("Index", "Home");
        } 
         
    }
}