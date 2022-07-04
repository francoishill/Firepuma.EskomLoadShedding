using Firepuma.EskomLoadShedding.FunctionApp.Status;
using Firepuma.EskomLoadShedding.FunctionApp.Status.Models.ValueObjects;

namespace Firepuma.EskomLoadShedding.Tests.FunctionApp.Status;

public class StatusScraperTests
{
    [Fact]
    public async Task Temp()
    {
        // Arrange
        var scraper = new StatusScraper();
        var responseHtml = await TestHelpers.GetTestDataFileContentString("Status", "LoadSheddingAndOutagesSnippet1.html");

        // Act
        var status = scraper.ParseCityOfCapeTownStatusHtml(responseHtml);

        /*
                        Eskom load-shedding active: 2-3 July
                        Eskom customers:
                        2 July: Stage 4 from 07:00 - 22:00 and Stage 2 from 22:00 - 07:00 on Sunday morning.
                        3 July: Stage 4 from 07:00 - 22:00 and Stage 2 from 22:00 - 07:00 on Monday morning.

                        City customers:
                        2 July: Stage 3 from 07:00 - 22:00 and Stage 2 from 22:00 - 07:00 on Sunday morning.
                        3 July: Stage 3 from 07:00 - 22:00.
         */

        // Assert
        Assert.Equal(LoadSheddingStatus.Stage3, status);
    }
}