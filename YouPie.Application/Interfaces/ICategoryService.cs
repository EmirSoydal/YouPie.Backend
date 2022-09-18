using YouPie.Core.Models;

namespace YouPie.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> Get();
    Task<Category?> Get(string id);
    Task Post(Category newCategory);
    Task Post(IEnumerable<Category> newCategories);
    Task<bool> Put(string id, Category updatedCategory);
    Task Delete();
    Task<bool> Delete(string id);
}