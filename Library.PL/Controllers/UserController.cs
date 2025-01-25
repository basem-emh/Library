using Library.BLL.CustomModels.Employees;
using Library.DAL.Entities.Users;
using Library.PL.Models.EmployeeViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(UserManager<ApplicationUser> userManager ,
            ILogger<EmployeeController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string searchInp)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(searchInp))
                users = await _userManager.Users.ToListAsync();
            else
            {
                users = await _userManager.Users
                    .Where(user=>user.NormalizedEmail.Trim().Contains(searchInp.Trim().ToUpper()))
                    .ToListAsync();
            }
            return View(users);
        }
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var message = string.Empty;

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    message = "User not found.";
                }
                else
                {
                    var result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        message = "An error occurred during deleting the user.";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An error has occurred during deleting the user.";
            }
            TempData["ErrorMessage"] = message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
