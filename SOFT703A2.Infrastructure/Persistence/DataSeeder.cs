using SOFT703A2.Domain.Models;
using SOFT703A2.Infrastructure.Contracts.Repositories;
using SOFT703A2.Infrastructure.Repositories;

namespace SOFT703A2.Infrastructure.Persistence;

public class DataSeeder
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public DataSeeder(IUserRepository userRepository, IRoleRepository roleRepository,
        ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task SeedData()
    {
        await SeedRolesAsync();
        await SeedAdminUser();
        await SeedCategoriesAsync();
        await SeedProductsAsync();
    }

    private async Task SeedAdminUser()
    {
        var adminUser = await _userRepository.GetUserByEmail("admin@gmail.com");
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "123456789",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            await _userRepository.AddDefaultAsync(adminUser, "P@ssw0rd");
            var result = await _userRepository.GetUserByEmail("admin@gmail.com");
            if (result != null)
            {
                await _userRepository.SetRole("admin@gmail.com", "ADMIN");
            }
            else
            {
            }
        }
    }

    private async Task SeedCategoriesAsync()
    {
        if (!await _categoryRepository.IsEmpty())
        {
            var categories = new List<Category>
            {
                new() { Name = "Plushies" },
                new() { Name = "Bags" },
                new() { Name = "Wallets" },
                new() { Name = "Figures" }
            };

            foreach (var category in categories)
            {
                await _categoryRepository.AddAsync(category);
            }
        }
    }

    private async Task SeedRolesAsync()
    {
        if (!await _roleRepository.IsEmpty())
        {
            var roles = new List<Role>
            {
                new() { Name = "Admin", NormalizedName = "ADMIN" },
                new() { Name = "Client", NormalizedName = "CLIENT" }
            };

            foreach (var role in roles)
            {
                await _roleRepository.AddAsync(role);
            }
        }
    }

    public async Task SeedProductsAsync()
    {
        if (!await _productRepository.IsEmpty())
        {
            var categories = await _categoryRepository.GetAllAsync();
            var productList = new List<Product>()
            {
                new()
                {
                    Name = "Vaporeon Mimikiu",
                    Price = 50.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1714578092_480x.jpg?v=1626812080"
                },
                new()
                {
                    Name = "Flareon Mimikiu",
                    Price = 40.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1804715633_480x.jpg?v=1627213674"
                },
                new()
                {
                    Name = "Jolteon Mimikiu",
                    Price = 60.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1804718336_480x.jpg?v=1626812077"
                },
                new()
                {
                    Name = "Leafeon Mimikiu",
                    Price = 70.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1714578097_480x.jpg?v=1627213876"
                },
                new()
                {
                    Name = "Glaceon Mimikiu",
                    Price = 30.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1804715638_480x.jpg?v=1626812076"
                },
                new()
                {
                    Name = "Umbreon Mimikiu",
                    Price = 80.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1804715637_480x.jpg?v=1626812080"
                },
                new()
                {
                    Name = "Espeon Mimikiu",
                    Price = 90.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1714578094_480x.jpg?v=1626812078"
                },
                new()
                {
                    Name = "Sylveon Mimikiu",
                    Price = 10.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Plushies"),
                    Photo =
                        "https://pokemon-faction.com/cdn/shop/products/product-image-1804697401_480x.jpg?v=1626812079"
                },
                new()
                {
                    Name = "Pokeball Bag",
                    Price = 75.00,
                    Stock = 40,
                    Category = categories.Find(c => c.Name == "Bags"),
                    Photo =
                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT3yNLmeHVBEDrlsRFn0WXA0OFmFou8WBkULc3INApw0SPFj4-O4_5-wwRWCFmorGOYAc8&usqp=CAU"
                },
                new()
                {
                    Name = "Pokeball Bag",
                    Price = 22.00,
                    Stock = 20,
                    Category = categories.Find(c => c.Name == "Bags"),
                    Photo =
                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQnHo86MIiby9KnrfXeT75TIiui9TYYsBGNTaI80ZuAJmXup8qrJGAyTNUCqTHcQg36hzM&usqp=CAU"
                },
                new()
                {
                    Name = "Pokeball Bag",
                    Price = 124.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Bags"),
                    Photo =
                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSxmk_skPglNXsONKVIgVfeZgqsfshoyEPGoFuo5Pm0JtEtodj-R1YaYmx9UTihhuzT2Vk&usqp=CAU"
                },
                new()
                {
                    Name = "AA Wallet",
                    Price = 50.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Wallets"),
                    Photo = "https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P8557_710-95821_01.jpg"
                },
                new()
                {
                    Name = "HHH Wallet",
                    Price = 80.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Wallets"),
                    Photo =
                        "https://i5.walmartimages.com/asr/cef81da1-9108-4fa6-b6c8-c5c6b29eb80c.283f48d4bbf1eaa7ff16a90ed00da983.jpeg?odnHeight=612&odnWidth=612&odnBg=FFFFFF"
                },
                new()
                {
                    Name = "ZZZ Wallet",
                    Price = 40.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Wallets"),
                    Photo =
                        "https://target.scene7.com/is/image/Target/GUEST_649bd33d-1108-4aad-8ab4-35b0a94ce8d7?wid=800&hei=800&qlt=80&fmt=webp"
                },
                new()
                {
                    Name = "BB Wallet",
                    Price = 150.00,
                    Stock = 50,
                    Category = categories.Find(c => c.Name == "Wallets"),
                    Photo =
                        "https://target.scene7.com/is/image/Target/GUEST_d341fb62-90eb-4d73-afdd-889cb14489b5?wid=800&hei=800&qlt=80&fmt=webp"
                },
                new()
                {
                    Name = "SSS Figure",
                    Price = 200.00,
                    Stock = 100,
                    Category = categories.Find(c => c.Name == "Figures"),
                    Photo = "https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P9073_710-96728_01.jpg"
                },
                new()
                {
                    Name = "FFF Figure",
                    Price = 300.00,
                    Stock = 100,
                    Category = categories.Find(c => c.Name == "Figures"),
                    Photo = "https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P9206_703-97872_01.jpg"
                },
                new()
                {
                    Name = "YYY Figure",
                    Price = 700.00,
                    Stock = 100,
                    Category = categories.Find(c => c.Name == "Figures"),
                    Photo = "https://www.pokemoncenter.com/images/DAMRoot/Full-Size/10000/P6259_703-05967_01.jpg"
                }
            };

            foreach (var product in productList)
            {
                await _productRepository.AddAsync(product);
            }
        }
    }
}