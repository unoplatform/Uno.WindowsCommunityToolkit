// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CommunityToolkit.WinUI.UI.Controls
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
        /// Defaults to <c>true</c>, as the upstream DataGrid always invalidates. It was <c>false</c> on iOS to work around
        /// https://github.com/unoplatform/uno/issues/3519, which no longer applies to Uno Platform 7, and is only kept as an opt-out.
        /// </remarks>
        public static bool EnableInvalidateMeasureInMeasureOverride { get; set; } = true;
    }
}