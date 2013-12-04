using System;
using System.Web;
using System.Reflection;
using System.Configuration;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class ServiceIdentity
    {
        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;
        private const int SECURITY_IMPERSONATION = 2;
        
        [DllImport("advapi32.dll")]
        private static extern int LogonUserA(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);

        public static WindowsImpersonationContext Impersonate(string userName, string password)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;
            if (!RevertToSelf()) return null;
            if (LogonUserA(userName.Substring(userName.LastIndexOf("\\") + 1), userName.Substring(0, userName.IndexOf("\\")), password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
            {
                if (DuplicateToken(token, SECURITY_IMPERSONATION, ref tokenDuplicate) != 0)
                {
                    WindowsIdentity windowsIdentity = new WindowsIdentity(tokenDuplicate);
                    WindowsImpersonationContext impersonationContext = windowsIdentity.Impersonate();
                    if ((impersonationContext != null))
                    {
                        CloseHandle(token);
                        CloseHandle(tokenDuplicate);
                        return impersonationContext;
                    }
                }
            }
            if (token != IntPtr.Zero) CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero) CloseHandle(tokenDuplicate);
            return null;
        }
    }
}