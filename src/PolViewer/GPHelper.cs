using Microsoft.GroupPolicy;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PolViewer
{
    internal static class GPHelper
    {
        internal static ReadOnlyCollection<GPInfo> PopulateGPInfo()
        {
            if (DomainHelper.TryGetDomainName(out var domainName))
            {
                return new GPDomain(domainName, DCUsage.UseAnyDC)
                    .GetAllGpos()
                    .Select(gpo => new GPInfo() { Name = gpo.DisplayName, Guid = gpo.Id.ToString() })
                    .ToList()
                    .AsReadOnly();
            }
            else
            {
                throw new Exception("No domain found");
            }
        }
    }
}