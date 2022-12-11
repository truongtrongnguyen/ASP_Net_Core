using System.Net;
using System.Net.Mail;
namespace B64_Gui_Mail;


public class MailUtils
{
    // Xây dựng phương thức gởi Mail tại localhost, Máy chủ phải có cài Transporter hoặc MailServer
    // người gởi, người nhận, tiêu đề, nội dung
    public static async Task<string> SendMail(string from, string to, string subject, string body)
    {
        MailMessage message = new MailMessage(from, to, subject, body);

        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        message.IsBodyHtml = true;

        // Thiết lập địa chỉ phản hồi là người gởi Mail
        message.ReplyToList.Add(new MailAddress(from));
        message.Sender = new MailAddress(from); // Thiết lập thông tin người gởi Mail
        // --> Đây là message được stmp Client kết nối đến server để gởi đi

        using var smtpClient = new SmtpClient("localhost");    // tạo smtpClient để thực hiện gởi Mail

        try
        {
            await smtpClient.SendMailAsync(message);   // Tiến hành gởi Mail
            return "Gui Mail thanh cong";
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return "Gui Mail that bai";
        }

    }

    // người gởi, người nhận, tiêu đề, nội dung, email xác nhận, passwork
    public static async Task<string> SendGMail(string from, string to, string subject, string body, string gmail, string passwork)
    {
        MailMessage message = new MailMessage(from, to, subject, body);
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.IsBodyHtml = true;

        // Thiết lập địa chỉ phản hồi là người gởi Mail
        message.ReplyToList.Add(new MailAddress(from));
        // Thiết lập thông tin người gởi Mail
        message.Sender = new MailAddress(from);

        // tạo smtpClient để thực hiện gởi Mail
        using var smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Port = 587;
        smtpClient.EnableSsl = true;
        // Thiết lập địa chỉ xác nhận
        smtpClient.Credentials = new NetworkCredential(gmail, passwork);

        try
        {
            await smtpClient.SendMailAsync(message);
            return "Goi Mail thanh cong";
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return "Goi Mail that bai";
        }

    }
}