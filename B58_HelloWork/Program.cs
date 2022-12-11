using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HelloWork_With_ASPNetCore
{
    public class Program
    {
        /*
            Host (IHost) object:(Ben trong Host gom)
                - Dependency Inversion (ID): IServiceProvider (SelectCollection)
                - Logging (ILogging)
                - Configuration
                -IHostService => StartAsync: Run HTTP server (Kestrel http)

            1. Tao IHostBuilder no se sinh ra Host
            2. Cau hinh, dang ky cac dich vu (goi ConfigureWebHostDefaults)
            3. IHostBuilder.Build() => Host (IHost)
            4. Host.Run();
        */
        // static void Main(string [] args)
        // {
        //     IHostBuilder builder = Host.CreateDefaultBuilder(args);
        //     // Cau hinh mac dinh cho Host tao ra
        //     builder.ConfigureWebHostDefaults(( IWebHostBuilder webBuilder) =>{
        //         // Tuy bien them ve Host
        //         // webBuilder
        //         // chi ra lop su dung pipeline(Middleware)
        //         webBuilder.UseWebRoot("Publish");   // Tao ra mot file va su dung de tra ve cac file duoc luu trong do khi request la 1 file
        //         webBuilder.UseStartup<MyStartUp>();
        //     });
        //     IHost host = builder.Build();
        //     host.Run();
        // }





        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
