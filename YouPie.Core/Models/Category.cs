namespace YouPie.Core.Models;

public class Category : BaseEntity
{
    private string Title { get; set; } = null!;
    private string[] SubcategoryIds { get; set; } = null!;
}