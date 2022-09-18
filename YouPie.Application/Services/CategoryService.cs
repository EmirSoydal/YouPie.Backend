using Microsoft.Extensions.Logging;
using YouPie.Application.Interfaces;
using YouPie.Core.Interfaces;
using YouPie.Core.Models;

namespace YouPie.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ILogger _logger;
    private readonly IRepository<Category> _repository;

    public CategoryService(ILogger logger, IRepository<Category> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> Get()
    {
        _logger.LogInformation("All Categories have been brought {Time}", DateTime.Now);
        return await _repository.GetAllAsync();
    }

    public async Task<Category?> Get(string id)
    {
        var localCategory = await _repository.GetAsync(id);
        if (localCategory == null)
            _logger.LogWarning("Category has not been found {Time}", DateTime.Now);
        _logger.LogInformation("Category has been brought {Time}", DateTime.Now);
        return localCategory;
    }

    public async Task Post(Category newCategory)
    {
        await _repository.CreateAsync(newCategory);
        _logger.LogInformation("New Category has been created {Time}",DateTime.Now);
        
    }

    public async Task Post(IEnumerable<Category> newCategories)
    {
        await _repository.CreateManyAsync(newCategories);
        _logger.LogInformation("New Categories have been created {Time}",DateTime.Now);
    }

    public async Task<bool> Put(string id, Category updatedCategory)
    {
        var localCategory = await _repository.GetAsync(id);
        if (localCategory is null)
        {
            _logger.LogWarning("Category could not been updated, no Category found {Time}",DateTime.Now);
            return false;
        }

        updatedCategory.Id = localCategory.Id;
        await _repository.ReplaceAsync(id, updatedCategory);
        _logger.LogInformation("Category has been updated {Time}",DateTime.Now);
        return true;
    }

    public async Task Delete()
    {
        await _repository.DeleteAllAsync();
        _logger.LogInformation("All Categories have been deleted {Time}", DateTime.Now);
    }

    public async Task<bool> Delete(string id)
    {
        var localCategory = await _repository.GetAsync(id);
        if (localCategory is null)
        {
            _logger.LogWarning("Category could not been deleted, no Category found {Time}",DateTime.Now);
            return false;
        }

        await _repository.DeleteAsync(id);
        _logger.LogInformation("Category has been deleted {Time}", DateTime.Now);
        return true;
    }
}