using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using VF.Serenity.AutomationFramework.Configuration;

namespace VF.Serenity.AutomationFramework
{
    public sealed class TestsBootstrap
    {
        private static readonly Lazy<TestsBootstrap> Inst =
            new Lazy<TestsBootstrap>(() => new TestsBootstrap(), true);

        internal readonly IConfigurationRoot Configuration;

        private TestsBootstrap()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", true, false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ServiceProvider = ConfigureServices();
        }

        public static TestsBootstrap Instance => Inst.Value;

        public IServiceProvider ServiceProvider { get; }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ILoggerFactory>(p =>
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddSerilog(p.GetRequiredService<Serilog.Core.Logger>());
                return loggerFactory;
            });
            var serenitySection = Configuration.GetSection("SerenitySection").Get<SerenitySection>();
            services.AddSingleton(serenitySection);
            return services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (ServiceProvider is ServiceProvider provider)
                provider.Dispose();
        }
    }
}
