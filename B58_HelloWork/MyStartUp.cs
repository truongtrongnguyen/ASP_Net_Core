using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWork_With_ASPNetCore
{
    public class MyStartUp
    {
        // Phuong thuc nay dung de dang ky cac dich vu DI
        public void ConfigureServices(IServiceCollection service)
        {
            // service.AddSingleton();
        }

        // xay dung pipeline (Middleware)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
           // UseStaticFiles nen duoc viet ngay dau 
           // khi 1 request co chua dia chi la 1 ten file thi no se chay vao day tra ve file can tim do
           app.UseStaticFiles();


            // Dung de phan tich dieu huong va tao ra cac endpoinrouting
            // No se phan tich neu Request la Get thi no chay vao UseEndpoints con khong thi no chay xuong duoi
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async (context) => {
                    string html = "Trang chu";
                    await context.Response.WriteAsync(html);
                });

                endpoints.MapGet("/about.html", async (HttpContext Context) => {
                    await Context.Response.WriteAsync("Trang gioi thieu");
                });

                endpoints.MapGet("/contact", async (HttpContext Context) => {
                    await Context.Response.WriteAsync("Trang lien he");
                });
            });

            app.Map("/abc", (IApplicationBuilder app1) => {
                app1.Run(async (HttpContext context) =>{
                    await context.Response.WriteAsync("Trang abc");
                });
            });

             // Duoc dung khi tat ca dia chi truy cap khong co trong he thong
            app.UseStatusCodePages();


            // Truong hop Run nay it khi nguoi ta su dung(nen dong comment) ma thay vao do la su dung Use StatusCodePage 
            //    app.Run( async(HttpContext context) => {
            //     await context.Response.WriteAsync("Xin chao day la MyStartUp");
            // });
        }
    }
}
