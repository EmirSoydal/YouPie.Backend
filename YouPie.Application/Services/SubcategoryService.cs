using Microsoft.Extensions.Logging;
using YouPie.Application.Interfaces;
using YouPie.Core.Interfaces;
using YouPie.Core.Models;

namespace YouPie.Application.Services;

public class SubcategoryService : ISubcategoryService
{
    private readonly ILogger _logger;
    private readonly IRepository<Subcategory> _repository;

    public SubcategoryService(ILogger logger, IRepository<Subcategory> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<Subcategory>> Get()
    {
        _logger.LogInformation("All Subcategories have been brought {Time}", DateTime.Now);
        return await _repository.GetAllAsync();
    }

    public async Task<Subcategory?> Get(string id)
    {
        var localSubcategory = await _repository.GetAsync(id);
        if (localSubcategory == null)
            _logger.LogWarning("Subcategory has not been found {Time}", DateTime.Now);
        _logger.LogInformation("Subcategory has been brought {Time}", DateTime.Now);
        return localSubcategory;
    }

    public async Task Post(Subcategory newSubcategory)
    {
        await _repository.CreateAsync(newSubcategory);
        _logger.LogInformation("New Subcategory has been created {Time}",DateTime.Now);
        
    }

    public async Task Post(IEnumerable<Subcategory> newSubcategories)
    {
        await _repository.CreateManyAsync(newSubcategories);
        _logger.LogInformation("New Subcategories have been created {Time}",DateTime.Now);
    }

    public async Task<bool> Put(string id, Subcategory updatedSubcategory)
    {
        var localSubcategory = await _repository.GetAsync(id);
        if (localSubcategory is null)
        {
            _logger.LogWarning("Subcategory could not been updated, no Subcategory found {Time}",DateTime.Now);
            return false;
        }

        updatedSubcategory.Id = localSubcategory.Id;
        await _repository.ReplaceAsync(id, updatedSubcategory);
        _logger.LogInformation("Subcategory has been updated {Time}",DateTime.Now);
        return true;
    }

    public async Task Delete()
    {
        await _repository.DeleteAllAsync();
        _logger.LogInformation("All Subcategories have been deleted {Time}", DateTime.Now);
    }

    public async Task<bool> Delete(string id)
    {
        var localSubcategory = await _repository.GetAsync(id);
        if (localSubcategory is null)
        {
            _logger.LogWarning("Subcategory could not been deleted, no Subcategory found {Time}",DateTime.Now);
            return false;
        }

        await _repository.DeleteAsync(id);
        _logger.LogInformation("Subcategory has been deleted {Time}", DateTime.Now);
        return true;
    }
}