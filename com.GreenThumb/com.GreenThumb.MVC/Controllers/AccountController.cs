﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using com.GreenThumb.MVC.Models;
using com.GreenThumb.BusinessLogic;
using System.Reflection;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    if (CheckDbUser(model.UserName, password:model.Password))
                    {
                        Session.Add("MigrateUser", model.UserName);

                        return RedirectToAction("MigrateUser", "Account");
                    }

                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
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
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName 
                        = model.UserName, 
                    Email 
                        = model.Email,
                    FirstName
                        = model.FirstName,
                    LastName
                        = model.LastName
                };

                if (!CheckDbUser(model.UserName))
                {
                    if (new UserManager().AddNewUserPasswordChange(new User() 
                    {
                        UserName
                            = model.UserName,
                        EmailAddress 
                            = model.Email,
                        Zip
                            = Request["ZipCode"],
                        FirstName
                            = model.FirstName,
                        LastName 
                            = model.LastName
                    }, model.Password))
                    {
                        var result = await UserManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, user.FirstName));
                            UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Surname, user.LastName));

                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                            // Send an email with this link
                            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                            return RedirectToAction("Index", "Home");
                        }

                        AddErrors(result);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Opps! There was a problem processing on our end.");
                    }

                    
                }
                else
                {
                    ModelState.AddModelError("", "Username is not available.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// GET for re-registering a user that exists in the GreenThumbGardens database.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Username to reregister with.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult MigrateUser()
        {
            string userName = Session["MigrateUser"].ToString();

            if (String.IsNullOrEmpty(userName))
            {
                return View("Error");
            }

            var model = new ReRegisterViewModel() 
            {
                UserName = userName
            };

            return View(model);
        }

        /// <summary>
        /// POST for re-registering a user that exists in the GreenThumbGardens database.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="model">Contains the values from the form.</param>
        /// <returns>View that is the result from the model.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MigrateUser(ReRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager();
                    
                bool flag = userManager.ConfirmUserInfo(model.UserName, model.Email, model.Password);

                if (flag)
                {
                    com.GreenThumb.BusinessObjects.User oldUser = null;

                    try
                    {
                        oldUser = userManager.GetUserByUserName(model.UserName);
                    }
                    catch (Exception)
                    {
                        return View("Error");
                    }

                    if (null != oldUser)
                    {

                        var user = new ApplicationUser
                        {
                            UserName
                                = model.UserName,
                            Email
                                = model.Email,
                            FirstName
                                = oldUser.FirstName,
                            LastName
                                = oldUser.LastName
                        };

                        var result = await UserManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, user.FirstName));
                            UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Surname, user.LastName));

                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                            Session.Remove("MigrateUser");

                            return RedirectToAction("Index", "Home");
                        }

                        AddErrors(result);

                        if (result.Errors.Any(c => c.Contains("Passwords")) && 1 == result.Errors.Count()) // If the only error is invalid password, redirect to password change...
                        {
                            Session.Add("MigrationNewPassword", oldUser);
                            Session.Add("PasswordError", new 
                            { 
                                OldValue 
                                    = model.Password, 
                                ErrorMessage 
                                    = result.Errors.ElementAt(0) 
                            });

                            Session.Remove("MigrateUser");

                            return RedirectToAction("MigrationNewPassword", "Account");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Information could not be retrieved.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Information was not correct.");
                }
            }

            ViewBag.UserName = model.UserName;

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// GET for changing the password of a user whose password does not meet new rules.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult MigrationNewPassword()
        {
            if (null != Session["MigrationNewPassword"])
            {
                com.GreenThumb.BusinessObjects.User oldUser =
                    (com.GreenThumb.BusinessObjects.User)Session["MigrationNewPassword"];

                var model = new ResetPasswordViewModel() 
                {
                    UserName = oldUser.UserName
                };

                ViewBag.PasswordError = RetrievePasswordErrorValue("ErrorMessage");

                return View(model);
            }

            return View("Error");
            
        }

        /// <summary>
        /// POST for changing the password of a user whose password does not meet new rules.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MigrationNewPassword(ResetPasswordViewModel model)
        {
            if (null == Session["PasswordError"])
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                com.GreenThumb.BusinessObjects.User oldUser =
                    (com.GreenThumb.BusinessObjects.User)Session["MigrationNewPassword"];

                if (null != oldUser)
                {
                    var user = new ApplicationUser
                    {
                        UserName
                            = model.UserName,
                        Email
                            = oldUser.EmailAddress,
                        FirstName
                            = oldUser.FirstName,
                        LastName
                            = oldUser.LastName
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, user.FirstName));
                        UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Surname, user.LastName));

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        if (!new UserManager().EditPasssword(user.UserName, RetrievePasswordErrorValue("OldValue"), model.Password))
                        {
                            ModelState.AddModelError("", "Error changing password in database.");

                            return View(model);
                        }

                        Session.Remove("MigrationNewPassword");
                        Session.Remove("PasswordError");

                        return RedirectToAction("Index", "Home");
                    }

                    AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError("", "Previous information could not be found.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        // Created by: Trent Cullinan 03/25/16
        private bool CheckDbUser(string userName, string password = null)
        {
            return new UserManager().ConfirmUserExists(userName, password: password);
        }

        // Created by: Trent Cullinan 03/25/16
        private string RetrievePasswordErrorValue(string propertyName)
        {
            string result = string.Empty;

            try
            {
                result = (string)Session["PasswordError"]
                    .GetType()
                    .GetProperty(propertyName)
                    .GetValue(Session["PasswordError"], null);
            }
            catch (Exception) { } // result will be an empty string.

            return result;
        }
    } 
}