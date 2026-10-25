using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.Domain.Validation;
public class DomainValidation
{
    public static void NotNull(object? value, string name)
    {
        if (value is null)
        {
            throw new EntityValidationException($"{name} should not be null");
        }
    }
    public static void NotNullOrEmpty(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EntityValidationException($"{name} should not be empty or null");
        }
    }
    public static void MinLength(string value, int min, string name)
    {
        if (value.Length < min)
        {
            throw new EntityValidationException($"{name} should be at least {min} characters long");
        }
    }
    public static void MaxLength(string value, int max, string name)
    {
        if (value.Length > max)
        {
            throw new EntityValidationException($"{name} should be less than or equal to {max} characters long");
        }
    }
}
