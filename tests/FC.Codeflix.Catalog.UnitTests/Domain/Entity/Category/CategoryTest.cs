using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;
namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _fixture;

    public CategoryTest(CategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName =nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validData = _fixture.GetValidCategory();
        var datetimeBefore = DateTime.Now;

        var category = new DomainEntity.Category(validData.Name, validData.Description);
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBe(Guid.Empty);
        category.CreatedAt.Should().NotBe(default(DateTime));
        category.CreatedAt.Should().BeOnOrAfter(datetimeBefore);
        category.CreatedAt.Should().BeOnOrBefore(datetimeAfter);
        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var validData = _fixture.GetValidCategory();
        var datetimeBefore = DateTime.Now;

        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBe(Guid.Empty);
        category.CreatedAt.Should().NotBe(default(DateTime));
        category.CreatedAt.Should().BeOnOrAfter(datetimeBefore);
        category.CreatedAt.Should().BeOnOrBefore(datetimeAfter);
        category.IsActive.Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        var validCategory = _fixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(name!, validCategory.Description);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Name should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var validCategory = _fixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(validCategory.Name, null!);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Description should not be null");
    }
    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(GetNamesGreaterThan255Characters), parameters: 10)]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string name)
    {

        Action action = () => new DomainEntity.Category(name, "Category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Name should be at least 3 characters long");
    }

    public static IEnumerable<object[]> GetNamesGreaterThan255Characters(int numberOfTests = 6)
    {
        var fixture = new CategoryTestFixture();
        var uniqueNames = new HashSet<string>();
        while (uniqueNames.Count < numberOfTests)
        {
            var isOdd = uniqueNames.Count % 2 == 1;
            var name = fixture.Faker.Random.String2(isOdd ? 1 : 2);
            if (uniqueNames.Add(name))
            {
                yield return new object[] { name };
            }
        }
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var validCategory = _fixture.GetValidCategory();
        var invalidName = _fixture.Faker.Lorem.Letter(256);
        Action action = () => new DomainEntity.Category(invalidName, validCategory.Description);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Name should be less than or equal to 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var validCategory = _fixture.GetValidCategory();
        var invalidDescription = _fixture.Faker.Lorem.Letter(10_001);
        Action action = () => new DomainEntity.Category(validCategory.Name, invalidDescription);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Description should be less than or equal to 10000 characters long");
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Activate()
    {
        var validData = _fixture.GetValidCategory();

        var category = new DomainEntity.Category(validData.Name, validData.Description, false);
        category.Activate();

        Assert.True(category.IsActive);
    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Deactivate()
    {
        var validData = _fixture.GetValidCategory();

        var category = new DomainEntity.Category(validData.Name, validData.Description, true);
        category.Deactivate();

        category.IsActive.Should().BeFalse();
    }
    
    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void Update() 
    { 
        var validCategory = _fixture.GetValidCategory();
        var categoryWithNewValues = _fixture.GetValidCategory();

        validCategory.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);

        validCategory.Name.Should().Be(categoryWithNewValues.Name);
        validCategory.Description.Should().Be(categoryWithNewValues.Description);
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateOnlyName() 
    {
        var validCategory = _fixture.GetValidCategory();
        var categoryWithNewValues = _fixture.GetValidCategory();
        validCategory.Update(categoryWithNewValues.Name);
        Assert.Equal(categoryWithNewValues.Name, validCategory.Name);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var validCategory = _fixture.GetValidCategory();
        Action action = () => validCategory.Update(name!);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Name should not be empty or null");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("Ca")]
    [InlineData("A")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string name)
    {
        var validCategory = _fixture.GetValidCategory();
        Action action = () => validCategory.Update(name);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Name should be at least 3 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "A").ToList());
        var validCategory = _fixture.GetValidCategory();
        Action action = () => validCategory.Update(invalidName);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Name should be less than or equal to 255 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(0, 10_001).Select(_ => "A").ToList());
        var validCategory = _fixture.GetValidCategory();
        Action action = () => validCategory.Update(validCategory.Name, invalidDescription);
        var exception = Assert.Throws<EntityValidationException>(action);
        exception.Message.Should().Be("Description should be less than or equal to 10000 characters long");
    }

}
