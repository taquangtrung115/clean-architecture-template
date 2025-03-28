using CleanArchitectureTemplate.Domain.Entities.Base;
using CleanArchitectureTemplate.Domain.Constants;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace CleanArchitectureTemplate.Infrastructure.Seeders;

public class ProductSeeder(ApplicationDbContext dbContext) : IProductSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
        IEnumerable<CleanArchitectureTemplate.Domain.Entities.Category> categories;
        IEnumerable<CleanArchitectureTemplate.Domain.Entities.AdditionImgUrl> additionImgUrls;
        IEnumerable<CleanArchitectureTemplate.Domain.Entities.Review> reviews;
        IEnumerable<CleanArchitectureTemplate.Domain.Entities.Brand> brands;
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Product.Any(s => s.IsDeleted == false || s.IsDeleted == null))
            {
                var products = GetProducts(out categories, out additionImgUrls, out reviews, out brands);
                dbContext.Product.AddRange(products);
                if (!dbContext.Category.Any(s => s.IsDeleted == false || s.IsDeleted == null) && categories.Any())
                {
                    dbContext.Category.AddRange(categories);
                }
                if (!dbContext.AdditionImgUrl.Any(s => s.IsDeleted == false || s.IsDeleted == null) && additionImgUrls.Any())
                {
                    dbContext.AdditionImgUrl.AddRange(additionImgUrls);
                }
                if (!dbContext.Review.Any(s => s.IsDeleted == false || s.IsDeleted == null) && reviews.Any())
                {
                    dbContext.Review.AddRange(reviews);
                }
                if (!dbContext.Brand.Any(s => s.IsDeleted == false || s.IsDeleted == null) && brands.Any())
                {
                    dbContext.Brand.AddRange(brands);
                }
                ApplyAuditInformation();
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    private void ApplyAuditInformation()
    {
        var entries = dbContext.ChangeTracker.Entries<AuditableEntity>();

        string? currentUser = "System";

        foreach (var entry in entries)
        {
            try
            {
                // Check if the Id is of type Guid and if it is empty, initialize it with a new Guid
                if (entry.Entity.Id is Guid guid && guid == Guid.Empty)
                {
                    entry.Entity.Id = Guid.NewGuid();
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModificationDate = DateTime.UtcNow;
                        entry.Entity.ModificationBy = currentUser;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.ModificationDate = DateTime.UtcNow;
                        entry.Entity.ModificationBy = currentUser;
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];

        return roles;
    }
    public static string GetUppercaseLetters(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        // Extract all uppercase letters from the input string
        var result = new StringBuilder();
        if (!input.Any(s => char.IsUpper(s)))
        {
            result.Append(input.ToUpper());
            return result.ToString();
        }
        foreach (var c in input)
        {
            if (char.IsUpper(c))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    private IEnumerable<CleanArchitectureTemplate.Domain.Entities.Product> GetProducts(
        out IEnumerable<CleanArchitectureTemplate.Domain.Entities.Category> categories
        , out IEnumerable<CleanArchitectureTemplate.Domain.Entities.AdditionImgUrl> additionImgUrls
        , out IEnumerable<CleanArchitectureTemplate.Domain.Entities.Review> reviews
        , out IEnumerable<CleanArchitectureTemplate.Domain.Entities.Brand> brands
        )
    {
        var owner = new ApplicationUser()
        {
            Email = "admin@test.com",
            UserName = "admin"
        };
        var lstProductDatas = ReadProductsFromJson("D:\\CleanAchitectureTemplate\\src\\CleanArchitectureTemplate.Infrastructure\\Seeders\\DataTemp\\Product.json");
        var lstProduct = new List<CleanArchitectureTemplate.Domain.Entities.Product>();
        var lstCategory = new List<CleanArchitectureTemplate.Domain.Entities.Category>();
        var lstAdditionImgUrl = new List<CleanArchitectureTemplate.Domain.Entities.AdditionImgUrl>();
        var lstReview = new List<CleanArchitectureTemplate.Domain.Entities.Review>();
        var lstBrand = new List<CleanArchitectureTemplate.Domain.Entities.Brand>();
        foreach (var item in lstProductDatas)
        {
            var product = new CleanArchitectureTemplate.Domain.Entities.Product();
            var category = new CleanArchitectureTemplate.Domain.Entities.Category();
           
           
            var brand = new CleanArchitectureTemplate.Domain.Entities.Brand();
            product.Id = Guid.NewGuid();
            product.ProductName = item.name;
            product.ProductCode = GetUppercaseLetters(item.name);
            product.Description = item.description;
            product.IsInStock = item.isInStock;
            product.Gender = item.gender;
            product.ImageUrl = item.imageUrl;
            if (!string.IsNullOrEmpty(item.brandName))
            {
                brand.Id = Guid.NewGuid();
                brand.BrandName = item.brandName;
                brand.BrandCode = GetUppercaseLetters(item.brandName);
                if (!lstBrand.Any(s => s.BrandName == brand.BrandName))
                {
                    product.BrandID = brand.Id;
                    lstBrand.Add(brand);
                }
                else
                {
                    var brandByID = lstBrand.FirstOrDefault(s => s.BrandName == brand.BrandName);
                    product.BrandID = brandByID != null ? brandByID.Id : Guid.Empty;
                }
            }
            if (item.additionalImageUrls != null)
            {
                foreach (var additionalImageUrls in item.additionalImageUrls)
                {
                    var additionImgUrl = new CleanArchitectureTemplate.Domain.Entities.AdditionImgUrl();
                    additionImgUrl.Id = Guid.NewGuid();
                    additionImgUrl.ProductID = product.Id;
                    additionImgUrl.ImageUrl = additionalImageUrls;

                    if (!lstAdditionImgUrl.Any(s => s.ImageUrl == additionImgUrl.ImageUrl))
                        lstAdditionImgUrl.Add(additionImgUrl);
                }
            }
            if (item.reviews != null)
            {
                foreach (var itemReview in item.reviews)
                {
                    var review = new CleanArchitectureTemplate.Domain.Entities.Review();
                    review.ReviewTitle = itemReview.reviewTitle;
                    review.ReviewContent = itemReview.reviewText;
                    if (DateTime.TryParse(itemReview.date, out var dateReview))
                    {
                        review.Date = dateReview;
                    }
                    review.Rating = itemReview.rating;
                    review.UserName = itemReview.username;
                    review.UserImage = itemReview.userImage;
                    review.Location = itemReview.location;
                    review.ProductID = product.Id;
                    review.ReviewCode = GetUppercaseLetters(itemReview.reviewTitle);

                    if (!lstReview.Any(s => s.ReviewTitle == review.ReviewTitle))
                        lstReview.Add(review);
                }
                
            }

            product.ImageUrl = item.imageUrl;
            product.TotalReview = item.totalReviewCount;
            if (item.price != null)
            {
                product.Price = (double)item.price.current.value;
            }

            product.StrSize = string.Join(",", item.availableSizes) ?? string.Empty;

            category.CategoryName = item.category;
            category.Id = Guid.NewGuid();
            category.CategoryCode = GetUppercaseLetters(item.category);
            product.CategoryID = category.Id;
            if (!lstCategory.Any(s => s.CategoryName == category.CategoryName))
            {
                lstCategory.Add(category);
            }
            lstProduct.Add(product);
        }
        categories = lstCategory.AsEnumerable();
        additionImgUrls = lstAdditionImgUrl.AsEnumerable();
        reviews = lstReview.AsEnumerable();
        brands = lstBrand.AsEnumerable();
        return lstProduct;
    }
    public List<Product> ReadProductsFromJson(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var products = JsonSerializer.Deserialize<ProductData>(jsonData);
        return products.products;
    }
}

#region Class Temp
public class Product
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public bool isInStock { get; set; }
    public string gender { get; set; }
    public string category { get; set; }
    public List<string> availableSizes { get; set; }
    public int rating { get; set; }
    public List<Review> reviews { get; set; }
    public int totalReviewCount { get; set; }
    public DateTime productionDate { get; set; }
    public Price price { get; set; }
    public string brandName { get; set; }
    public string imageUrl { get; set; }
    public List<string> additionalImageUrls { get; set; }
}

public class Review
{
    public string username { get; set; }
    public string userImage { get; set; }
    public string location { get; set; }
    public int rating { get; set; }
    public string date { get; set; }
    public string reviewTitle { get; set; }
    public string reviewText { get; set; }
}

public class Price
{
    public Current current { get; set; }
}

public class Current
{
    public decimal value { get; set; }
    public string text { get; set; }
}
public class ProductData
{
    public List<Product> products { get; set; }
}
#endregion
