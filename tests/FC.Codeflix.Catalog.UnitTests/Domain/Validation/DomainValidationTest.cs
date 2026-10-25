
using Bogus;
using Xunit;
using FC.Codeflix.Catalog.Domain.Validation;
using FluentAssertions;
using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOk()
    {
        var  value = Faker.Commerce.ProductName();
        Action action = () => DomainValidation.NotNull(value, "value");

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullThrowWhenNull()
    {
        string? value = null;
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.NotNull(value, fieldName);

        action.Should().Throw<EntityValidationException>($"{fieldName} should not be null");
    }
    
    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData(null)]
    [InlineData("")]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);
        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be empty or null");
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOK))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOrEmptyOK()
    { 
        var target = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);
        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLessThanMin))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetMinLengthOkData), parameters: 6)]
    public void MinLengthThrowWhenLessThanMin(string target, int min)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.MinLength(target, min, fieldName);
        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should be at least {min} characters long");
    }

    public static IEnumerable<object[]> GetMinLengthOkData(int numberOfTests = 6)
    {
        var faker = new Faker();
        for (int i = 1; i <= numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var min = target.Length + (new Random()).Next(1, 10);
            yield return new object[] { target, min };
        }
    }

    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValueGreaterThanMin), parameters: 6)]
    public void MinLengthOk(string target, int min)         
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.MinLength(target, min, fieldName);
        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValueGreaterThanMin(int numberOfTests = 6)
    {
        var faker = new Faker();
        for (int i = 1; i <= numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var min = target.Length - (new Random()).Next(1, 5);
            yield return new object[] { target, min };
        }
    }
    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreaterThanMax))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValueGreaterThanMax), parameters: 6)]
    public void MaxLengthThrowWhenGreaterThanMax(string target, int max)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.MaxLength(target, max, fieldName);
        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should be less than or equal to {max} characters long");
    }

    public static IEnumerable<object[]> GetValueGreaterThanMax(int numberOfTests = 6)
    {
        var faker = new Faker();
        for (int i = 1; i <= numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var max = target.Length - (new Random()).Next(1, 5);
            yield return new object[] { target, max };
        }
    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValueLessThanMax), parameters: 6)]
    public void MaxLengthOk(string target, int max)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.MaxLength(target, max, fieldName);
        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValueLessThanMax(int numberOfTests = 6)
    {
        var faker = new Faker();
        for (int i = 1; i <= numberOfTests; i++)
        {
            var target = faker.Commerce.ProductName();
            var max = target.Length + (new Random()).Next(0, 5);
            yield return new object[] { target, max };
        }
    }
}
