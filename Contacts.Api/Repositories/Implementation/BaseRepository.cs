using Contacts.Api.Models;
using Contacts.Api.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api.Repositories.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> GetById(Guid id)
        {
            return _context.Set<T>().Where(entity => entity.Id == id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentOutOfRangeException(nameof(entity));

            entity.CreationDate = DateTime.UtcNow;
            entity.LastUpdateDate = DateTime.UtcNow;

            await _context.Set<T>().AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            entity.LastUpdateDate = DateTime.UtcNow;

            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRangeAsync(List<T> entities)
        {
            _context.RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRangeAsync(List<T> entities)
        {
            entities.ForEach(x => x.LastUpdateDate = DateTime.UtcNow);
            _context.UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = DateTime.UtcNow;
                entity.LastUpdateDate = DateTime.UtcNow;
            }

            _context.AddRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
