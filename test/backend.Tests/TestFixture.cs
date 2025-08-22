using backend.DbContext;
using backend.Tests.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace backend.Tests;

public class TestFixture : IDisposable
{
    public MyDbContext DbContext { get; private set; }
    public ILogger Logger { get; private set; }

    public TestFixture()
    {
        // Dapperのデフォルト設定を変更
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var serviceCollection = new ServiceCollection();

        // IConfiguration の設定
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables("")
            .Build();
        serviceCollection.AddSingleton<IConfiguration>(configuration);

        // Logger　の設定
        serviceCollection.AddLogging();
        // DbContext の設定
        serviceCollection.AddScoped<MyDbContext>();
        // RequestContext の設定
        serviceCollection.AddScoped(typeof(ILogger<>), typeof(LoggerForTest<>));

        var serviceProvider = serviceCollection.BuildServiceProvider();

        DbContext = serviceProvider.GetRequiredService<MyDbContext>();
        Logger = serviceProvider.GetRequiredService<ILogger<TestFixture>>();
    }

    public void Dispose()
    {
        // リソースの解放が必要な場合はこちらに記述
    }
}