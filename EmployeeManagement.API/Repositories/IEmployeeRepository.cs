using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(string? search);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}