using System.Linq.Expressions;
using CleanArchitectureTemplate.Domain.Constants;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Repositories;

public class DishesRepository(ApplicationDbContext dbContext) : GenericRepository<Dish>(dbContext), IDishesRepository
{
    public override async Task<(List<Dish>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, params Expression<Func<Dish, object>>[] includeProperties)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = DbSet.AsQueryable();

        // thực hiện filter data nếu có 
        baseQuery = baseQuery.Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                                       || r.Description.ToLower().Contains(searchPhraseLower)));

        // thưc hiện Include nếu có 
        foreach (var includeProperty in includeProperties)
        {
            baseQuery = baseQuery.Include(includeProperty);
        }

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            // xác định các column có thể Sort 
            var columnsSelector = new Dictionary<string, Expression<Func<Dish, object>>>
            {
                { nameof(Dish.Name), r => r.Name },
                { nameof(Dish.Description), r => r.Description },
                { nameof(Dish.Price), r => r.Price },
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
