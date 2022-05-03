// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security;
using System.Text;
using Microsoft.Toolkit.Uwp.UI.Automation.Peers;
using Microsoft.Toolkit.Uwp.UI.Controls.DataGridInternals;
using Microsoft.Toolkit.Uwp.UI.Controls.Primitives;
using Microsoft.Toolkit.Uwp.UI.Controls.Utilities;
using Microsoft.Toolkit.Uwp.UI.Data.Utilities;
using Microsoft.Toolkit.Uwp.UI.Utilities;
using Microsoft.Toolkit.Uwp.Utilities;
using Windows.ApplicationModel.DataTransfer;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

using DiagnosticsDebug = System.Diagnostics.Debug;

namespace Microsoft.Toolkit.Uwp.UI.Controls
{
    /// <summary>
    /// Uno-specific feature configuration
    /// </summary>
    public static class DataGridFeatureConfiguation
    {
        /// <summary>
        /// Gets or sets a value indicating whether if InvalidateMeasure invocations can be done in MeasureOverride locations.
        /// </summary>
        /// <remarks>
        /// This configuration is required until https://github.com/unoplatform/uno/issues/3519 is fixed. Without this
        /// the layout engine turns into a loop and consumes CPU excessively, or freezes the app.
        /// </remarks>
        public static bool EnableInvalidateMeasureInMeasureOverride { get; set; } = true;
    }
}
