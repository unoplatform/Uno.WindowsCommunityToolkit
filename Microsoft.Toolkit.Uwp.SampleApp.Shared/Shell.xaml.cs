// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.SampleApp.Pages;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation.Metadata;
using Microsoft.Toolkit.Uwp.UI.Controls.Graph;

#if !HAS_UNO
using Monaco;
using Monaco.Editor;
using Monaco.Helpers;
#endif

namespace Microsoft.Toolkit.Uwp.SampleApp
{
    public sealed partial class Shell
    {
        // Also need to update msal redirect in AndroidManifest.xml
        private readonly string ClientID = "a7d8cef0-4145-49b2-a91d-95c54051fa3f";

        public static Shell Current { get; private set; }

        public Shell()
        {
            var MyProvider = new InteractiveProvider()
            {
#if __ANDROID__
                RedirectUri = $"msal{ClientID}://auth"
#endif
            };
            MyProvider.ClientId = ClientID;

            InitializeComponent();
            Current = this;
        }
    }
}