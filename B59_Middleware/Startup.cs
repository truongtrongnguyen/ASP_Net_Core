namespace Middleware_B59;

public class Startup{
    // Đăng ký các dịch vụ của ứng dụng
    public void ConfigureServices(IServiceCollection services)
    {
        // Do SecondMiddleware được kế thừa từ IMiddleware nên ta sẽ phải đăng ký dịch vụ của ứng dụng
        services.AddSingleton<SecondMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStaticFiles();

        // app.UseMiddleware<FirstMiddleware>();   // Sử dụng Middleware tự tạo
        app.UseFirstMiddleware();                   // Sử dụng Method thay cho dòng trên
        app.UseSecondMiddleware();

        // Nếu có Middleware trả về thì nó sẽ dừng ngay tại EndpoinRouting chứ không chạy đến cuối file
        app.UseRouting();
        app.UseEndpoints(endpoint => {
            // E1
            endpoint.MapGet("/about.html", async (HttpContext context) => {
                await context.Response.WriteAsync("Trang goi thieu");
            });
            // E2
            endpoint.MapGet("/product.html", async (HttpContext context) => {
                await context.Response.WriteAsync("Trang san pham");
            });
        });

        // Rẽ nhánh pipeline
        app.Map("/admin", app1 => {
            app1.UseRouting();
            app1.UseEndpoints(endpoint => {
                endpoint.MapGet("/user", async (HttpContext context) => {
                    await context.Response.WriteAsync("Trang User");
                });
                endpoint.MapGet("/customer", async (HttpContext context) => {
                    await context.Response.WriteAsync("Trang Khach hang");
                });   
            }); 
            app1.Run(async (HttpContext context) => {
                await context.Response.WriteAsync("Trang Admin");
            });
        });

        // Temianta Middleware M1  --> Không gọi Middleware phía sau
        app.Run(async (HttpContext context) => {
            await context.Response.WriteAsync("Trang Middleware");
        });
    }
}
/*
    pipeline : UseStaticFile 
                -> FirstMiddleware
                        -> SecondMiddleware 
                            -> EndpointRoutingMiddleware (Nếu E1, E2 có trả về thì nó sẽ dừng lại ngay tại đây)
                                -> M1
*/