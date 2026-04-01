using EmployeeManagement.API.DTOs;

namespace EmployeeManagement.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync(string? search);
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
        Task UpdateAsync(int id, UpdateEmployeeDto dto);
        Task DeleteAsync(int id);
    }
}