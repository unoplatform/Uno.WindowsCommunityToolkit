using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Graph
{
    /// <summary>
    /// Put in app.xaml resources with ClientId
    /// 
    /// https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/Acquiring-tokens-interactively
    /// </summary>
    public class InteractiveProvider : CommonProvider
    {
        public InteractiveProvider()
        {
        }

        override protected void Initialize()
        {
            GlobalProvider.Instance.Client = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount)
                .WithRedirectUri(RedirectUri)
                .WithClientName(GlobalProvider.ClientName)
                .WithClientVersion(Assembly.GetExecutingAssembly().GetName().Version.ToString())
#if __ANDROID__
                .WithParentActivityOrWindow(() => Uno.UI.ContextHelper.Current as Android.App.Activity)
#endif
                .Build();

            GlobalProvider.Instance.Provider = new InteractiveAuthenticationProvider(GlobalProvider.Instance.Client, Scopes);
        }
    }
}
