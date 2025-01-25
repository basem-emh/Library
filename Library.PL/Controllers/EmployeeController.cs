using Library.BLL.Common.Helpers.Attachments;
using Library.BLL.CustomModels.Employees;
using Library.BLL.Services.Departments;
using Library.BLL.Services.Employees;
using Library.PL.Models.EmployeeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Library.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeServices _employeeServices;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IDepartmentServices _departmentServices;
        private readonly IAttachmentSettings _attachment;

        public EmployeeController(IEmployeeServices employeeServices,
            ILogger<EmployeeController> logger,
            IWebHostEnvironment webHostEnvironment,
            IDepartmentServices departmentServices,
            IAttachmentSettings attachment)
        {
            _employeeServices = employeeServices;
            _logger = logger;
            _environment = webHostEnvironment;
            _departmentServices = departmentServices;
            _attachment = attachment;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var employees = await _employeeServices.GetEmployeesAsync(search);
            return View(employees);
        }
        #endregion
        
        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeServices.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            var message = string.Empty;
            try
            {
                if (!ModelState.IsValid)
                    return View(employeeVM);


                var createdEmployee = new CreateEmployeeDto
                {
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Age = employeeVM.Age,
                    Email = employeeVM.Email,
                    Gender = employeeVM.Gender,
                    HiringDate = employeeVM.HiringDate,
                    IsActive = employeeVM.IsActive,
                    Image = employeeVM.Image,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Salary = employeeVM.Salary,
                    DepartmentId = employeeVM.DepartmentId,
                };
                

                var results = await _employeeServices.CreateEmployeeAsync(createdEmployee);
                if (results > 0)
                    return RedirectToAction(nameof(Index));

                message = "Employee Is Not Added";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during adding employee :(";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _employeeServices.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            var message = string.Empty;

            if (!ModelState.IsValid)
                return View(employeeVM);
            try
            {
                var updatedEmployee = new UpdateEmployeeDto
                {
                    Id = id,
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Age = employeeVM.Age,
                    Email = employeeVM.Email,
                    Gender = employeeVM.Gender,
                    HiringDate = employeeVM.HiringDate,
                    IsActive = employeeVM.IsActive,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Salary = employeeVM.Salary,
                    DepartmentId = employeeVM.DepartmentId,
                    Image = employeeVM.Image,
                };

                var updeted = await _employeeServices.UpdateEmployeeAsync(updatedEmployee) > 0;
                if (updeted)
                    return RedirectToAction(nameof(Index));

                message = "An error has occured during update department :(";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during updating department :(";

            }

            ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
        }

        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _employeeServices.DeleteEmployeeAsync(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                message = "An error has occured during deteting Employee :(";
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during deleting employee :(";

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
