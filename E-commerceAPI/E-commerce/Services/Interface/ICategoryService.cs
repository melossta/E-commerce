using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;

namespace E_commerce.Services
{
    public interface ICategoryService
    {
        //Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<CategorySummaryDto>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}
