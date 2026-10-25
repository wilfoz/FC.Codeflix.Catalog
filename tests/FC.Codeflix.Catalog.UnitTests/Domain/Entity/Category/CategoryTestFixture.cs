using FC.Codeflix.Catalog.UnitTests.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;
public class CategoryTestFixture: BaseFixture
{
    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3 || categoryName.Length > 255)
        {
            categoryName = Faker.Commerce.Categories(1)[0];
        }
        if(categoryName.Length > 255)
        {
            categoryName = categoryName.Substring(0, 255);
        }
        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = "";
        while (categoryDescription.Length < 3 || categoryDescription.Length > 10000)
        {
            categoryDescription = Faker.Commerce.ProductDescription();
        }
        if(categoryDescription.Length > 10000)
        {
            categoryDescription = categoryDescription.Substring(0, 10000);
        }
        return categoryDescription;
    }

    public DomainEntity.Category GetValidCategory() => new DomainEntity.Category(
        name: GetValidCategoryName(),
        description: GetValidCategoryDescription()
    );

    public CategoryTestFixture(): base()
    {
        // You can add any additional setup or initialization code here if needed
    }
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{
}
