
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

public class SendMailServices
{
    private MailSetting _mailSetting {get;set;}
    public SendMailServices(IOptions<MailSetting> mailSetting)
    {
        _mailSetting = mailSetting.Value;
    }
    public async Task<string> SendMail(MailContent mailContent)
    {
        // Tạo một đối tượng để gởi Mail, Sử dụng thư viện MimeKit
        var email = new MimeMessage();
        // Thiết lập người gởi, gồm tên và địa chỉ
        email.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail);
        // Thiết lập gởi từ 
        email.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail));
        // Thiết lập người nhận
         email.To.Add(new MailboxAddress(mailContent.to, mailContent.to));
         email.Subject =  mailContent.body;

         // Thiết lập nội dung gởi đi
         var builder = new BodyBuilder();
         builder.HtmlBody = mailContent.body;
         // Ngoài ra còn có thể thiết lập nhiều kiểu gởi đi như file, textbody,, ...
         email.Body = builder.ToMessageBody();

         // Tạo đối tượng gởi Mail
         using var smtp = new MailKit.Net.Smtp.SmtpClient();
         
         try
         {
            await smtp.ConnectAsync(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls);  // Mở kết nối
            // Tiến hành xác thực Email
            await smtp.AuthenticateAsync(_mailSetting.Mail, _mailSetting.Passwork);
            await smtp.SendAsync(email);
         }
         catch(Exception e)
         {
            Console.WriteLine(e.Message);
            return "Loi"+e.Message;
         }
         // Sau khi gởi xong thì ngắt kết nối
        smtp.Disconnect(true);
        return "Gui Mail SendMailServices thanh cong";
    }
}

// Thiết lập class chứa người nhận, tiêu đề, nội dung
public class MailContent
{
    public string? to {get; set;}
    public string? subject {get; set; }
    public string? body {get; set; }
}