using Library.BLL.CustomModels.Departments;
using Library.BLL.Services.Departments;
using Library.DAL.Entities.Departments;
using Library.PL.Models.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Library.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentServices departmentServices,
              ILogger<DepartmentController> logger,
              IWebHostEnvironment environment)
        {
            _departmentServices = departmentServices;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var department = await _departmentServices.GetAllAsync(search);
            return View(department);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentServices.GetAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }

        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            var message = string.Empty;
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var createdDepartment = new CreateDepartmentDto
                {
                    Name = departmentVM.Name,
                    Description = departmentVM.Description
                };
                
                var created = await _departmentServices.CreateDeptAsync(createdDepartment) > 0;
                
                if (created)
                    TempData["Message"] = "Department is created";
                else
                {
                    ModelState.AddModelError(string.Empty, message);
                    TempData["Message"] = "Department is not created";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //1-log Exception
                _logger.LogError(ex,ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during Creating department :(";

                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id) 
        {
            if (id is null)
                return BadRequest();
            var department = await _departmentServices.GetAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(new DepartmentViewModel()
            {
                Name = department.Name,
                Description= department.Description,
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
            {
            var message = string.Empty;

            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var departmentToUpdate = new UpdateDepartmentDto()
                {
                    Id = id,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                };
                var updete = await _departmentServices.UpdateDeptAsync(departmentToUpdate) > 0;
                if (updete)
                {
                    TempData["Message"] = $"Departmet {departmentToUpdate.Name} is Updated";
                }
                else
                {
                    message = "An error has occured during update department :(";
                    ModelState.AddModelError(string.Empty, message);
                    TempData["Message"] = $"Departmet {departmentToUpdate.Name} is Updated";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during updating department :(";

                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DepartmentViewModel departmentVM)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _departmentServices.DeleteDeptAsync(id);
                if (deleted)
                {
                    TempData["Message"] = $"Department {departmentVM.Name} is deleted";
                }
                else
                {
                    TempData["Message"] = $"Department {departmentVM.Name} is not deleted";
                    message = "An error has occured during deteting department :(";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during deleting department :(";
                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion
    }
}
