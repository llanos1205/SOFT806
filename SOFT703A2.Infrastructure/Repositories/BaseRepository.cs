using Microsoft.EntityFrameworkCore;
using SOFT703A2.Infrastructure.Contracts.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;

namespace SOFT703A2.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var result = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var result = _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        var result = _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<T> DeleteAsync(string id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
            return entity;
        var result = _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Set<T>().AnyAsync(e => e.Id == id);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> IsEmpty()
    {
        return await _context.Set<T>().AnyAsync();
    }
}