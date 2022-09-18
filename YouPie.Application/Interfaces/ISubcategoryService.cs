using YouPie.Core.Models;

namespace YouPie.Application.Interfaces;

public interface ISubcategoryService
{
    Task<IEnumerable<Subcategory>> Get();
    Task<Subcategory?> Get(string id);
    Task Post(Subcategory newSubcategory);
    Task Post(IEnumerable<Subcategory> newSubcategories);
    Task<bool> Put(string id, Subcategory updatedSubcategory);
    Task Delete();
    Task<bool> Delete(string id);
}