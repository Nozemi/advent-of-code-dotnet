using AdventOfCode.Library;
using AdventOfCode.Library.Extensions;
using AdventOfCode.Library.Utilities;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(config =>
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
    )
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(container =>
    {
        container.RegisterAssemblyTypes(typeof(IPuzzle).Assembly).As<IPuzzle>();
    })
    .UseSerilog();

var app = builder.Build();

var puzzles = app.Services
    .GetServices<IPuzzle>()
    .OrderBy(service => service.GetType().Name);

foreach (var puzzle in puzzles)
{
    var config = app.Services.GetService<IConfiguration>();
    if (config != null && bool.Parse(config["DownloadInput"] ?? "false") && config["AoCToken"] != null)
    {
        await PuzzleInputLoader.DownloadInput(puzzle.Year(), puzzle.Day(), config["AoCToken"]!, puzzle.InputFile());
    }

    long? part1 = null;
    long? part2 = null;
    if (!(await puzzle.RawInput()).IsEmpty())
    {
        part1 = await puzzle.SolvePart1();
        part2 = await puzzle.SolvePart2();
    }
    long? part1Example = null;
    long? part2Example = null;
    if (!(await puzzle.RawInput(true)).IsEmpty())
    {
        part1Example = await puzzle.SolvePart1(true);
        part2Example = await puzzle.SolvePart2(true);
    }
    
    Log.Information("== Solving {Name} ==", puzzle.GetType().Name);
    Log.Information("Part 1: {Answer} (Example: {ExampleAnswer})", part1, part1Example);
    Log.Information("Part 2: {Result} (Example: {ExampleAnswer})", part2, part2Example);
}