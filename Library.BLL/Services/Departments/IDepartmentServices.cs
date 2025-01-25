using Library.BLL.CustomModels.Departments;

namespace Library.BLL.Services.Departments
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync(string search);
        Task<DepartmentDetailsDto?> GetAsync(int id);
        Task<int> CreateDeptAsync(CreateDepartmentDto departmentDto);
        Task<int> UpdateDeptAsync(UpdateDepartmentDto departmentDto);
        Task<bool> DeleteDeptAsync(int id);
    }
}
