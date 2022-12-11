
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class TestOptionsMiddleaware : IMiddleware
{
    // Inject Options được cấu hình trong file appsetting.json vào Middleware
    private TestOptions _testOptions {get; set;}
    private ProductName _productName {get; set;}
    public TestOptionsMiddleaware(IOptions<TestOptions> testOptions, ProductName productName)
    {
        _testOptions = testOptions.Value;
        _productName = productName;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("TESOPIONS su dung TestOptionsMiddleware: \n");
        stringBuilder.Append($"TestOptions.opt_key1: {_testOptions.opt_key1}\n");
        stringBuilder.Append($"TestOptions.opt_key2.k1: {_testOptions.opt_key2.k1}\n");
        stringBuilder.Append($"TestOptions.opt_key2.k2: {_testOptions.opt_key2.k2}\n");    
        foreach(var i in _productName.GetName())
        {
            stringBuilder.Append($"ProductName: {i}\n");
        }    
        await context.Response.WriteAsync("Show options in TestOptionsMiddleware\n");
        await context.Response.WriteAsync(stringBuilder.ToString());
        await next(context);
    }
}