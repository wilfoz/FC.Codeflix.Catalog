using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixtureCollection))]
public class CreateCategoryTestFixtureCollection: ICollectionFixture<CreateCategoryTestFixture>
{
}
public class CreateCategoryTestFixture : BaseFixture
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

    public CreateCategoryInput GetValidInput()
        => new CreateCategoryInput(
            name: GetValidCategoryName(),
            description: GetValidCategoryDescription(),
            isActive: GetRandomBoolean()
        );

    public CreateCategoryInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
        return invalidInputShortName;
    }

    public CreateCategoryInput GetInvalidInputLongName()
    {
        var invalidInputLongName = GetValidInput();
        invalidInputLongName.Name = new string('a', 256);
        return invalidInputLongName;
    }

    public CreateCategoryInput GetInvalidInputDescriptionNull()
    {
        var invalidInputDescriptionNull = GetValidInput();
        invalidInputDescriptionNull.Description = null!;
        return invalidInputDescriptionNull;
    }

    public CreateCategoryInput GetInvalidInputDescriptionLong()
    {
        var invalidInputDescriptionLong = GetValidInput();
        invalidInputDescriptionLong.Description = new string('a', 10001);
        return invalidInputDescriptionLong;
    }
}
