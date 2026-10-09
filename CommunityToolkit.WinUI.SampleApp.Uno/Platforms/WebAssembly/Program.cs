// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.UI.Xaml;

namespace CommunityToolkit.WinUI.SampleApp
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            App.InitializeLogging();

            Application.Start(_ => new App());

            return 0;
        }
    }
}
