﻿using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Repositories;

namespace E_commerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        //{
        //    return await _categoryRepository.GetAllCategoriesAsync();
        //}
        public async Task<IEnumerable<CategorySummaryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            // Map to DTOs
            var categoryDtos = categories.Select(c => new CategorySummaryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            });

            return categoryDtos;
        }




        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteCategoryAsync(categoryId);
        }
    }
}
