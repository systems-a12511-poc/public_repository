using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;

namespace backend
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            // Startupクラスを使用してASP.NET Coreアプリケーションの設定を行う
            builder.UseStartup<Startup>();
        }
    }
}
