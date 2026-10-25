using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int time = 10)
    {
        var fixture = new UpdateCategoryTestFixture();
        for (int i = 0; i < 10; i++) 
        { 
            var exampleCategory = fixture.GetExampleCategory();
            var input = fixture.GetValidInput(exampleCategory.Id);

            yield return new object[] { exampleCategory, input };

        }
    }
}
