using Microsoft.EntityFrameworkCore;
using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Persistence;


namespace SOFT703A2.Infrastructure.Repositories;

public class TrolleyRepository : BaseRepository<Trolley>, ITrolleyRepository
{
    public TrolleyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Trolley?> GetExtendedTrolley(string trolleyId)
    {
        
        return await _context.Trolley.Include(x => x.ProductXTrolleys).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == trolleyId);
        
    }

    public async Task<Trolley?> GetLatest(string id)
    {
        var trolley = await _context.Trolley.Include(x => x.ProductXTrolleys).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.UserId == id && x.IsCurrent);

        if (trolley == null)
        {
            trolley = await AddAsync(new Trolley()
                { UserId = id, IsCurrent = true, TransactionDate = DateTime.Now, Total = 0 });
            trolley = await RecalculateTotal(trolley.Id);
            trolley.ProductXTrolleys = new List<ProductXTrolley>();
            return trolley;
        }

        return trolley;
    }

    public async Task<Trolley> RecalculateTotal(string id)
    {
        var trolley = _context.Trolley.Include(x => x.ProductXTrolleys).ThenInclude(x => x.Product)
            .FirstOrDefault(x => x.Id == id);
        trolley.Total = trolley.ProductXTrolleys.Sum(x => x.Product.Price * x.Quantity);
        return await UpdateAsync(trolley);
    }

    public async Task<Trolley> AddProduct(string id, string productId)
    {
        var product = _context.Product.Find(productId);
        var trolley = _context.Trolley.Include(x => x.ProductXTrolleys).ThenInclude(x => x.Product)
            .FirstOrDefault(x => x.Id == id);
        var existingProduct = trolley.ProductXTrolleys.SingleOrDefault(pt => pt.ProductId == productId);
        if (existingProduct == null)
        {
            trolley.ProductXTrolleys.Add(new ProductXTrolley
            {
                Product = product,
                Quantity = 1
            });
        }
        else
        {
            existingProduct.Quantity++;
        }

        trolley.Total += product.Price;
        var updatedTrolley = await UpdateAsync(trolley);
        updatedTrolley = await RecalculateTotal(updatedTrolley.Id);
        return updatedTrolley;
    }

    public async Task<Trolley> RemoveProduct(string id, string productId)
    {
        var product = _context.Product.Find(productId);
        var trolley = _context.Trolley.Include(x => x.ProductXTrolleys).ThenInclude(x => x.Product)
            .FirstOrDefault(x => x.Id == id);
        var existingProduct = trolley.ProductXTrolleys.SingleOrDefault(pt => pt.ProductId == productId);
        if (existingProduct != null)
        {
            if (existingProduct.Quantity > 1)
            {
                existingProduct.Quantity--;
            }
            else
            {
                trolley.ProductXTrolleys.Remove(existingProduct);
            }
        }

        trolley.Total -= product.Price;
        var updatedTrolley = await UpdateAsync(trolley);
        updatedTrolley = await RecalculateTotal(updatedTrolley.Id);
        return updatedTrolley;
    }

    public async Task<Trolley> CheckOut(string trolleyId)
    {
        var trolley = _context.Trolley.Find(trolleyId);
        trolley.IsCurrent = false;
        trolley.TransactionDate = DateTime.Now;
        var newTrolley = new Trolley()
        {
            UserId = trolley.UserId,
            IsCurrent = true,
            Total = 0
        };
        await UpdateAsync(trolley);
        return await AddAsync(newTrolley);
    }
}