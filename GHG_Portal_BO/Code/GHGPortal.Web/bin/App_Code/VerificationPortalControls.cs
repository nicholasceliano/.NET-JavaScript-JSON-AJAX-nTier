using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class VPC
{
    public static void SecurityCheck()
    {
        string userLevel = "userLevel",
                nameSession = "userName",
                userListSession = "UserList",
                adminListSession = "adminList",
                superAdminListSession = "superAdminList";

        if (HttpContext.Current.Session[userLevel] == null)
        {
            if (HttpContext.Current.Session[nameSession] == null || HttpContext.Current.Session[adminListSession] == null || HttpContext.Current.Session[superAdminListSession] == null)
            {
                EmployeeInfo ei = new EmployeeInfo();
                string userList = ConvertStringArrayToString(ei.GetUserListNames()),
                    adminList = ConvertStringArrayToString(ei.GetAdminListNames()),
                    superAdminList = ConvertStringArrayToString(ei.GetSuperAdminListNames()),
                    userFullName = ei.FirstLastName(HttpContext.Current.User.Identity.Name);

                HttpContext.Current.Session[superAdminListSession] = superAdminList;
                HttpContext.Current.Session[adminListSession] = adminList;
                HttpContext.Current.Session[userListSession] = userList;
                HttpContext.Current.Session[nameSession] = userFullName;
            }

            CheckAdmin(HttpContext.Current.Session[userListSession].ToString(), HttpContext.Current.Session[adminListSession].ToString(), HttpContext.Current.Session[superAdminListSession].ToString(), HttpContext.Current.Session[nameSession].ToString());
        }
    }

    private static string ConvertStringArrayToString(string[] array)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        foreach (string value in array)
            builder.Append(value + ",");

        return builder.ToString();
    }

    private static void CheckAdmin(string userList, string adminList, string superAdminList, string userName)
    {
        string userLevel = "userLevel",
            user = "U",
            admin = "A",
            superAdmin = "SA";

        if (adminList.Contains(userName) || superAdminList.Contains(userName))
        {
            HttpContext.Current.Session[userLevel] = admin;
            if (superAdminList.Contains(userName))
                HttpContext.Current.Session[userLevel] = superAdmin;
        }
        else if (userList.Contains(userName))
            HttpContext.Current.Session[userLevel] = user;
        else
            HttpContext.Current.Server.Transfer("~/AccessDenied.htm");
    }

    #region DDL Databinds

    public static void DDLetc(IOrderedEnumerable<string> dataSource, DropDownList ddl)
    {
        ddl.DataSource = dataSource;
        ddl.DataBind();
    }

    public static void DDLDatabound(DropDownList current, DropDownList ddlSource = null)
    {
        if (current.Items.Count > 0)
        {
            if (current.Items[0].Text != string.Empty)
                current.Items.Insert(0, new ListItem(string.Empty, String.Empty));
        }

        if (ddlSource != null)
        {
            if (current.ID == ddlSource.ID)
            {
                for (int i = 0; i < current.Items.Count; i++)
                {
                    if (current.Items[i].Value.Contains("MANUAL PORTAL"))
                        current.Items[i].Text = "MANUAL";
                }
            }
        }
    }

    #endregion

    #region Admin Pages

    public static void Secuirty(Label lblPageTitle, Panel pnlSearchCriteria, Panel pnlInfoContainer)
    {
        string userLevel = (string)HttpContext.Current.Session["userLevel"];

        if (userLevel == "U")
        {
            lblPageTitle.Text = "Access denied.";
            pnlSearchCriteria.Visible = false;
            pnlInfoContainer.Visible = false;
            return;
        }
    }

    #endregion

    #region Controls
    
    public static string AddCommas<T>(T value)
    {
        string val, dec;

        if (value.ToString().Contains('.'))
        {
            val = value.ToString().Split('.')[0];
            dec = value.ToString().Split('.')[1];
        }
        else
        {
            val = value.ToString();
            dec = null;
        }
        
        if (val.Length > 3)
            val = val.Insert(val.Length - 3, ",");
        if (val.Length > 7)
            val = val.Insert(val.Length - 7, ",");
        if (val.Length > 11)
            val = val.Insert(val.Length - 11, ",");
        if (val.Length > 15)
            val = val.Insert(val.Length - 15, ",");

        if (dec != "000" && dec != null)
            val = val + "." + dec.Substring(0, 2);

        return val;
    }

    public static string AddEmailHyperlink(string text, string email)
    {
        string eMailName = email;
        int nameLength = eMailName.Length,
            nameStart = text.IndexOf(eMailName);

        text = text.Insert(nameStart + nameLength, "</a>");
        text = text.Insert(nameStart, "<a href='mailto:" + email + "'>");

        return text;
    }

    public static string wrapLabel(string text, int lenBreak)
    {
        int count = text.Length,
            breaks = count / lenBreak,
            nextSpace = 0;

        for (int i = 1; i < breaks + 1; i++)
        {
            if (i == 1)
                nextSpace = text.IndexOf(" ", lenBreak * i);
            else if (i > 1)
                nextSpace = text.IndexOf(" ", text.IndexOf("<br />", lenBreak * (i - 1)) + lenBreak);

            if (nextSpace != -1)
                text = text.Insert(nextSpace, "<br />");
        }
        return text;
    }

    public static void PanelStlying(Panel pnl)
    {
        pnl.CssClass = "popupPanel";
    }

    #endregion
}