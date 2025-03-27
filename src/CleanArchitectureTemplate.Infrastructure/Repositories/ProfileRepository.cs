using System.Linq.Expressions;
using CleanArchitectureTemplate.Domain.Constants;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Repositories;

public class ProfileRepository(ApplicationDbContext dbContext) : GenericRepository<Profile>(dbContext), IProfileRepository
{
    public override async Task<(List<Profile>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, params Expression<Func<Profile, object>>[] includeProperties)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = DbSet.AsQueryable();

        // thực hiện filter data nếu có 
        baseQuery = baseQuery.Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                                      ));

        // thưc hiện Include nếu có 
        foreach (var includeProperty in includeProperties)
        {
            baseQuery = baseQuery.Include(includeProperty);
        }

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            // xác định các column có thể Sort 
            var columnsSelector = new Dictionary<string, Expression<Func<Profile, object>>>
            {
                { nameof(Profile.Name), r => r.Name },
                { nameof(Profile.DayOfBirth), r => r.DayOfBirth },
                { nameof(Profile.LastName), r => r.LastName },
            };

            if (columnsSelector.TryGetValue(sortBy, out var selectedColumn))
            {
                //baseQuery = sortDirection == SortDirection.Ascending
                //    ? baseQuery.OrderBy(selectedColumn)
                //    : baseQuery.OrderByDescending(selectedColumn);
            }
        }

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }
}
