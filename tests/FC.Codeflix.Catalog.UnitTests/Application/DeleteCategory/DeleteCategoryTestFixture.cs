using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Application.GetCategory;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace FC.Codeflix.Catalog.UnitTests.Application.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixtureCollection))]
public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>
{
}

public class DeleteCategoryTestFixture: BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new Mock<ICategoryRepository>();
    public Mock<IUnitOfWorks> GetUnitOfWorkMock() => new Mock<IUnitOfWorks>();

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

    public Category GetValidCategory() => new (
        name: GetValidCategoryName(),
        description: GetValidCategoryDescription()
    );
}
