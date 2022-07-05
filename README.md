# Firepuma.EskomLoadShedding

A project to parse and represent Eskom (South Africa) loadshedding stages and schedules.

**Note: This project currently only contains code to process the City of Cape Town loadshedding schedules and stages and is still a work in progress.**

## Deploy

Deploy the `FunctionApp/FunctionApp.csproj` project as an Azure Function.

See [.github/workflows/deploy-function-app.yml](.github/workflows/deploy-function-app.yml) for an example for deploying via Github actions, or visit [the documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-github-actions?tabs=dotnet).

## Development

Open the `Firepuma.EskomLoadShedding.sln` solution file. `FunctionApp/Api/*` contains the http triggers (API endpoints of the Azure Function). `Tests` project contains the unit tests.

### GetLoadsheddingCalendar API endpoint

This endpoint renders the loadshedding events as an [ICalendar](https://datatracker.ietf.org/doc/html/rfc5545) (internet calendar or ical for short) for the given `customerType` and `areaNumber`. The default `startDaysAgo` and `endInDays` values can be overwritten with query params.

Code for generating the calendar lives in `FunctionApp/Infrastructure/Formatters/VCalendarOutputFormatter.cs` and is implicitly used when creating a new `new OkVCalendarObjectResult`.

### GetLoadsheddingSlots API endpoint

This endpoint responds with an array of time ranges for the given `customerType`, `stage`, `areaNumber` and `date`.

```json
[
	{
		"start": "04:00:00",
		"end": "06:30:00"
	},
	{
		"start": "20:00:00",
		"end": "22:30:00"
	}
]
```