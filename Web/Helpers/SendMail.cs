using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace Cats.Helpers
{
    public class SendMail
    {
        /// <summary>
        /// An SMTP engine with attachment functionality.
        /// </summary>

            public string From { get; set; }
            public string To { get; set; }
            public string Body { get; set; }
            public string Subject { get; set; }
            public string SMTPAddr { get; set; }
            public int Port { get; set; }
            /// <summary>
            /// Full path of the attachment file including its filename
            /// </summary>
            public string FileName { get; set; }
            public bool HTML { get; set; }



            //send
            public SendMail(string From, string To, string Subject, string Body, string FileName, bool HTML, string smtpAddress, string userName, string password, int port)
            {
                string SMTPAddr;
                int Port;

                if (From == null || To == null)
                {
                    throw new ArgumentException("Both the From and To parts are needed.");
                }

                SMTPAddr = smtpAddress;
                Port = port;

                if (SMTPAddr == null)
                {
                    throw new ArgumentException("SMTP host is needed.");
                }

                if (Port < 1 && Port > 65535)
                {
                    throw new ArgumentException("Invalid Port number.");
                }




                SmtpClient client = new SmtpClient(SMTPAddr, Port);

                MailAddress msg_From = new MailAddress(From);

                MailAddress msg_To = new MailAddress(To);

                MailMessage message = new MailMessage(From, To);

                message.Body = Body;
                message.Subject = Subject;
                message.IsBodyHtml  = true;
                message.Body = Body;
               
                if (FileName != null)
                {
                    Attachment attach = new Attachment(FileName, MediaTypeNames.Application.Octet);

                    ContentDisposition disposition = attach.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(FileName);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(FileName);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(FileName);

                    message.Attachments.Add(attach);
                }

                try
                {

                    NetworkCredential loginInfo = new NetworkCredential(userName, password);
                    client.UseDefaultCredentials = false;
                    client.Port = port;
                    client.Credentials = (ICredentialsByHost)loginInfo;
                    client.EnableSsl = true;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    // log, pop up...

                }

            }


            public SendMail(string smtpAddress, int port)
            {
                HTML = true;
                SMTPAddr = smtpAddress;
                Port = port;
            }

            /// <summary>
            /// Sends an e-mail message to an SMTP server for delivery
            /// </summary>
            public int Send()
            {
                if (From == null || To == null)
                {
                    throw new ArgumentException("Both the From and To parts are needed.");
                }

                if (Port < 1 || Port > 65535)
                {
                    throw new ArgumentException("Invalid Port number.");
                }

                SmtpClient client = new SmtpClient(SMTPAddr, Port);

                MailAddress msg_From = new MailAddress(From);

                MailAddress msg_To = new MailAddress(To);

                MailMessage message = new MailMessage(From, To);
                message.Body = Body;
                message.Subject = Subject;
                message.IsBodyHtml = HTML;

                if (FileName != null)
                {
                    Attachment attach = new Attachment(FileName, MediaTypeNames.Application.Octet);

                    ContentDisposition disposition = attach.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(FileName);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(FileName);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(FileName);

                    message.Attachments.Add(attach);
                }

                try
                {
                    client.Send(message);
                    return 0;
                }
                catch (Exception ex)
                {
                    // log, pop up...
                    return -1;
                }
            }
        }
    
}