using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultDemo.Business
{
    public interface IAzureKeyVaultConfiguration
    {
        string AadClientId { get; }
        bool IsLocal { get; }
        string CertName { get; }
        string Uri { get; }
    }
}
