using System;
using System.DirectoryServices.ActiveDirectory;
using System.Net.NetworkInformation;

namespace PolViewer
{
    internal static class DomainHelper
    {
        internal static bool TryGetDomainName(out string domainName) => TryGetDomainFromEnvVars(out domainName) || TryGetDomainFromAD(out domainName) || TryGetDomainFromDns(out domainName);

        private static bool TryGetDomainFromEnvVars(out string domainName)
        {
            try
            {
                domainName = Environment.UserDomainName;
                return true;
            }
            catch (Exception)
            {
                domainName = string.Empty;
                return false;
            }
        }

        private static bool TryGetDomainFromAD(out string domainName)
        {
            try
            {
                domainName = Domain.GetComputerDomain().Name;
                return true;
            }
            catch (Exception)
            {
                domainName = string.Empty;
                return false;
            }
        }

        private static bool TryGetDomainFromDns(out string domainName)
        {
            try
            {
                domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
                return true;
            }
            catch (Exception)
            {
                domainName = string.Empty;
                return false;
            }
        }
    }
}