using CompanyMVC.Helper;
using CompanyMVC.ViewModels;
using CompanyMVC.Views.Account;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly SignInManager<ApplicationUser> _signIn;

        public AccountController(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signIn)
        {
            _user = user;
            _signIn = signIn;
        }

        #region Register 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)//server Side Validation
            {
                var User = new ApplicationUser//manual map from  RegisterViewModel(View Model) To Identity(database) 
                {
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    ISAgree = model.IsAgree,
                    UserName = model.Email.Split('@')[0]

                };
                var Result = await _user.CreateAsync(User, model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);//return the Description error 
                    }
                }
            }
            return View(model);

        }
        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()//to View the Form 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //1)Check User Email 
                var User = await _user.FindByEmailAsync(model.Email);//to search about the entered email in userManager 
                if (User is not null)//check if Email User Is found
                {

                    //2)check check User Password
                    var result = await _user.CheckPasswordAsync(User, model.Password);
                    if (result is true)
                    {
                        //3)Login
                        var LoginResult = await _signIn.PasswordSignInAsync(User, model.Password, model.RememberMe, false);

                        if (LoginResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email Not Exist");
                }

            }
            return View(model);
        }
        #endregion

        #region Sign Out
        public new async Task<IActionResult> SignOut()
        {//we used new cause Controller Class which AccountController inherit from have the same name function so new used to declare another new one
            await _signIn.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        public IActionResult ForGetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForGetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var User = await _user.FindByEmailAsync(model.Email);

                if (User is not null)
                {
                    //https://localhost:44380/Account/Resetpassword/email?=mr2438844@gmail.com?Token=hlhhd5ffhlg
                    //1)Request.Scheme =>https://localhost:44380
                    //2)Controller =>"Account"
                    //3)Action =>"ResetPassword"
                    //4)email +Token  => new {email=User.Email,token}
                    var token = await _user.GeneratePasswordResetTokenAsync(User);//valid For Only One Time For this User To be more secure 
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = User.Email, token }, Request.Scheme);
                    var email = new Email
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = "ResetPasswordLink"
                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            else
            {
                return View(nameof(ForGetPassword), model);
            }
            return View(nameof(ForGetPassword), model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;//to send it to
            TempData["token"] = token;
            return View();

        }
        //to send data from action to action use => Tempdata[]
        //to send data from action to View use=> viewBag[] || Viewdata[]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;//to get User 
                string token = TempData["token"] as string;
                var User = await _user.FindByEmailAsync(email);
                var Result = await _user.ResetPasswordAsync(User, token, model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var Error in Result.Errors) //using loop as there are many errors in the inputs
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }

            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }

}
