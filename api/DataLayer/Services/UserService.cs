using AutoMapper;
using DataLayer.DTOs;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
{
    public interface IUserService: IBaseService
    {
        public Task<User> GetUserById(int workday);
        public Task<List<Employee>> GetUsersInfo(string name, string workday);
        public Task<Employee> GetUserInfo(string workday);
        public Task<List<Employee>> GetActiveUsersInfo(string name, string workday);
        public User isUsernameAvailable(UserCreationDTO user);
        public User isEmailAvailable(UserCreationDTO user);
    }



    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<User> GetUserById(int workday)
        {
            return await context.Users.Include(e => e.Employee).FirstOrDefaultAsync(x => x.Workday == workday);
        }

        public async Task<List<Employee>> GetUsersInfo(string name, string workday)
        {
            // User depends of Employee, thefore It's required to query based on employee. If user does not exist, user info is null but employee data is retrieved
            return await context.Employees.Include(e => e.Users).Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name) || u.Workday.ToString() == workday).ToListAsync();
        }

        public async Task<Employee> GetUserInfo(string workday)
        {
            // User depends of Employee, thefore It's required to query based on employee. If user does not exist, user info is null but employee data is retrieved
            return await context.Employees.Where(e => e.Workday.ToString() == workday).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetActiveUsersInfo(string name, string workday)
        {
            // User depends of Employee, thefore It's required to query based on employee. If user does not exist, user info is null but employee data is retrieved
            return await context.Employees.Include(e => e.Users).Where(u => u.Users.Count > 0 && (u.FirstName.Contains(name) || u.LastName.Contains(name) || u.Workday.ToString() == workday)).ToListAsync();
        }

        public User isUsernameAvailable(UserCreationDTO user)
        {
            return context.Users.Include(u => u.Employee).Where(u => u.Workday != user.Workday && u.Username == user.Username).FirstOrDefault();
        }

        public User isEmailAvailable(UserCreationDTO user)
        {
            return context.Users.Include(u => u.Employee).Where(u => u.Workday != user.Workday && u.Email == user.Email).FirstOrDefault();
        }
    }
}
