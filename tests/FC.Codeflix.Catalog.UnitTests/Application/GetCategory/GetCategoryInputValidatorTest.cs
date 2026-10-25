using FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FC.Codeflix.Catalog.Application.Exceptions;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Application.GetCategory;

[Collection(nameof(GetCategoryTestFixtureCollection))]
public class GetCategoryInputValidatorTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryInputValidatorTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ValidationOk))]
    [Trait("Application", "GetCategoryInputValidation - Use Cases")]
    public void ValidationOk()
    {
        var input = new GetCategoryInput(Guid.NewGuid());
        var validator = new GetCategoryInputValidator();
        var validationResult = validator.Validate(input);
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(InvalidWhenEmptyId))]
    [Trait("Application", "GetCategoryInputValidation - Use Cases")]
    public void InvalidWhenEmptyId()
    {
        var input = new GetCategoryInput(Guid.Empty);
        var validator = new GetCategoryInputValidator();
        var validationResult = validator.Validate(input);
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors[0].ErrorMessage.Should().Be("Id should not be empty");
    }
}
