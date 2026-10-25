namespace FC.Codeflix.Catalog.Application.UseCases.Category.Commom;
using DomainEntity = Domain.Entity;
public class CategoryModelOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public CategoryModelOutput(
        Guid id, 
        string name, 
        string? description, 
        bool isActive, 
        DateTime createdAt
     )
    {
        Id = id;
        Name = name;
        Description = description ?? "";
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public static CategoryModelOutput FromCategory(DomainEntity.Category category)
        => new (
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt
        );

}
