
using Microsoft.EntityFrameworkCore;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;

namespace SOFT703A2.Infrastructure.Repositories;
public class ProductRepository: BaseRepository<Product>,IProductRepository
{
    
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Product>> GetExtendedSearch(string name, bool byCategory, bool byPromoted)
    {
        try
        {
            var query = _context.Product.AsQueryable(); 

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (byCategory)
                {
                    
                    query = query.Where(product => product.Category.Name.ToLower().Contains(name));
                }
                else
                {
                    
                    query = query.Where(product => product.Name.ToLower().Contains(name));
                }
            }
            

            if (byPromoted)
            {
                // Apply filter for promoted products
                query = query.Where(product => product.IsPromoted);
            }

            // Execute the query and return the filtered products as a list
            var filteredProducts = await query
                .Select(product => new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Stock = product.Stock,
                    Price = product.Price,
                    IsPromoted = product.IsPromoted,
                    Photo = product.Photo,
                    Category = product.Category
                })
                .ToListAsync();

            return filteredProducts;
        }
        catch (Exception ex)
        {
            // do something here
            throw;
        }
    }

    public async Task<List<Product>> GetAllWithCategoriesAsync()
    {
        var result = await _context.Product.Include(c=> c.Category).ToListAsync();
        return result;
    }
}