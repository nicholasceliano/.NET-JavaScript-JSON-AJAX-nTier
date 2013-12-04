using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Hess.Corporate.GHGPortal.Business;
using Hess.Corporate.GHGPortal.Configuration;
using Hess.Corporate.GHGPortal.Services;
using System.Threading;
using System.Text;
using System.Globalization;
using Hess.Corporate.GHGPortal.Email;
using System.IO;
using DotLiquid;
using System.Web.Script.Services;

namespace GHGPortal.Web
{
    [WebService(Namespace = "http://ghportal.ihess.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class EnvianceServices : System.Web.Services.WebService
    {
        private AppConfiguration config;
        private string START_MESSAGE = "Enviance Submission Process Starting";
        private string END_MESSAGE = "Enviance Submission Finished";
        public EnvianceServices()
        {
            config = AppConfiguration.Current;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet=true)]
        public bool SubmitToEnviance()
        {
            SendEmail(START_MESSAGE, START_MESSAGE, config.SystemAdmin, config.SystemAdmin, false);
            EnvianceInterfaceRecords records = EnvianceInterfaceRecords.GetRecords();
            bool finishedSuccessfully = true;
            try
            {
                if (records.Count > 0)
                    records = new EnvianceAccess().SubmitRecords(records);

                foreach (EnvianceInterfaceRecord record in records)
                    record.Save();

                List<EnvianceInterfaceRecord> errorRecords = records.Where(r => r.ErrorCode != 0).ToList<EnvianceInterfaceRecord>();
                if (errorRecords.Count > 0)
                    EmailAdmins(errorRecords);       
            }
            catch (Exception e)
            {
                EmailTechnicalError(e.Message);
                finishedSuccessfully = false;
            }
            SendEmail(END_MESSAGE, END_MESSAGE, config.SystemAdmin, config.SystemAdmin, false);
            return finishedSuccessfully;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void SendUnapprovedEmails()
        {
            MonthlyValidationReminderRecords records = MonthlyValidationReminderRecords.GetRecords();
            try
            {
                if (records.Count > 0)
                    EmailEveryone(records);
            }
            catch (Exception e)
            {
                EmailTechnicalError(e.Message);
            }
        }

        private void EmailEveryone(MonthlyValidationReminderRecords records)
        {
            EmployeeInfo e = new EmployeeInfo();
            Employees userList = e.GetAllUserEmails();

            foreach (Employee user in userList)
            {
                string email = user.Email;

                StringBuilder emailBuilder = new StringBuilder();
                string templatePath = HttpContext.Current.Server.MapPath(AppConfiguration.Current.MonthlyValidationReminder);
                string templateText = File.ReadAllText(templatePath);
                DotLiquid.Template.RegisterSafeType(typeof(MonthlyValidationReminderRecord), new string[] { "FacilityName", "EmmittingAssetName", "UnitOfMeasure", "ValidationPeriod", "EmissionVolume", "FormattedVolume" });
                Template template = Template.Parse(templateText);

                string date = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");

                string html = template.Render(Hash.FromAnonymousObject(new { Date = date, SystemAdmin = config.SystemAdmin, Kickouts = records }));

                if (email != null)
                    SendEmail(html, config.MonthyValidationReminderSubject, config.SystemAdmin, email);
                else
                    EmailTechnicalError("Could not send kickout report to: " + user);
            }
        }

        private void EmailAdmins(List<EnvianceInterfaceRecord> records)
        {
            EmployeeInfo e = new EmployeeInfo();
            Employees adminList = e.GetEnvianceEmailList();
            
            foreach (Employee admin in adminList)
            {
                string email = admin.Email;

                StringBuilder emailBuilder = new StringBuilder();
                string templatePath = HttpContext.Current.Server.MapPath(AppConfiguration.Current.EnvianceKickoutEmailTemplateFile);
                string templateText = File.ReadAllText(templatePath);
                DotLiquid.Template.RegisterSafeType(typeof(EnvianceInterfaceRecord), new string[] { "FacilityName", "EmmittingAssetName", "ProductName", "UnitOfMeasure", "Status", "ValidationPeriod", "EmissionVolume", "FormattedVolume" });
                Template template = Template.Parse(templateText);

                string html = template.Render(Hash.FromAnonymousObject(new { SystemAdmin = config.SystemAdmin, Kickouts = records }));

                if (email != null)
                    SendEmail(html, config.EnvianceKickoutReportSubject, config.SystemAdmin, email);
                else
                    EmailTechnicalError("Could not send kickout report to: " + admin);
            }
        }

        private void EmailOwners(IEnumerable<EnvianceInterfaceRecord> records)
        {
            Dictionary<string, HashSet<EnvianceInterfaceRecord>> uniqueOwners = new Dictionary<string, HashSet<EnvianceInterfaceRecord>>();
            foreach (EnvianceInterfaceRecord record in records)
            {
                FacilityDataOwners owners = FacilityDataOwners.GetOwners(record.FacilityName.Trim());
                if (owners == null || owners.Count == 0)
                    EmailTechnicalError(String.Format("No owners found for Facility: {0} and Asset Name: {1}. This combo has kickouts.", record.FacilityName, record.EmmittingAssetName));
                foreach (FacilityDataOwner owner in owners)
                {
                    HashSet<EnvianceInterfaceRecord> recordSet = null;
                    if (uniqueOwners.ContainsKey(owner.OwnerSSO))
                        recordSet = uniqueOwners[owner.OwnerSSO];
                    else
                    {
                        recordSet = new HashSet<EnvianceInterfaceRecord>();
                        uniqueOwners.Add(owner.OwnerSSO, recordSet);
                    }
                    recordSet.Add(record);
                }
            }

            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = culture.TextInfo;

            foreach (KeyValuePair<string, HashSet<EnvianceInterfaceRecord>> owner in uniqueOwners)
            {
                StringBuilder emailBuilder = new StringBuilder();
                string templatePath = HttpContext.Current.Server.MapPath(AppConfiguration.Current.EnvianceKickoutEmailTemplateFile);
                string templateText = File.ReadAllText(templatePath);
                DotLiquid.Template.RegisterSafeType(typeof(EnvianceInterfaceRecord), new string[] { "FacilityName", "EmmittingAssetName", "ProductName", "UnitOfMeasure", "Status", "ValidationPeriod", "EmissionVolume", "FormattedVolume"  });
                Template template = Template.Parse(templateText);

                string html = template.Render(Hash.FromAnonymousObject(new { SystemAdmin = config.SystemAdmin, Kickouts = owner.Value.ToArray() }));

                Employees emps = Employees.GetEmployeesBySearchString(owner.Key.Trim());
                if (emps != null && emps.Count == 1)
                    SendEmail(html, config.EnvianceKickoutReportSubject, config.SystemAdmin, emps.First().Email);
                else
                    EmailTechnicalError("Could not send kickout report to: " + owner.Key);
            }
        }

        private void EmailTechnicalError(string message)
        {
            SendEmail(message, config.EnvianceTechnicalErrorSubject, config.SystemAdmin, config.SystemAdmin);
        }

        private void SendEmail(string message, string subject, string from, string to, bool isHTML = true)
        {
            Email email = new Email(subject);
            email.ToEmail = to;
            email.FromEmail = from;
            email.Body = message;
            email.IsBodyHTML = isHTML;
            EmailSender sender = new EmailSender(config.SMTPServer);
            sender.SendEmailViaSMTP(email);
        }
    }
}
