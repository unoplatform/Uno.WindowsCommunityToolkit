using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Graph
{
    /// <summary>
    /// Provides a common base class for XAML based provider wrappers to the Microsoft.Graph.Auth SDK.
    /// </summary>
    public abstract partial class CommonProvider: DependencyObject
    {
        public string ClientId
        {
            get { return (string)GetValue(ClientIdProperty); }
            set { SetValue(ClientIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClientId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientIdProperty =
            DependencyProperty.Register(nameof(ClientId), typeof(string), typeof(InteractiveProvider), new PropertyMetadata(string.Empty, ClientIdPropertyChanged));

        private static void ClientIdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CommonProvider provider)
            {
                provider.Initialize();
            }
        }

        public string RedirectUri
        {
            get { return (string)GetValue(RedirectUriProperty); }
            set { SetValue(RedirectUriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RedirectUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RedirectUriProperty =
            DependencyProperty.Register(nameof(RedirectUri), typeof(string), typeof(CommonProvider), new PropertyMetadata("https://login.microsoftonline.com/common/oauth2/nativeclient"));

        // TODO: Create Metadata converter for XAML?
        public string[] Scopes { get; set; } = new string[] { "User.ReadBasic.All" };

        protected abstract void Initialize();
    }
}
