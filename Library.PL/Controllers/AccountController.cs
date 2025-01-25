using Library.BLL.Common.Helpers.Emails;
using Library.DAL.Entities.Users;
using Library.PL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Library.BLL.Common.Helper;

namespace Library.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        } 
        #endregion

        #region Sign Up // Register
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel upViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var user = await _userManager.FindByNameAsync(upViewModel.UserName);

            if (user is { })
            {
			    ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This username is already in use with another account");
				return View(upViewModel);
			}

            user = new ApplicationUser()
            {
                FirstName = upViewModel.FName,
                LastName = upViewModel.LName,
                UserName = upViewModel.UserName,
                Email = upViewModel.Email,
                IsActive = upViewModel.IsAgree,
            };

            var result = await _userManager.CreateAsync(user, upViewModel.Password);
            if (result.Succeeded)
                return RedirectToAction(nameof(SignIn));
            foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

            return View(upViewModel);
        }
        #endregion
        
        #region Sign In
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel inViewModel)
		{
			if(!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(inViewModel.Email);
            if (user is { })
            {
                var flag = await _userManager.CheckPasswordAsync(user, inViewModel.Password);
                
                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, inViewModel.Password, inViewModel.RememberMe, true);
                 
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account is not confirmed yet!!!");
					if (result.IsLockedOut)
						ModelState.AddModelError(string.Empty, "Your account is Locked!!!");

                    if(result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
				}

            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(inViewModel);
        }
        #endregion

        #region Sign Out 
        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetVM.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new { Email = forgetVM.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Body = url,
                        Subject = "Reset Password",
                        To = forgetVM.Email,
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            return View(forgetVM);
        }
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(SignIn));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion
    }
}
