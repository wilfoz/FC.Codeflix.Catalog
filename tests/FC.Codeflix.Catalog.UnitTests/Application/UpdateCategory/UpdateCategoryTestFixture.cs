using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixtureCollection))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>
{
}

public class UpdateCategoryTestFixture: BaseFixture
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

    public bool GetRandomBoolean() => Faker.Random.Bool();

    public Category GetExampleCategory()
        => new Category(
            name: GetValidCategoryName(),
            description: GetValidCategoryDescription(),
            isActive: GetRandomBoolean()
        );


    public UpdateCategoryInput GetValidInput(Guid? id = null)
        => new UpdateCategoryInput(
            id: id ?? Guid.NewGuid(),
            name: GetValidCategoryName(),
            description: GetValidCategoryDescription(),
            isActive: GetRandomBoolean()
        );
}
