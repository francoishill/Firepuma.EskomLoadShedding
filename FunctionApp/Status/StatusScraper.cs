using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Firepuma.EskomLoadShedding.FunctionApp.Status.Exceptions;
using Firepuma.EskomLoadShedding.FunctionApp.Status.Models.ValueObjects;

namespace Firepuma.EskomLoadShedding.FunctionApp.Status;

public class StatusScraper
{
    private static readonly HttpClient _httpClient = new();

    public async Task<LoadSheddingStatus> GetCurrentCityOfCapeTownStatusAsync(CancellationToken cancellationToken)
    {
        const string url = "https://www.capetown.gov.za/Family%20and%20home/Residential-utility-services/Residential-electricity-services/Load-shedding-and-outages";

        var responseHtml = await _httpClient.GetStringAsync(url, cancellationToken);

        var parser = new HtmlParser();
        var document = await parser.ParseDocumentAsync(responseHtml);

        var pageContentHtml = document
            .QuerySelector(".page-content")
            ?.Text();

        return ParseCityOfCapeTownStatusHtml(pageContentHtml);
    }

    public LoadSheddingStatus ParseCityOfCapeTownStatusHtml(string loadSheddingTextHtml)
    {
        var linePattern = new Regex(@"(?<DateDay>[0-9]+) (?<DateMonth>[^ :]+): (?<Remaining>.*)", RegexOptions.Compiled);
        var stagePattern = new Regex(@"Stage (?<StageNum>[0-9]) from (?<StartTime>[0-9]{1,2}:[0-9]{1,2}) - (?<EndTime>[0-9]{1,2}:[0-9]{1,2})", RegexOptions.Compiled);

        var lines = loadSheddingTextHtml
            .Split("\n")
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim())
            .ToList();

        var reachedCityOfCapetown = false;
        foreach (var line in lines)
        {
            if (line.Contains("City customers:", StringComparison.Ordinal))
            {
                reachedCityOfCapetown = true;
                continue;
            }

            if (!reachedCityOfCapetown)
            {
                continue;
            }

            var lineMatch = linePattern.Match(line);
            if (!lineMatch.Success)
            {
                continue;
            }

            if (!int.TryParse(lineMatch.Groups["DateDay"].Value, out var dateDay))
            {
                throw new UnableToParseStatusException($"Date day value '{lineMatch.Groups["DateDay"].Value}' is not a valid integer value, full line is '{line}'");
            }

            if (!Enum.TryParse<MonthName>(lineMatch.Groups["DateMonth"].Value, true, out var dateMonth))
            {
                throw new UnableToParseStatusException($"Date month value '{lineMatch.Groups["DateMonth"].Value}' is not a valid month name, full line is '{line}'");
            }

            var stageMatches = stagePattern.Matches(lineMatch.Groups["Remaining"].Value);
            if (stageMatches.Count == 0)
            {
                continue;
            }

            foreach (Match stageMatch in stageMatches)
            {
                var stageNum = stageMatch.Groups["StageNum"].Value;
                var startTime = stageMatch.Groups["StartTime"].Value;
                var endTime = stageMatch.Groups["EndTime"].Value;

                // TODO: continue here
            }
        }

        throw new NotImplementedException();
    }
}