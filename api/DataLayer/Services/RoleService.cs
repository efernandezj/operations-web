using AutoMapper;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public interface IRoleService : IBaseService
    {
        public Task<List<Role>> Get();
        public Task<Role> GetRoleByID(int id);
    }

    public class RoleService : BaseService, IRoleService
    {
        public RoleService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<Role>> Get()
        {
            return await context.Roles
                                .Include(r => r.Creator).ThenInclude(u => u.Employee)
                                .Include(r => r.Modifier).ThenInclude(u => u.Employee).ToListAsync();
        }

        public async Task<Role> GetRoleByID(int id)
        {
            return await context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }
    }
}
