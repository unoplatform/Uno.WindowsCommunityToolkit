using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.Toolkit.Graph;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Graph
{
    /// <summary>
    /// Put in app.xaml resources with ClientId
    /// 
    /// https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/Integrated-Windows-Authentication
    /// </summary>
    public class IntegratedProvider : CommonProvider
    {
        public string TenantId
        {
            get { return (string)GetValue(TenantIdProperty); }
            set { SetValue(TenantIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TenantId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TenantIdProperty =
            DependencyProperty.Register(nameof(TenantId), typeof(string), typeof(IntegratedProvider), new PropertyMetadata("https://login.microsoftonline.com/organizations/"));

        public IntegratedProvider()
        {
        }

        override protected void Initialize()
        {
            // TODO: Deal with TenantId not being initialized yet if comes after ClientId in XAML...
            // TODO: Figure out way to test this scenario.
            GlobalProvider.Instance.Client = PublicClientApplicationBuilder.Create(ClientId)
                .WithTenantId(TenantId)
                .Build();

            GlobalProvider.Instance.Provider = new IntegratedWindowsAuthenticationProvider(GlobalProvider.Instance.Client, Scopes);
        }
    }
}
