using Library.BLL.CustomModels.Departments;
using Library.DAL.Entities.Departments;
using Library.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.Departments
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)//Ask CLR For Creating Object From Class Implmenting The Interface "IdepartmentReposatory" 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync(string search)
        {
            var departments = _unitOfWork.DepartmentReposatory.GetAllAsQueryable()
                   .Where(D => !D.IsDeleted && (string.IsNullOrEmpty(search) || D.Name.ToUpper().Contains(search.ToUpper())))
                   .Select(department => new DepartmentDto
                   {
                       Id = department.Id,
                       Name = department.Name,
                       Description = department.Description,
                   }).AsNoTracking().ToListAsync();
            return await departments;
        }   

        public async Task<DepartmentDetailsDto?> GetAsync(int id)
        {
            var department = await _unitOfWork.DepartmentReposatory.GetAsync(id);
            if (department is not null)
            {
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    CreatedBy = department.CreatedBy,
                    CreateOn = department.CreateOn,
                    IsDeleted = department.IsDeleted,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                };
            }
            return null;
        }

        public async Task<int> CreateDeptAsync(CreateDepartmentDto department)
        {
            var createdDepartment = new Department()
            {
                Name = department.Name,
                Description = department.Description,
                CreateOn = DateTime.Now,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            _unitOfWork.DepartmentReposatory.Add(createdDepartment);
            return await _unitOfWork.CompleteAsync();
        } 

        public Task<int> UpdateDeptAsync(UpdateDepartmentDto departmentDto)
        {
            var updatedDepartment = new Department()
            {   Id = departmentDto.Id,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreateOn = departmentDto.CreateOn,
                LastModifiedOn = DateTime.UtcNow,
                LastModifiedBy = 1
            };
            _unitOfWork.DepartmentReposatory.Update(updatedDepartment);
            return _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDeptAsync(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentReposatory;
            var department = await departmentRepo.GetAsync(id);
            if (department is { })
                departmentRepo.Delete(department);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }


}

