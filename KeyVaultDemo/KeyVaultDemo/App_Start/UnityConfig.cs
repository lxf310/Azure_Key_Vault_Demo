using KeyVaultDemo.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace KeyVaultDemo.App_Start
{
    public class UnityConfig
    {
        #region Unity Container

        public static IUnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        #endregion


        #region Methods

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IAzureKeyVaultClient, AzureKeyVaultClient>();
            //container.RegisterType<IAzureKeyVaultConfiguration, AzureKeyVaultConfiguration>();
            container.RegisterInstance(AzureKeyVaultConfiguration.Instance.Value);
        }

        #endregion
    }
}