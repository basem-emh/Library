using Library.BLL.Common.Helpers.Attachments;
using Library.BLL.CustomModels.Employees;
using Library.DAL.Entities.Employees;
using Library.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentSettings _attachment;

        public EmployeeServices(
            IUnitOfWork unitOfWork,
            IAttachmentSettings attachment) // Asking CLR for Creating object from class implmenting "IUnit" 
        {
            _unitOfWork = unitOfWork;
            _attachment = attachment;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search)
        {
            var employee = _unitOfWork.EmployeeReposatory
                .GetAllAsQueryable()
                .Where(E => !E.IsDeleted &&(string.IsNullOrEmpty(search) || E.Name.ToUpper().Contains(search.ToUpper())))
                .Include(E => E.Department)
                .Select(employee => new EmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Address = employee.Address,
                    PhoneNumber  = employee.PhoneNumber,
                    Department = employee.Department.Name
                }).ToListAsync();
            return await employee;
        }

        public async Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeReposatory.GetAsync(id);
            if (employee == null)
                return null;

            return new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                Gender = employee.Gender,
                Department = employee.Department?.Name,
                CreatedBy = employee.CreatedBy,
                CreatedOn = employee.CreateOn,
                LastModifiedBy = employee.LastModifiedBy,
                LastModifiedOn = employee.LastModifiedOn,
                Image =employee.Image,
            };
        }


        public async Task<int> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var recievdImage = string.Empty;

            if (employeeDto.Image is not null)
                recievdImage = _attachment.Upload(employeeDto.Image, "images");

            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Address = employeeDto.Address,
                Age = employeeDto.Age,
                Email = employeeDto.Email,
                Salary = employeeDto.Salary,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                IsActive = employeeDto.IsActive,
                DepartmentId = employeeDto.DepartmentId,
                Gender = employeeDto.Gender,
                Image = recievdImage,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            _unitOfWork.EmployeeReposatory.Add(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto)
        {
            var employee = await _unitOfWork.EmployeeReposatory.GetAsync(employeeDto.Id);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }

            var recievdImage = employee.Image;
            if (employeeDto.Image is not null)
            {
                recievdImage = _attachment.Upload(employeeDto.Image, "images");
            }

            employee.Name = employeeDto.Name;
            employee.Address = employeeDto.Address;
            employee.Age = employeeDto.Age;
            employee.Email = employeeDto.Email;
            employee.Salary = employeeDto.Salary;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.HiringDate = employeeDto.HiringDate;
            employee.IsActive = employeeDto.IsActive;
            employee.Gender = employeeDto.Gender;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.LastModifiedBy = 1;
            employee.LastModifiedOn = DateTime.UtcNow;
            employee.Image = recievdImage;

            _unitOfWork.EmployeeReposatory.Update(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeReposatory;
            var employee = await employeeRepo.GetAsync(id);
            if (employee is { })
                employeeRepo.Delete(employee);
            return await _unitOfWork.CompleteAsync() > 0;
        }

    }
}
