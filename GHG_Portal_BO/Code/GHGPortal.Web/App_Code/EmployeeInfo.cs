using System;
using System.Collections.Generic;
using System.Linq;
using Hess.Corporate.GHGPortal.Business;
using Hess.Corporate.GHGPortal.Configuration;

public class EmployeeInfo
{
    public string FirstLastName(string EmployeeLogin)
    {
        Employees emps = Employees.GetEmployeesBySearchString(EmployeeLogin);
        if (emps.Count > 0)
            return emps[0].FullName;
        else
            return EmployeeLogin;
    }

    public string[] GetSuperAdminListNames()
    {
        string groupName = "NYC-GHGSuperAdmin-APP";
        Employees emps = Employees.GetEmployeesByGroup(groupName);
        List<String> names = emps.Select(e => e.FullName).ToList<String>();
        return names.ToArray();
    }

    public string[] GetAdminListNames()
    {
        string groupName = "NYC-GHGAdmin-APP";
        Employees emps = Employees.GetEmployeesByGroup(groupName);
        List<String> names = emps.Select(e => e.FullName).ToList<String>();
        return names.ToArray();
    }

    public Employees GetAllUserEmails()
    {
        string groupName = "NYC-GHGAdmin-APP";
        string groupName2 = "NYC-GHGUsers-APP";
        Employees emps = Employees.GetEmployeesByGroup(groupName);
        Employees emps2 = Employees.GetEmployeesByGroup(groupName2);

        foreach (Employee e in emps2)
        {
            emps.Add(e);
        }

        return emps;
    }

    public Employees GetEnvianceEmailList()
    {
        string groupName = "NYC-GHGAdmin-APP";
        string groupName2 = "CorporateBusinessSystems";
        Employees emps = Employees.GetEmployeesByGroup(groupName);
        Employees emps2 = Employees.GetEmployeesByGroup(groupName2);

        foreach (Employee e in emps2)
	    {
            emps.Add(e);
	    }
        
        string additionalIDs= AppConfiguration.Current.AdditionalEnvianceEmail;
        
        if(additionalIDs.Length > 0)
        {
            string[] ids = additionalIDs.Split(';');
            foreach(string id in ids)
            {
                try{ emps.Add(Employees.GetEmployeeByUserID(id)); }
                catch { }
            }
        }
        return emps;
    }

    public string[] GetUserListNames()
    {
        string groupName = "NYC-GHGUsers-APP";
        Employees emps = Employees.GetEmployeesByGroup(groupName);
        List<String> names = emps.Select(e => e.FullName).ToList<String>();
        return names.ToArray();
    }

    public string[] GetAllUserListNames()
    {
        List<string> allNames = new List<string>();
        allNames.AddRange(GetUserListNames());
        allNames.AddRange(GetAdminListNames());
        allNames.AddRange(GetSuperAdminListNames());
        return allNames.ToArray();
    }
    
    public string GetUserID(string SearchString)
    {
        Employees emps  = Employees.GetEmployeesBySearchString(SearchString);
        if (emps.Count > 0)
            return emps[0].EmployeeId;
        else
            return string.Empty;
    }
}