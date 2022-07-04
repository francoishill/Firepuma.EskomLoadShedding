using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Exceptions;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;

namespace Firepuma.EskomLoadShedding.FunctionApp.Schedules;

public class RawSchedulesParser
{
    public async Task<LoadSheddingSchedule> ParseScheduleAsync(int stageNumber)
    {
        var fileName = $"Stage{stageNumber}.txt";
        var stageContent = await GetScheduleFileContentString(fileName);

        return ParseSchedule(fileName, stageContent);
    }

    private static LoadSheddingSchedule ParseSchedule(string fileName, string stageContent)
    {
        var lines = stageContent
            .Split("\n")
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim())
            .ToList();

        var linePattern = new Regex(@"(?<StartTime>[0-9]{1,2}:[0-9]{1,2}) (?<EndTime>[0-9]{1,2}:[0-9]{1,2}) (?<RemainingLine>[0-9, ]+)", RegexOptions.Compiled);
        var areasPerDayOfMonthPattern = new Regex(@"(([0-9](, ?)?)+) ?", RegexOptions.Compiled);

        var schedule = new LoadSheddingSchedule();

        var blockNumber = 0;
        foreach (var line in lines)
        {
            var lineMatch = linePattern.Match(line);

            if (!lineMatch.Success)
            {
                throw new UnableToParseScheduleException($"File {fileName} line '{line}', it does not match pattern '{linePattern.ToString()}'");
            }

            var startTime = lineMatch.Groups["StartTime"].Value;
            var endTime = lineMatch.Groups["EndTime"].Value;
            var remainingLine = lineMatch.Groups["RemainingLine"].Value;

            var parsedStartTime = TimeSpan.Parse(startTime);
            var parsedEndTime = TimeSpan.Parse(endTime);

            var timeRange = new LoadSheddingSchedule.TimeRange(parsedStartTime, parsedEndTime);

            if (startTime == "00:00")
            {
                blockNumber++;
            }

            var areaMatches = areasPerDayOfMonthPattern.Matches(remainingLine);

            var numColumnsPerRow = areaMatches.Count;

            if (numColumnsPerRow == 0)
            {
                throw new UnableToParseScheduleException($"File {fileName} line '{line}', the part after start/end time does not match pattern '{areasPerDayOfMonthPattern.ToString()}'");
            }

            if (numColumnsPerRow != 16 && numColumnsPerRow != 8)
            {
                throw new UnableToParseScheduleException($"File {fileName} line '{line}', unexpected match count {numColumnsPerRow} (expected 16 or 8) of pattern '{areasPerDayOfMonthPattern.ToString()}'");
            }

            var columnNumber = 0;
            foreach (Match areaMatch in areaMatches)
            {
                columnNumber++;

                var areas = areaMatch.Value
                    .Split(",")
                    .Where(areaNum => !string.IsNullOrWhiteSpace(areaNum))
                    .Select(areaNum => int.Parse(areaNum.Trim()))
                    .ToList();

                var daysOfMonth = GetDaysOfMonthForCell(numColumnsPerRow, blockNumber, columnNumber)
                    .Where(day => day <= 31)
                    .ToList();

                if (daysOfMonth.Count == 0)
                {
                    throw new UnableToParseScheduleException($"File {fileName} line '{line}', cannot get days of month for cell, numColumnsPerRow={numColumnsPerRow}, blockNumber={blockNumber}, columnNumber={columnNumber}");
                }

                foreach (var dayOfMonth in daysOfMonth)
                {
                    if (!schedule.DayOfMonthAndSlotsMappings.TryGetValue(dayOfMonth, out var slotsAndAreas))
                    {
                        slotsAndAreas = new LoadSheddingSchedule.SlotsAndAreas();
                        schedule.DayOfMonthAndSlotsMappings.Add(dayOfMonth, slotsAndAreas);
                    }

                    if (!slotsAndAreas.SlotsAndAreasMap.TryGetValue(timeRange, out var listOfAreas))
                    {
                        listOfAreas = new List<int>();
                        slotsAndAreas.SlotsAndAreasMap.Add(timeRange, listOfAreas);
                    }

                    listOfAreas.AddRange(areas);
                }
            }
        }

        return schedule;
    }

    private static IEnumerable<int> GetDaysOfMonthForCell(
        int numColumnsPerRow,
        int blockNumber,
        int columnNumber)
    {
        var days = new List<int>();

        //TODO: the below can be simplified algorithmically
        if (numColumnsPerRow == 16)
        {
            days.Add(columnNumber);
            days.Add(columnNumber + 16);
        }
        else if (numColumnsPerRow == 8)
        {
            if (blockNumber == 1)
            {
                days.Add(columnNumber);
                days.Add(columnNumber + 16);
            }
            else if (blockNumber == 2)
            {
                days.Add(columnNumber + 8);
                days.Add(columnNumber + 24);
            }
        }

        return days;
    }

    private static async Task<string> GetScheduleFileContentString(string fileName)
    {
        var stream = GetScheduleFileContentStream(fileName);
        using var streamReader = new StreamReader(stream);
        return await streamReader.ReadToEndAsync();
    }

    private static Stream GetScheduleFileContentStream(string fileName)
    {
        var assembly = typeof(RawSchedulesParser).Assembly;
        var namespaceName = typeof(RawSchedulesParser).Namespace;
        var fileFullName = $"{namespaceName}.Data." + fileName.TrimStart('\\').Replace("\\", ".");
        var resourceStream = assembly.GetManifestResourceStream(fileFullName);

        if (resourceStream == null)
        {
            throw new Exception($"Unable to read file '{fileFullName}'");
        }

        return resourceStream;
    }
}