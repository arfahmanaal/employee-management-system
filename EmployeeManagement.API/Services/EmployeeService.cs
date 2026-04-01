using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Exceptions;
using EmployeeManagement.API.Models;
using EmployeeManagement.API.Repositories;

namespace EmployeeManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(string? search)
        {
            var employees = await _repository.GetAllAsync(search);
            return employees.Select(MapToDto);
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                throw new NotFoundException("Employee", id);
            return MapToDto(employee);
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            if (await _repository.EmailExistsAsync(dto.Email))
                throw new BadRequestException("Email already exists.");

            var employee = MapToEntity(dto);
            var created  = await _repository.AddAsync(employee);
            return MapToDto(created);
        }

        public async Task UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("Employee", id);

            if (await _repository.EmailExistsAsync(dto.Email, excludeId: id))
                throw new BadRequestException("Email already exists.");

            existing.FullName      = dto.FullName;
            existing.Email         = dto.Email;
            existing.PhoneNumber   = dto.PhoneNumber;
            existing.Designation   = dto.Designation;
            existing.Department    = dto.Department;
            existing.DateOfJoining = dto.DateOfJoining;
            existing.Status        = dto.Status;
            existing.Location      = dto.Location;
            existing.SalaryBand    = dto.SalaryBand;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("Employee", id);

            await _repository.DeleteAsync(id);
        }

        private static EmployeeDto MapToDto(Employee e) => new()
        {
            EmployeeId    = e.EmployeeId,
            FullName      = e.FullName,
            Email         = e.Email,
            PhoneNumber   = e.PhoneNumber,
            Designation   = e.Designation,
            Department    = e.Department,
            DateOfJoining = e.DateOfJoining,
            Status        = e.Status,
            Location      = e.Location,
            SalaryBand    = e.SalaryBand
        };

        private static Employee MapToEntity(CreateEmployeeDto dto) => new()
        {
            FullName      = dto.FullName,
            Email         = dto.Email,
            PhoneNumber   = dto.PhoneNumber,
            Designation   = dto.Designation,
            Department    = dto.Department,
            DateOfJoining = dto.DateOfJoining,
            Status        = dto.Status,
            Location      = dto.Location,
            SalaryBand    = dto.SalaryBand
        };
    }
}