// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using CommunityToolkit.WinUI.UI.Controls.DataGridInternals;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace CommunityToolkit.WinUI.UI.Controls
{
    /// <summary>
    /// Mouse wheel scrolling that moves like Uno Platform's ScrollViewer: 15% of the viewport per notch, animated, with rapid notches
    /// adding up to one target. The offsets go through the grid's own scroll processing, so rows are realized as they come into view.
    /// </summary>
    public partial class DataGrid
    {
        // WHEEL_DELTA, the delta of one notch of a standard mouse wheel.
        private const double DATAGRID_mouseWheelNotchDelta = 120;

        // The ScrollViewer's step: 15% of the viewport per notch, capped at the viewport.
        private const double DATAGRID_mouseWheelViewportShare = 0.15;

        private DataGridWheelMotion _wheelMotionHorizontal;
        private DataGridWheelMotion _wheelMotionVertical;
        private bool _isWheelMotionRunning;

        /// <summary>
        /// Scrolls for a mouse wheel event.
        /// </summary>
        /// <param name="mouseWheelDelta">The event's wheel delta: positive is up, or right for a horizontal wheel.</param>
        /// <param name="isForHorizontalScroll">Whether the event scrolls sideways.</param>
        /// <param name="isHorizontalMouseWheel">Whether the event comes from a horizontal wheel.</param>
        /// <returns>False when the grid has no room left in that direction, so the event can scroll a parent instead.</returns>
        internal bool ScrollForMouseWheel(double mouseWheelDelta, bool isForHorizontalScroll, bool isHorizontalMouseWheel)
        {
            if (!this.IsEnabled || this.DisplayData.NumDisplayedScrollingElements <= 0)
            {
                return false;
            }

            double viewport = isForHorizontalScroll ? this.CellsWidth : this.CellsHeight;

            if (double.IsInfinity(viewport) || viewport <= 0)
            {
                return false;
            }

            // A horizontal wheel's delta is positive to the right; a vertical one's is positive up, which scrolls up, or left with Shift.
            double distance = isHorizontalMouseWheel ? mouseWheelDelta : -mouseWheelDelta;
            distance = Math.Clamp(distance * viewport * DATAGRID_mouseWheelViewportShare / DATAGRID_mouseWheelNotchDelta, -viewport, viewport);

            if (distance == 0)
            {
                return true;
            }

            // Against where the motion in flight will come to rest: a motion already headed for the end has no room left.
            double from = _isWheelMotionRunning
                ? (isForHorizontalScroll ? _wheelMotionHorizontal.ProjectedEnd : _wheelMotionVertical.ProjectedEnd)
                : (isForHorizontalScroll ? this.HorizontalOffset : _verticalOffset);

            double maximum = isForHorizontalScroll ? GetHorizontalOffsetMaximum() : GetVerticalOffsetMaximum();

            if (distance < 0 ? from <= 0 : from >= maximum)
            {
                return false;
            }

            if (!_isWheelMotionRunning)
            {
                _wheelMotionHorizontal.Start(this.HorizontalOffset);
                _wheelMotionVertical.Start(_verticalOffset);
                _isWheelMotionRunning = true;

                CompositionTarget.Rendering += WheelMotion_Rendering;

                // A press takes over from the wheel: touch, the scroll bars, a click on a row.
                this.AddHandler(PointerPressedEvent, new PointerEventHandler(WheelMotion_PointerPressed), true /*handledEventsToo*/);
                this.Unloaded += WheelMotion_Unloaded;
            }

            if (isForHorizontalScroll)
            {
                _wheelMotionHorizontal.AddDistance(distance);
            }
            else
            {
                _wheelMotionVertical.AddDistance(distance);
            }

            return true;
        }

        internal void StopWheelMotion()
        {
            if (!_isWheelMotionRunning)
            {
                return;
            }

            _isWheelMotionRunning = false;
            _wheelMotionHorizontal.Stop();
            _wheelMotionVertical.Stop();

            CompositionTarget.Rendering -= WheelMotion_Rendering;
            this.RemoveHandler(PointerPressedEvent, new PointerEventHandler(WheelMotion_PointerPressed));
            this.Unloaded -= WheelMotion_Unloaded;
        }

        // The same maximum that ProcessScrollOffsetDelta scrolls to.
        private double GetVerticalOffsetMaximum()
        {
            if (_vScrollBar != null && this.VerticalScrollBarVisibility == ScrollBarVisibility.Visible)
            {
                return Math.Max(0, _vScrollBar.Maximum);
            }

            return Math.Max(0, this.EdgedRowsHeightCalculated - this.CellsHeight);
        }

        private double GetHorizontalOffsetMaximum() => Math.Max(0, this.ColumnsInternal.VisibleEdgedColumnsWidth - this.CellsWidth);

        private void WheelMotion_Rendering(object sender, object e)
        {
            // The frame raise captures its handlers, so this one can still run in the frame that stopped the motion.
            if (!_isWheelMotionRunning || e is not RenderingEventArgs args)
            {
                return;
            }

            long timestamp = args.RenderingTime.Ticks;
            bool runningHorizontal = _wheelMotionHorizontal.Tick(timestamp, 0, GetHorizontalOffsetMaximum());
            bool runningVertical = _wheelMotionVertical.Tick(timestamp, 0, GetVerticalOffsetMaximum());

            // Relative to the offsets the grid has applied, so a frame that runs before the grid has measured the last one still lands
            // where the motion is, rather than adding to a scroll that is pending.
            ProcessScrollOffsetDelta(_wheelMotionHorizontal.Position - this.HorizontalOffset, true /*isForHorizontalScroll*/);
            ProcessScrollOffsetDelta(_wheelMotionVertical.Position - _verticalOffset, false /*isForHorizontalScroll*/);

            if (!runningHorizontal && !runningVertical)
            {
                StopWheelMotion();
            }
        }

        private void WheelMotion_PointerPressed(object sender, PointerRoutedEventArgs e) => StopWheelMotion();

        private void WheelMotion_Unloaded(object sender, RoutedEventArgs e) => StopWheelMotion();
    }
}
