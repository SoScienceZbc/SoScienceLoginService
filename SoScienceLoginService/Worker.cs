using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoScienceLoginService
{
    class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        #region ServiceSetup
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //TODO: AddSSL in the "lo.UseHttps(Path,Name)"
            _logger.LogInformation("Worker running at: {time} just before Creating LoginService", DateTimeOffset.Now);
            string cert = "/home/soscience/Desktop/Services/soscience.dk.pfx";
            string pass = File.ReadAllText("/home/soscience/Desktop/Services/PassPhrase.txt");
            await Host.CreateDefaultBuilder().ConfigureWebHostDefaults(cw =>
            {
                cw.UseKestrel().UseStartup<GrpcAgent>().ConfigureKestrel(kj =>
                {
                    kj.Listen(System.Net.IPAddress.Any, 48053, lo =>
                    {
                        lo.Protocols = HttpProtocols.Http2;
                        lo.UseHttps(cert,pass.Trim());
                    });
                });
            }).Build().StartAsync(stoppingToken);

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
#endregion
    }
}
