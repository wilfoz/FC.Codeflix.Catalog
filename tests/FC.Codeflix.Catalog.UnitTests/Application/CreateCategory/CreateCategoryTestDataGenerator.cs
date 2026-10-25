namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<Object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputsList = new List<Object[]>();
        var totalInvalidCases = 4;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases) {
                case 0:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should be at least 3 characters long"
                    });
                    break;
                case 1:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputLongName(),
                        "Name should be less than or equal to 255 characters long"
                    });
                    break;
                case 2:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputDescriptionNull(),
                        "Description should not be null"
                    });
                    break;
                case 3:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputDescriptionLong(),
                        "Description should be less than or equal to 10000 characters long"
                    });
                    break;
                default:
                    break;
            }
        }
        return invalidInputsList;
    }
}
