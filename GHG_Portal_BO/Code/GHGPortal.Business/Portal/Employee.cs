using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hess.ActiveDirectory;

namespace Hess.Corporate.GHGPortal.Business
{
    public class Employee: BusinessObject<Employee>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Title { get; set; }

        public string EmployeeId { get; set; }

        public string FullName { get { return (FirstName == null || LastName == null || FirstName == LastName) ? DisplayName : FirstName + " " + LastName; } }

        protected override object GetIdValue()
        {
            return UserName;
        }

        public void Fill(ADUsersItem item)
        {
            this.FirstName = item.FirstName;
            this.LastName = item.LastName;
            this.Email = item.Email;
            this.DisplayName = item.DisplayName;
            this.Title = item.Title;
            this.EmployeeId = item.UserId;
        }

    }
}