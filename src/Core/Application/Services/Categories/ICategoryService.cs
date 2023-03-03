using Application.Services.Common;
using Domain.Entitites.Categories;

namespace Application.Services.Categories
{
    public interface ICategoryService : ITransientService
    {
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);
    }
}
