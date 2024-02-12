using Microsoft.IdentityModel.Tokens;
using SOFT703A2.Infrastructure.Contracts.Repositories;

namespace SOFT703A2.Infrastructure.ViewModels.Catalog;

using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.ViewModels.Catalog;

public class MarketPlaceViewModel : IMarketPlaceViewModel
{
    public List<Domain.Models.Product> Catalog { get; set; }
    public Trolley? CurrentTrolley { get; set; }


    private readonly IProductRepository _productRepository;
    private readonly ITrolleyRepository _trolleyRepository;
    private readonly IUserRepository _userRepository;

    public MarketPlaceViewModel(IProductRepository productRepository, ITrolleyRepository trolleyRepository,
        IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _trolleyRepository = trolleyRepository;
        _userRepository = userRepository;
    }

    public MarketPlaceViewModel()
    {
    }

    public async Task GetAllAsync()
    {
        Catalog = await _productRepository.GetAllWithCategoriesAsync();
        CurrentTrolley = await _trolleyRepository.GetLatest(_userRepository.GetUserId());
    }

    public async Task UpdateCatalog(string productName, bool byCategory, bool byPromoted, string orderBy)
    {
        Catalog = await _productRepository.GetExtendedSearch(productName, byCategory, byPromoted);
        if (!string.IsNullOrEmpty(orderBy) && !Catalog.IsNullOrEmpty())
        {
            switch (orderBy)
            {
                case "nameSort":
                    Catalog = Catalog.OrderBy(p => p.Name).ToList();
                    break;
                case "toHighestPriceSort":
                    Catalog = Catalog.OrderBy(p => p.Price).ToList();
                    break;
                case "toLowestPriceSort":
                    Catalog = Catalog.OrderByDescending(p => p.Price).ToList();
                    break;
                case "categorySort":
                    Catalog = Catalog.OrderBy(p => p.Category.Name).ToList();
                    break;
            }
        }
    }

    public async Task AddToTrolley(string productId)
    {
        CurrentTrolley = await _trolleyRepository.GetLatest(_userRepository.GetUserId());
        await _trolleyRepository.AddProduct(CurrentTrolley.Id, productId);
    }

    public async Task RemoveFromTrolley(string productId)
    {
        CurrentTrolley = await _trolleyRepository.GetLatest(_userRepository.GetUserId());
        await _trolleyRepository.RemoveProduct(CurrentTrolley.Id, productId);
    }

    public async Task CheckOut(string trolleyId)
    {
        await _trolleyRepository.CheckOut(trolleyId);
    }

    public async Task GetTrolley()
    {
        CurrentTrolley = await _trolleyRepository.GetLatest(_userRepository.GetUserId());
    }
}