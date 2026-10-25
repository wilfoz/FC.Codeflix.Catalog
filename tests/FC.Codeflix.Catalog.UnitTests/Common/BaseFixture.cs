using Bogus;

namespace FC.Codeflix.Catalog.UnitTests.Common;
public class BaseFixture
{
    public Faker Faker { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
    }
}
