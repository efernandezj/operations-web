using AutoMapper;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
{
    public interface IEmployeeService : IBaseService
    {
        public Task<List<Employee>> GetEmployees(string name, string workday);
        public Task<Employee> GetEmployeeByID(int Workday);
    }



    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<Employee>> GetEmployees(string name, string workday)
        {
            return await context.Employees.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name) || u.Workday.ToString() == workday).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByID(int Workday)
        {
            return await context.Employees.Where(e => e.Workday == Workday).FirstOrDefaultAsync();
        }
    }
}
