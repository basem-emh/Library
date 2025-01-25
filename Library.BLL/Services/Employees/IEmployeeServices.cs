using Library.BLL.CustomModels.Employees;

namespace Library.BLL.Services.Employees
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search);
        Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreateEmployeeDto employeeDto);
        Task<int> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
