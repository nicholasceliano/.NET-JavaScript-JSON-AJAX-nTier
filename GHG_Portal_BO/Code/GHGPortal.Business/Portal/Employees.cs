using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hess.ActiveDirectory;
namespace Hess.Corporate.GHGPortal.Business
{
    public class Employees:BusinessObjects<Employees, Employee>
    {
        private static string removePrefix(string EmployeeLogin)
        {
            if (EmployeeLogin.Contains("IHESS\\") || EmployeeLogin.Contains("ihess\\"))
            {
                EmployeeLogin = EmployeeLogin.Remove(0, 6).ToString();
            }
            return EmployeeLogin;
        }
        public static Employees GetEmployeesByUserId(string userId)
        {
            Employees emps = new Employees();

            ADUsers users = new ADUsers();
            ADUsersItem item = users.GetUserByUserId(removePrefix(userId));
            Employee emp = new Employee();
            if (item != null)
            {
                emp.Fill(item);
                emps.Add(emp);
            }
            return emps;
        }

        public static Employee GetEmployeeByUserID(string userID)
        {

            Employee emp = new Employee();
            ADUsers users = new ADUsers();
            ADUsersItem item = users.GetUserByUserId(removePrefix(userID));
            emp.Fill(item);

            return emp;
        }

        //always returns collection with 1 item
        public static Employees GetEmployeesBySearchString(string userId)
        {
            Employees emps = new Employees();

            ADUsers users = new ADUsers();
            ADUsersItem item = users.GetUserWithSearchString(removePrefix(userId));
            Employee emp = new Employee();
            if (item != null)
            {
                emp.Fill(item);
                emps.Add(emp);
            }
            return emps;
        }

        public static Employees GetEmployeesByUserId(string[] userIds)
        {
            Employees emps = new Employees();
            foreach (string userId in userIds)
            {
                ADUsers users = new ADUsers();
                ADUsersItem item = users.GetUserByUserId(removePrefix(userId));
                Employee emp = new Employee();
                emp.Fill(item);
                emps.Add(emp);
            }

            return emps;
        }

        public static Employees GetEmployeesByGroup(string groupName)
        {
            Employees emps = new Employees();

            ADUsers users = new ADUsers();
            users = users.GetUsersByGroup(groupName);
            foreach (ADUsersItem user in users)
            {
                Employee emp = new Employee();
                emp.Fill(user);
                emps.Add(emp);
            }
            return emps;
        }
    }
}