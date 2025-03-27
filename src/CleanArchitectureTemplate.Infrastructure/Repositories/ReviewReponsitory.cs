﻿using CleanArchitectureTemplate.Domain.Constants;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Infrastructure.Repositories;

public class ReviewReponsitory(ApplicationDbContext dbContext) : GenericRepository<Review>(dbContext), IReviewReponsitory
{
    public override async Task<(List<Review>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy,
      SortDirection sortDirection, params Expression<Func<Review, object>>[] includeProperties)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = DbSet.AsQueryable();

        // thực hiện filter data nếu có 
        baseQuery = baseQuery.Where(r => searchPhraseLower == null || (r.ReviewCode.ToLower().Contains(searchPhraseLower)
                                                                       || r.UserName.ToLower().Contains(searchPhraseLower)
                                                                       || r.Location.ToLower().Contains(searchPhraseLower)
                                                                       || r.ReviewTitle.ToLower().Contains(searchPhraseLower)
                                                                       || r.ReviewContent.ToLower().Contains(searchPhraseLower)
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
            var columnsSelector = new Dictionary<string, Expression<Func<Review, object>>>
            {
                { nameof(Review.CreationDate), r => r.CreationDate },
                { nameof(Review.ReviewCode), r => r.ReviewCode },
                { nameof(Review.ReviewTitle), r => r.ReviewTitle },
                { nameof(Review.Location), r => r.Location },
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
