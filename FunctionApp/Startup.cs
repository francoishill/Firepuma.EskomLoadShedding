using Firepuma.EskomLoadShedding.FunctionApp;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Firepuma.EskomLoadShedding.FunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<RawSchedulesParser>();
    }
}