using System;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Configuration;

namespace Lib.Models
{
    public static class Common
    {
        public static DateTime GetHKTime()
        {
            DateTime dt = DateTime.Now.ToUniversalTime();
            dt = TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            return dt;
        }

        public static DateTime GetGMTime(DateTime ust)
        {
            DateTime dt;//= ust.ToUniversalTime();
            dt = ust.AddHours(5);
            return dt;
        }

        public static DateTime GetHKTime(DateTime ust)
        {
            DateTime dt;
            dt = GetGMTime(ust);
            dt = dt.AddHours(8);// (TimeZoneInfo.FindSystemTimeZoneById("China Standard Time").BaseUtcOffset);

            return dt;
        }

        public static bool EmailError(Exception ex, System.Web.HttpRequest Request, string UserName, int? UserId, string ErrorFrom)
        {
            //-- Start [29-10-15] By Aniket Doc-1151.
            string[] PName = Request.Url.ToString().Split('/');
            string ErrPage = null;
            if (PName.Length > 0)
            {
                for (int i = 0; i < PName.Length; i++)
                {
                    if (PName[i].ToString().Contains(".aspx"))
                    {
                        ErrPage = PName[i].ToString();
                        if (ErrPage.Contains('?'))
                        {
                            string[] temp = ErrPage.Split('?');
                            ErrPage = temp[0].ToString();
                        }
                    }
                }
            }

            //ErrorLogDataContext errDataContext = new ErrorLogDataContext();
            //errDataContext.ErrorLog_Insert(GetHKTime(), UserId, Request.UserHostAddress.ToString(), ex.StackTrace.ToString(), ex.Message, ErrorFrom, ErrPage);

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, GetHKTime()));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, UserId));
            para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, Request.UserHostAddress.ToString()));
            para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.StackTrace.ToString()));
            para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.Message));
            para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrorFrom));
            para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrPage));
            db.ExecuteSP("ErrorLog_Insert", para.ToArray(), false);



            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p>Error raise from Sunrise Diamond.</p>");
                loSb.Append(@"<b>Date Time: </b>" + GetHKTime().ToString() + "<br />");
                loSb.Append(@"<b>Location: </b>" + Request.Url.ToString() + "<br />");
                loSb.Append(@"<b>Message: </b>" + ex.Message + "<br />");
                loSb.Append(@"<b>Trace: </b>" + ex.StackTrace.ToString() + "<br />");
                if (ex.InnerException != null)
                {
                    loSb.Append(@"<b>Inner Messsag: </b>" + ex.InnerException.Message.ToString() + "<br />");
                    loSb.Append(@"<b>Inner Trace: </b>" + ex.InnerException.StackTrace.ToString() + "<br />");
                }
                if (UserName != "" && UserName != null)
                    loSb.Append(@"<b>Username: </b>" + UserName + "<br />");
                else
                    loSb.Append(@"<b>Username: </b>" + HttpContext.Current.User.Identity.Name + "<br />");

                loSb.Append(@"<b>IP: </b>" + Request.UserHostAddress.ToString() + "<br />");
                loSb.Append(@"<b>DNS: </b>" + Request.UserHostName.ToString() + "<br />");
                loSb.Append(@"<b>Browser: </b>" + Request.Browser.Type.ToString() + "<br />");
                loSb.Append(@"<b>Browser Version: </b>" + Request.Browser.Version.ToString() + "<br />");
                loSb.Append(@"<b>Agent: </b>" + Request.UserAgent.ToString() + "<br />");

                loSb.Append(EmailSignature());

                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail2"]);

                loMail.Subject = "Error - Sunrise – " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss");
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(Convert.ToString(loSb), null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                loSmtp.Send(loMail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailError(Exception ex, System.Web.HttpRequest Request, string UserName, string fsSender, string fsExtraText, int UserId, string ErrorFrom)
        {
            //-- Start [29-10-15] By Aniket Doc-1151.
            string[] PName = Request.Url.ToString().Split('/');
            string ErrPage = null;
            if (PName.Length > 0)
            {
                for (int i = 0; i < PName.Length; i++)
                {
                    if (PName[i].ToString().Contains(".aspx"))
                    {
                        ErrPage = PName[i].ToString();
                        if (ErrPage.Contains('?'))
                        {
                            string[] temp = ErrPage.Split('?');
                            ErrPage = temp[0].ToString();
                        }
                    }
                }
            }
            //-- Over [29-10-15]

            //--By Aniket on [22-04-2015] To Store Error log (added UserId and ErrorFrom Parameters above).
            //ErrorLogDataContext errDataContext = new ErrorLogDataContext();
            //errDataContext.ErrorLog_Insert(GetHKTime(), UserId, Request.UserHostAddress.ToString(), ex.StackTrace.ToString(), ex.Message, ErrorFrom, ErrPage);

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, GetHKTime()));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, UserId));
            para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, Request.UserHostAddress.ToString()));
            para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.StackTrace.ToString()));
            para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.Message));
            para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrorFrom));
            para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrPage));
            db.ExecuteSP("ErrorLog_Insert", para.ToArray(), false);

            //--Over [22-04-2015] 

            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p>Error raise from Sunrise Diamond.</p>");
                loSb.Append(@"<p><b>" + fsExtraText + "</b></p>");
                loSb.Append(@"<b>Date Time: </b>" + GetHKTime().ToString() + "<br />");
                loSb.Append(@"<b>Location: </b>" + Request.Url.ToString() + "<br />");
                loSb.Append(@"<b>Message: </b>" + ex.Message + "<br />");
                loSb.Append(@"<b>Trace: </b>" + ex.StackTrace.ToString() + "<br />");
                if (ex.InnerException != null)
                {
                    loSb.Append(@"<b>Inner Messsag: </b>" + ex.InnerException.Message.ToString() + "<br />");
                    loSb.Append(@"<b>Inner Trace: </b>" + ex.InnerException.StackTrace.ToString() + "<br />");
                }

                if (UserName != "" && UserName != null)
                    loSb.Append(@"<b>Username: </b>" + UserName + "<br />");
                else
                    loSb.Append(@"<b>Username: </b>" + HttpContext.Current.User.Identity.Name + "<br />");

                loSb.Append(@"<b>IP: </b>" + Request.UserHostAddress.ToString() + "<br />");
                loSb.Append(@"<b>DNS: </b>" + Request.UserHostName.ToString() + "<br />");
                loSb.Append(@"<b>Browser: </b>" + Request.Browser.Type.ToString() + "<br />");
                loSb.Append(@"<b>Browser Version: </b>" + Request.Browser.Version.ToString() + "<br />");
                loSb.Append(@"<b>Agent: </b>" + Request.UserAgent.ToString() + "<br />");

                loSb.Append(EmailSignature());

                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsSender);
                loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail2"]);

                loMail.Subject = "Error - Sunrise – " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss");
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(Convert.ToString(loSb), null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                loSmtp.Send(loMail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private static String EmailHeader()
        {
            return @"<html><head><style type=""text/css"">body{font-family: Verdana,'sans-serif';font-size:12px;}p{text-align:justify;margin:10px 0 !important;}
                a{color:#1a4e94;text-decoration:none;font-weight:bold;}a:hover{color:#3c92fe;}table td{font-family: Verdana,'sans-serif' !important;font-size:12px;padding:3px;border-bottom:1px solid #dddddd;}
                </style></head><body>
                <div style=""width:750px; margin:5px auto;font-family: Verdana,'sans-serif';font-size:12px;line-height:20px; background-color:#f2f2f2;"">
                <img alt=""Sunrise Diamonds Ltd"" src=""https://sunrisediamonds.com.hk/Images/email-head.png"" width=""750px"" />
                <div style=""padding:10px;"">";
        }

        private static String EmailSignature()
        {
            return @"<p>Please do let us know if you have any questions. Email us on <a href=""mailto:support@sunrisediamonds.com.hk"">support@sunrisediamonds.com.hk</a></p>
                <p>Thanks and Regards,<br />Sunrise Diamond Team,<br />
                <a href=""https://sunrisediamonds.com.hk"">www.sunrisediamonds.com.hk</a></p>
                </div></div></body></html>";
        }

    }
}
