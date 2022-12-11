namespace Middleware_B59;

// Tạo Middleware được kế thừa từ IMiddleware
public class SecondMiddleware : IMiddleware
{
    // context là nội dung Middleware phía trước đó, còn next là để nó truyền Middleware cho phía sau
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)  // implement IMiddleware
    {
        if(context.Request.Path == "/xxx.html")
        {
            // Khi mà ta thiết lập Response.WriteAsync trước thiết lập Headers thì nó sẽ báo lỗi nên ta phỉa để nó sau cùng
            context.Response.Headers.Add("SecondMiddleware", "Ban khong duoc truy cap");
            // Nó dừng tại trang này nên ta sẽ cho nó xuất ra dữ liệu của Middleware phía trước
            var DataFirstMiddleware = context.Items["DataFirstMiddleware"];
            if(DataFirstMiddleware != null)
            {
                await context.Response.WriteAsync((string)DataFirstMiddleware);
            }

            await context.Response.WriteAsync("Ban khong duoc truy cap");
        }
        else
        {
            context.Response.Headers.Add("SecondMiddleware", "Ban duoc truy cap");
            var DataFirstMiddleware = context.Items["DataFirstMiddleware"];
            if(DataFirstMiddleware != null)
            {
                await context.Response.WriteAsync((string)DataFirstMiddleware);
            }
            await next(context);
        }
    }
}