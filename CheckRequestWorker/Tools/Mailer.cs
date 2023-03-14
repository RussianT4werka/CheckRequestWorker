using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckRequestWorker.Tools
{
    internal static class Mailer
    {
        static SmtpClient client;
        private static string user;

        static void LoadOptions()
        {
            var lines = File.ReadAllLines("MailSettings.ini");
            client = new SmtpClient();
            client.Host = lines[0].Split('=')[1].Trim();
            client.Port = int.Parse(lines[1].Split('=')[1].Trim());
            NetworkCredential auth = new NetworkCredential
            {
                UserName = lines[2].Split('=')[1].Trim(),
                Password = lines[3].Split('=')[1].Trim()
                //SecurePassword =new System.Security.SecureString()
            };
            /*var pass = lines[3].Split('=')[1].Trim();
            foreach (var p in pass)
                auth.SecurePassword.AppendChar(p);*/
            user = auth.UserName;
            client.UseDefaultCredentials = false;
            client.Credentials = auth;
            //client.EnableSsl = true;
        }

        public static void SendMessage(string email, string message)
        {
            try
            {
                LoadOptions();
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(user);
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = "Отказано в посещение";
                mailMessage.Body = message;
                //client.Send(user, email, "Отказано в посещение", message);
                //Task.Run(async() => await client.SendMailAsync(mailMessage));
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        //MafioznikLe2281337@yandex.ru ShumilovkaGorodok
    }
}
