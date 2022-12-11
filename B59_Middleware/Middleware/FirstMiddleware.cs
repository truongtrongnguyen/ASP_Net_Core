namespace Middleware_B59;

// Lớp tạo ra Middleware tự định nghĩa
public class FirstMiddleware
{
    private readonly RequestDelegate _next;
    //Tạo contructor để tham chiếu đến các Middleware phía sau
    // RequestDelegate ~ async (HttpContext context) => {}
    public FirstMiddleware(RequestDelegate next)
    {
         _next = next;
    }
    // Phương thức này sẽ được gọi khi HttpContext đi qua Middleware trong pipeline
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"URL: {context.Request.Path}");
         // await context.Response.WriteAsync($"<p>URL: {context.Request.Path}</p>"); //--> sẽ báo lỗi do nó được thiết lập trước Headers
        // Tryền dữ liệu cho Middleware phía sau
        context.Items.Add("DataFirstMiddleware", $"<p>URL: {context.Request.Path}</p>");  
        await _next(context);   // Chuyển HttpContext cho các middleware phía sau
        // Nếu không chuyển Middleware thì nó sẽ dừng ngay tại trang này 
    }
}