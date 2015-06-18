using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using CheckInOut.DAL.Context;
using CheckInOut.DAL.Models;

namespace CheckInOut.DAL.Helpers
{
    public class Utils
    {
        private readonly CheckInOutContext _context = new CheckInOutContext();

        public string GetVisitorAccessNumber(int companyId)
        {

            const string chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 4)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());

            while (_context.Visitors.Any(s => s.AccessNumber == result && s.CompanyId == companyId))
            {
                result = new string(
                    Enumerable.Repeat(chars, 4)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
            }
            return result;
        }

        public string GetEmployeeAccessNumber(int branchId)
        {

            const string chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 4)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());

            while (_context.Employees.Any(s => s.AccessNumber == result && s.BranchId == branchId))
            {
                result = new string(
                    Enumerable.Repeat(chars, 4)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
            }
            return result;
        }

        public bool SignInMail(string toAddress, string subject, string body, int companyId)
        {
            try
            {
                var toEmails = toAddress.Split(';');

                var configurationEmail = _context.ConfigurationEmails.FirstOrDefault(s => s.CompanyId == companyId);
                if (configurationEmail != null)
                {
                    var sender = new MailAddress(configurationEmail.UserName, configurationEmail.Company.Name);

                    var smtp = new SmtpClient
                    {
                        Host = configurationEmail.SmtpClient,
                        Port = configurationEmail.Port,
                        EnableSsl = configurationEmail.EnableSsl,
                        UseDefaultCredentials = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(sender.Address, configurationEmail.Password),
                        Timeout = 20000
                    };

                    var msg = new MailMessage
                    {
                        From = sender,
                        Subject = subject,
                        Body = body + Environment.NewLine + Environment.NewLine +
                               "*This is an automated email please do not respond*",
                    };

                    for (int i = 0; i < toEmails.Count(); i++)
                    {
                        msg.To.Add(new MailAddress(toEmails[i]));
                    }

                    var ccEmails = configurationEmail.CcEmails.Split(';');
                    for (int j = 0; j < ccEmails.Count(); j++)
                    {
                        msg.CC.Add(new MailAddress(ccEmails[j]));
                    }

                    smtp.Send(msg);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public bool SignInMail(string toAddress, string subject, string body, int companyId)
        //{
        //    try
        //    {
        //        var toEmails = toAddress.Split(';');

        //        var configurationEmail = _context.ConfigurationEmails.FirstOrDefault(s => s.CompanyId == companyId);
        //        if (configurationEmail != null)
        //        {
        //            SmtpClient mySmtpClient = new SmtpClient(configurationEmail.SmtpClient);

        //            // set smtp-client with basicAuthentication
        //            mySmtpClient.UseDefaultCredentials = false;
        //            NetworkCredential basicAuthenticationInfo = new
        //                NetworkCredential(configurationEmail.UserName, configurationEmail.Password);
        //            mySmtpClient.Credentials = basicAuthenticationInfo;

        //            // add from,to mailaddresses
        //            MailAddress from = new MailAddress(configurationEmail.UserName, configurationEmail.Company.Name);
        //            //MailAddress to = new MailAddress("test2@example.com", "TestToName");



        //            //MailMessage myMail = new MailMessage(from, to);

        //            MailMessage myMail = new MailMessage
        //            {
        //                From = from,
        //                Subject = subject,
        //                Body = body + Environment.NewLine + Environment.NewLine +
        //                       "*This is an automated email please do not respond*",
        //            };

        //            for (int i = 0; i < toEmails.Count(); i++)
        //            {
        //                myMail.To.Add(new MailAddress(toEmails[i]));
        //            }

        //            // add ReplyTo
        //            //MailAddress replyto = new MailAddress("reply@example.com");
        //            //myMail.ReplyTo = replyto;

        //            // set subject and encoding
        //            //myMail.Subject = "Test message";
        //            myMail.SubjectEncoding = Encoding.UTF8;

        //            // set body-message and encoding
        //           // myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
        //            myMail.BodyEncoding = Encoding.UTF8;
        //            // text or html
        //            myMail.IsBodyHtml = true;

        //            mySmtpClient.Send(myMail);
        //        }
        //        return true;
        //    }

        //    catch (SmtpException ex)
        //    {
        //        throw new ApplicationException
        //            ("SmtpException has occured: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public Branch GetBranch(int deviceId)
        {
            return _context.Branches.FirstOrDefault(s => s.Devices.Any(d => d.Id == deviceId));
        }

    }
}
