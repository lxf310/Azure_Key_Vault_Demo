using log4net;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace KeyVaultDemo.Business
{
    public class AzureKeyVaultClient : IAzureKeyVaultClient
    {
        #region Properties

        private readonly KeyVaultClient _keyVaultClient;
        private readonly string _kvUri;
        private static readonly ILog Log = LogManager.GetLogger(typeof(AzureKeyVaultClient));

        #endregion


        #region Constrctor

        public AzureKeyVaultClient(IAzureKeyVaultConfiguration config)
        {
            _kvUri = config.Uri;
            if (config.IsLocal)
            {
                _keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
                {
                    var authenticationContext = new AuthenticationContext(authority, null);
                    X509Certificate2 cert = getCertificateBySubject(config.CertName);
                    var clientAssertionCertificate = new ClientAssertionCertificate(config.AadClientId, cert);
                    var result = await authenticationContext.AcquireTokenAsync(resource, clientAssertionCertificate);
                    var token = result.AccessToken;
                    return token;
                });
            }
            else
            {
                // Suppose the service is hosted in Azure cloud
                AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                _keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            }
        }

        #endregion


        #region Methods

        private X509Certificate2 getCertificateBySubject(string subject)
        {
            if (string.IsNullOrEmpty(subject)) return null;

            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);

                var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, subject, false);

                if (certificates.Count != 1)
                {
                    throw new Exception($"Failed to get certificate for {subject}.");
                }

                return certificates[0];
            }
        }

        #endregion


        #region IAzureKeyVaultClient

        public async Task<string> SetSecretAsyn(string name, string secret)
        {
            try
            {
                var response = await _keyVaultClient.SetSecretAsync(_kvUri, name, secret);
                return response.Id;
            }
            catch (KeyVaultErrorException e)
            {
                Log.Error("Failed to add a new secret.", e);
                throw;
            }
        }


        public async Task<string> GetSecretAsync(string name)
        {
            try
            {
                var response = await _keyVaultClient.GetSecretAsync($"{_kvUri}secrets/{name}").ConfigureAwait(false);
                return response.Value;
            }
            catch (KeyVaultErrorException e)
            {
                Log.Warn($"Failed to retrieve the secret of {name}.", e);
                throw;
            }
        }


        public async Task<string> DeleteSecretAsyn(string name)
        {
            try
            {
                var response = await _keyVaultClient.DeleteSecretAsync(_kvUri, name);
                return response.Id;
            }
            catch (KeyVaultErrorException e)
            {
                Log.Error("Failed to delete the secret.", e);
                throw;
            }
        }

        #endregion
    }
}