namespace Middleware_B59;

public class Program
{
    public static void Main(string[] args)
    {
       CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string [] args)
    {
        //IHostBuilder builder = 
        return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => {
            
            webBuilder.UseStartup<Startup>();
        });
     //   return builder;
    }
}
