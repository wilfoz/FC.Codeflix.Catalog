using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixtureCollection))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture>
{
}

public class GetCategoryTestFixture: BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new Mock<ICategoryRepository>();

    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3 || categoryName.Length > 255)
        {
            categoryName = Faker.Commerce.Categories(1)[0];
        }
        if (categoryName.Length > 255)
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
        if (categoryDescription.Length > 10000)
        {
            categoryDescription = categoryDescription.Substring(0, 10000);
        }
        return categoryDescription;
    }

    public DomainEntity.Category GetValidCategory() => new DomainEntity.Category(
        name: GetValidCategoryName(),
        description: GetValidCategoryDescription()
    );
}
