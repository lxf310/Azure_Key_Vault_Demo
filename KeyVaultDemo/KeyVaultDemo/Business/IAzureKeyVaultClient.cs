using System.Threading.Tasks;

namespace KeyVaultDemo.Business
{
    public interface IAzureKeyVaultClient
    {
        Task<string> SetSecretAsyn(string name, string secret);

        Task<string> GetSecretAsync(string name);

        Task<string> DeleteSecretAsyn(string name);
    }
}
