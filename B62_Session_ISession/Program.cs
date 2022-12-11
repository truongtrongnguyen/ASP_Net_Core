using Microsoft.Extensions.Caching.SqlServer;

namespace B62_Session_ISession;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddDistributedMemoryCache();   // Dữ liệu được lưu ở phía máy chủ

        // Cấu hình lưu xuống Database
        builder.Services.AddDistributedSqlServerCache((SqlServerCacheOptions options) => {
            options.ConnectionString = @"Data Source=DESKTOP-OJA04UG\SQLEXPRESS;Initial Catalog=TestData; Integrated Security=True";
            options.SchemaName = "dbo";
            options.TableName = "Session";
        });

        builder.Services.AddSession((SessionOptions options) => {
            options.Cookie.Name = "truongtrongnguyen";
            options.IdleTimeout = new TimeSpan(0, 30, 0);   // 30 phut
        });
        var app = builder.Build();

        app.UseSession(); // Gọi phương thức sử dụng Session
         // Thường thì Session sẽ gửi cho trình duyệt một Cookies và lần truy vấn tiếp theo sẽ dựa vào Cookies đó gởi lên Server để phục hồi dữ liệu

        

        app.UseRouting();
        app.UseEndpoints((IEndpointRouteBuilder endpoint) => {
            endpoint.MapGet("/readwriteSession", async (HttpContext context) => {
                int? count;
                count = context.Session.GetInt32("count");
                if(count ==null)
                {
                    count = 0;
                }
                count +=1;
                context.Session.SetInt32("count", count.Value);
                await context.Response.WriteAsync("So lan truy cap trang readwriteSession la: "+ count);
            });
        });
        app.MapGet("/", () => "Hello World!");
        app.Run();

    }
}
/*
    Cần tích hợp 2 Packeg:
        dotnet add package Microsoft.AspNetCore.Session
        dotnet add package Microsoft.Extensions.Caching.Memory

    Tích hợp packeg để tạo table lưu Session trên sql
        dotnet new tool-manifest # if you are setting up this repo
        dotnet tool install --local dotnet-sql-cache --version 7.0.0
    chạy lệnh để tạo table:
        dotnet sql-cache create "Data Source=DESKTOP-OJA04UG\SQLEXPRESS;Initial Catalog=TestData; Integrated Security=True;" dbo Session  
*/