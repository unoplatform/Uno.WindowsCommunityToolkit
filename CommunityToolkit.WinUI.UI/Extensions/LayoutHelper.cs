using System;
using Microsoft.UI.Xaml;
using Windows.Foundation;

namespace CommunityToolkit.WinUI.UI.Extensions
{
    public static class LayoutHelper
    {
        public static double SnapToPixels(this double value, double scale) => Math.Round(value * scale) / scale;

        public static Size SnapToPixels(this Size value, double scale) => new(
            SnapToPixels(value.Width, scale),
            SnapToPixels(value.Height, scale)
        );

        public static Thickness SnapToPixels(this Thickness value, double scale) => new(
            SnapToPixels(value.Left, scale),
            SnapToPixels(value.Top, scale),
            SnapToPixels(value.Right, scale),
            SnapToPixels(value.Bottom, scale)
        );
    }
}