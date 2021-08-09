using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace SoScienceLoginService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();

                //Test code below..
                //services.AddGrpc().Services.BuildServiceProvider(new ServiceProviderOptions {ValidateOnBuild = true });
                //Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(x =>
                //{
                //    x.UseStartup<GrpcAgent>();
                //});
                //services.AddHostedService<GrpcAgent>();                    

            });
    }
}
