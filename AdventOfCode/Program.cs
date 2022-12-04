﻿using AdventOfCode.Library.Puzzle;
using AdventOfCode.Puzzles;
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

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(config =>
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
    )
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(container =>
    {
        container.RegisterType<PuzzleSolver>().SingleInstance();
        container.RegisterAssemblyTypes(typeof(IPuzzle).Assembly).As<IPuzzle>().InstancePerLifetimeScope();
    })
    .UseSerilog()
    .UseConsoleLifetime()
    .Build();
    
host.Services.GetService<PuzzleSolver>()?.FindAndSolvePuzzles();