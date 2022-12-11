using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Test
{
    public class Startup
    {
        private IConfiguration _configuration {get; set;}
        public Startup (IConfiguration configuration)
        {
            // khi Startup được khởi tạo thì nó tự động bơm Dependency Inject này vào, và qua đó ta lấy được đối tượng IConfiguration 
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();  // Gọi phương thức kích hoạt Options
            services.AddTransient<TestOptionsMiddleaware>();
            services.AddSingleton<ProductName>();

            // Tiến hành thêm TestOptions vào dịch vụ ứng dụng để inject vào các DI khác
            // Và ta phải lấy nó ra thông qua đối tượng IOptions<TestOptions>
            var testOptions = _configuration.GetSection("TestOptions");
            services.Configure<TestOptions>(testOptions);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<TestOptionsMiddleaware>();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/ShowOptions",async (HttpContext context) => {
                // Configuration là dịch vụ được khởi tạo cùng với ứng dụng ASP.Net  
                // Những ứng dụng được cấu hình trong file appsettings.json được nạp thông qua dịch vụ Configuration
                    

                    #region Cách 1: 
                    // Và để lấy ra dịch vụ đó trì thông qua IApplicationBuilder 
                    //var configuration = context.RequestServices.GetService<IConfiguration>();

                    // var testOptions = configuration.GetSection("TestOptions");
                    // var opt_key1 = testOptions["opt_key1"];
                    // var k1 = testOptions.GetSection("opt_key2")["k1"];
                    // var k2 = testOptions.GetSection("opt_key2")["k2"];

                    // var stringBuilder = new StringBuilder();
                    // stringBuilder.Append($"TESOPTIONS\n");
                    // stringBuilder.Append($"TestOptions.opt_key1: {opt_key1}\n");
                    // stringBuilder.Append($"TestOptions.opt_key2.k1: {k1}\n");
                    // stringBuilder.Append($"TestOptions.opt_key2.k2: {k2}\n");
                    #endregion

                    #region Cách 2: Lấy các Options thông qua đối tượng
                    // Và để lấy ra dịch vụ đó trì thông qua IApplicationBuilder 
                    //var configuration = context.RequestServices.GetService<IConfiguration>();

                    // var testOptions = configuration.GetSection("TestOptions").Get<TestOptions>();
                    // var stringBuilder = new StringBuilder();
                    // stringBuilder.Append("TESOPIONS su dung doi tuong: \n");
                    // stringBuilder.Append($"TestOptions.opt_key1: {testOptions.opt_key1}\n");
                    // stringBuilder.Append($"TestOptions.opt_key2.k1: {testOptions.opt_key2.k1}\n");
                    // stringBuilder.Append($"TestOptions.opt_key2.k2: {testOptions.opt_key2.k2}\n");
                    #endregion

                    #region Cách 3: Lấy thông qua Dependency Inject 
                    var testOptions = context.RequestServices.GetService<IOptions<TestOptions>>().Value;

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("TESOPIONS su dungIOptions<TestOptions>: \n");
                    stringBuilder.Append($"TestOptions.opt_key1: {testOptions.opt_key1}\n");
                    stringBuilder.Append($"TestOptions.opt_key2.k1: {testOptions.opt_key2.k1}\n");
                    stringBuilder.Append($"TestOptions.opt_key2.k2: {testOptions.opt_key2.k2}\n");
                    #endregion
                    await context.Response.WriteAsync(stringBuilder.ToString());
                });
            });
        }
    }
}
