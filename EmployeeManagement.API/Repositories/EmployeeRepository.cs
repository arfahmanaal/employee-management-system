using EmployeeManagement.API.Data;
using EmployeeManagement.API.Exceptions;
using EmployeeManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(string? search)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(e =>
                    e.FullName.Contains(search) ||
                    e.Department.Contains(search) ||
                    e.Designation.Contains(search));

            return await query
                .OrderBy(e => e.FullName)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            var existing = await _context.Employees.FindAsync(employee.EmployeeId);
            if (existing == null)
                throw new NotFoundException("Employee", employee.EmployeeId);

            existing.FullName      = employee.FullName;
            existing.Email         = employee.Email;
            existing.PhoneNumber   = employee.PhoneNumber;
            existing.Designation   = employee.Designation;
            existing.Department    = employee.Department;
            existing.DateOfJoining = employee.DateOfJoining;
            existing.Status        = employee.Status;
            existing.Location      = employee.Location;
            existing.SalaryBand    = employee.SalaryBand;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                throw new NotFoundException("Employee", id);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _context.Employees
                .AnyAsync(e => e.Email == email &&
                               (excludeId == null || e.EmployeeId != excludeId));
        }
    }
}