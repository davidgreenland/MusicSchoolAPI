﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Diagnostics.Metrics;
using System.Net;

namespace MusicSchool.Services;

public class CategoryService : ICategoryService
{
    private readonly MusicSchoolDBContext _context;

    public CategoryService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
    {
        return await _context.Category
            .OrderBy(c => c.CategoryName)
            .Select(c => new CategoryResponse(c.Id, c.CategoryName))
            .ToListAsync();
    }

    public async Task<ApiResponse<CategoryResponse>> GetCategoryAsync(int id)
    {
        var category = await _context.Category
                .Include(c => c.Instruments)
                .SingleOrDefaultAsync(x => x.Id == id);

        return category == null
            ? new ApiResponse<CategoryResponse>(HttpStatusCode.NotFound, message: null)
            : new ApiResponse<CategoryResponse>(HttpStatusCode.OK, new CategoryResponse(category.Id, category.CategoryName, category.Instruments));
    }

    public async Task<ApiResponse<Category>> UpdateCategoryAsync(int id, [FromBody] UpdateCategory request)
    {
        var category = await _context.Category
            .SingleOrDefaultAsync(x => x.Id == id);

        if (category == null) 
        {
            return new ApiResponse<Category>(HttpStatusCode.NotFound, "Id not found");
        }

        category.CategoryName = request.NewCategoryName;
        await _context.SaveChangesAsync();

        return new ApiResponse<Category>(HttpStatusCode.OK, category);
    }

    public async Task<ApiResponse<Category>> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
    {
        var existingCategory = await _context.Category
            .SingleOrDefaultAsync(c => c.CategoryName == request.CategoryName);

        if (existingCategory != null)
        {
            return new ApiResponse<Category>(HttpStatusCode.Conflict, "Category already exists");
        }

        var newCategory = new Category { CategoryName = request.CategoryName };
        _context.Category.Add(newCategory);
        await _context.SaveChangesAsync();

        return new ApiResponse<Category>(HttpStatusCode.Created, newCategory);
    }

    public async Task<ApiResponse<Category>> DeleteCategoryAsync(int id)
    {
        var category = await _context.Category
            .SingleOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return new ApiResponse<Category>(HttpStatusCode.NotFound, $"Instrument {id} not found");
        }

        var categoryHasInstrument = await _context.Instrument
            .AnyAsync(x => x.CategoryId == id);

        if (categoryHasInstrument)
        {

            return new ApiResponse<Category>(HttpStatusCode.Conflict, "Unable to delete category");
        }

        _context.Category.Remove(category);
        await _context.SaveChangesAsync();

        return new ApiResponse<Category>(HttpStatusCode.NoContent, data: null);
    }
}
