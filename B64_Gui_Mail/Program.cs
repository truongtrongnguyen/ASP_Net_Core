using Microsoft.Extensions.Options;

namespace B64_Gui_Mail;

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


// public class Program
// {
//     private static IConfiguration _configuration { get; set; }
//     public Program(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }
//     public static void Main(string[] args)
//     {
//         var builder = WebApplication.CreateBuilder(args);

//         builder.Services.AddOptions();
//         var mailsetting = _configuration.GetSection("MailSetting");
//         builder.Services.Configure<MailSetting>(mailsetting);

//         // Đăng ký SendMailServices là một dịch vụ để MailSetting nó tự động inject vào, MailSetting lúc này thuộc IOptions<MailSetting>    
//         builder.Services.AddTransient<SendMailServices>();

//         var app = builder.Build();

//         app.UseRouting();
//         app.UseEndpoints((IEndpointRouteBuilder endpoints) =>
//         {
//             endpoints.MapGet("/TestSendMail", async (HttpContext context) =>
//             {
//                 var message = await MailUtils.SendMail("trongnguyenkhanhhung@gmail.com", "trongnguyenkhanhhung@gmail.com", "TestMail", "HelloWork");

//                 await context.Response.WriteAsync(message);
//             });

//             endpoints.MapGet("/TestSendMailServices", async (HttpContext context) =>
//             {
//                 var sendMailServices = context.RequestServices.GetService<SendMailServices>();

//                 var mailContent = new MailContent();
//                 mailContent.to = "trongnguyenkhanhhung@gmail.com";
//                 mailContent.subject = "Hoc goi Mail";
//                 mailContent.body = "<h2>Test Goi Mail</h2>";

//                 var kq = await sendMailServices.SendMail(mailContent);
//                 await context.Response.WriteAsync(kq);
//             });

//             endpoints.MapGet("/TestSendGMail", async (HttpContext context) =>
//             {
//                 var message = await MailUtils.SendGMail("trongnguyenkhanhhung@gmail.com", "trongnguyenkhanhhung@gmail.com", "TestMail", "HelloWork 2022", "trongnguyenkhanhhung@gmail.com", "vsydvuvlstbfnbik");

//                 await context.Response.WriteAsync(message);
//             });
//         });
//         app.MapGet("/", () => "Hello World!");
//         app.Run();
//     }

//}
/*
    Truy cập để tạo passwork gởi mail: 
    https://myaccount.google.com/apppasswords?rapt=AEjHL4O1FieQpcp8V4iMk9AeD6QlYEN7PPKSbr4hMnEAAM-L7B4dLKi22hCFiNnwSQb25RzaSH_NA9CK9pk0gCauhVKHL-16Tw

    Sử dụng thư viện gởi MailKit:
        dotnet add package MailKit
        dotnet add package MimeKit
    Trước tiên cần tạo cấu hình gởi Mail trong appsetting.json
*/

