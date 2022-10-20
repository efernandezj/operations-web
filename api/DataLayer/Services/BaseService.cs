using AutoMapper;
using DataLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Services
{
    public interface IBaseService
    {
        public Task<List<DropDownItemDTO>> GetListDropDownList<T>() where T : class;
        public Task<List<R>> GetListDropDownList<T, R>(Expression<Func<T, bool>> expression) where T : class;

        public void CreateEntity<T>(T Entity) where T : class;

        public abstract void UpdateEntity<T>(T Entities) where T : class;

        public abstract void DeleteEntity<T>(T Entities) where T : class;

        public abstract void SaveChanges();

        public abstract Task<int> SaveChangesAsync();
    }


    public class BaseService : IBaseService
    {
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;

        public BaseService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        public async Task<List<DropDownItemDTO>> GetListDropDownList<T>() where T : class
        {
            return mapper.Map<List<DropDownItemDTO>>(await context.Set<T>().ToListAsync());
        }

        public async Task<List<R>> GetListDropDownList<T, R>(Expression<Func<T, bool>> expression) where T : class
        {
            return mapper.Map<List<R>>(await context.Set<T>().Where(expression).ToListAsync());
        }




        public virtual void CreateEntity<T>(T Entity) where T : class
        {
            context.Add(Entity);
        }

        public virtual void UpdateEntity<T>(T Entities) where T : class
        {
            context.Update(Entities);
        }

        public virtual void DeleteEntity<T>(T Entities) where T : class
        {
            context.Remove(Entities);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
     