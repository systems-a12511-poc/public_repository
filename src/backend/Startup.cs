using Amazon.Lambda.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using backend.DbContext;
using backend.Middleware;
using Microsoft.Extensions.Logging;

namespace backend;

[LambdaStartup]
public class Startup
{
    /// <summary>
    /// Services for Lambda functions can be registered in the services dependency injection container in this method. 
    ///
    /// The services can be injected into the Lambda function through the containing type's constructor or as a
    /// parameter in the Lambda function using the FromService attribute. Services injected for the constructor have
    /// the lifetime of the Lambda compute container. Services injected as parameters are created within the scope
    /// of the function invocation.
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {


        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });

        // Dapperのデフォルト設定を変更
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        // ASP.NET Coreのコントローラーを追加
        services.AddControllers();
        services.AddLogging();
        // カスタムサービスの登録
        services.AddScoped<MyDbContext>();

        // TODO: ※一時対応※ Swaggerから各APIを実行可能↓
        // CORSポリシーの設定を追加（この例では全てのオリジン・メソッド・ヘッダーを許可）
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy", builder =>
            {
                builder.WithOrigins(
                    "http://localhost:5173"
                )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            });
        });
        // TODO:↑

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        Console.WriteLine("【Startup Configureからのログ】");
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        // TODO: ※一時対応※ Swaggerから各APIを実行可能↓
        // CORSミドルウェアを追加
        app.UseCors("AllowAllPolicy");
        // TODO:↑

        app.UseMiddleware<AuthorizationMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // ルートをコントローラーにマップ
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome root path.");
            });
        });

    }
}
