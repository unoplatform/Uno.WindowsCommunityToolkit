using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Graph
{
    public class GlobalProvider
    {
        public static readonly string ClientName = "Windows Community Toolkit";

        public static GlobalProvider Instance => Singleton<GlobalProvider>.Instance;

        public GraphServiceClient Graph { get; set; }

        private IAuthenticationProvider _provider;
        public IAuthenticationProvider Provider {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value;
                if (Graph == null && _provider != null)
                {
                    Graph = new GraphServiceClient(_provider);
                }
            }
        }

        public IPublicClientApplication Client { get; internal set; }

        public async void Logout()
        {
            foreach (var user in await Client.GetAccountsAsync())
            {
                await Client.RemoveAsync(user);
            }
        }
    }
}
