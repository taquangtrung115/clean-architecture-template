using System.Linq.Expressions;
using CleanArchitectureTemplate.Domain.Constants;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Repositories;

public class RestaurantsRepository(ApplicationDbContext dbContext) : GenericRepository<Restaurant>(dbContext), IRestaurantsRepository
{
    public override async Task<(List<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, params Expression<Func<Restaurant, object>>[] includeProperties)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = DbSet.AsQueryable();

        // thực hiện filter data nếu có 
        baseQuery = baseQuery.Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                                       || r.Description.ToLower().Contains(searchPhraseLower)));

        // thưcj hiện Include nếu có 
        foreach (var includeProperty in includeProperties)
        {
            baseQuery = baseQuery.Include(includeProperty);
        }

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            // xác định các column có thể Sort 
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category },
            };

            if (columnsSelector.TryGetValue(sortBy, out var selectedColumn))
            {
                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }
        }

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }
}
