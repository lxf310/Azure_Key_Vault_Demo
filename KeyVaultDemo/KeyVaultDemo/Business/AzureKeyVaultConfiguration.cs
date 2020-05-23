using System;
using System.Configuration;

namespace KeyVaultDemo.Business
{
    public class AzureKeyVaultConfiguration : ConfigurationSection, IAzureKeyVaultConfiguration
    {
        #region Properties

        private const string SectionName = "keyVault";
        private const string AadClientIdProp = "aadClientId";
        private const string IsLocalProp = "isLocal";
        private const string CertNameProp = "certName";
        private const string UriProp = "uri";
        public static readonly Lazy<IAzureKeyVaultConfiguration> Instance;

        #endregion


        #region Constrctor

        static AzureKeyVaultConfiguration()
        {
            Instance = new Lazy<IAzureKeyVaultConfiguration>(() => ConfigurationManager.GetSection(SectionName) as IAzureKeyVaultConfiguration);
        }

        #endregion


        #region IAzureKeyVaultConfiguration

        [ConfigurationProperty(AadClientIdProp)]
        public string AadClientId
        {
            get { return (string)this[AadClientIdProp]; }
            set { this[AadClientIdProp] = value; }
        }


        [ConfigurationProperty(IsLocalProp)]
        public bool IsLocal
        {
            get { return (bool)this[IsLocalProp]; }
            set { this[IsLocalProp] = value; }
        }


        [ConfigurationProperty(CertNameProp)]
        public string CertName
        {
            get { return (string)this[CertNameProp]; }
            set { this[CertNameProp] = value; }
        }


        [ConfigurationProperty(UriProp, IsRequired = true)]
        public string Uri
        {
            get { return (string)this[UriProp]; }
            set { this[UriProp] = value; }
        }

        #endregion
    }
}