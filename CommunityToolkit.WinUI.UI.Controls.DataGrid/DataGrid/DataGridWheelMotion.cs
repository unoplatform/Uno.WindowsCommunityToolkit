// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace CommunityToolkit.WinUI.UI.Controls.DataGridInternals
{
    /// <summary>
    /// The mouse wheel's scroll motion along one axis, as Uno Platform's ScrollViewer plays it (its ScrollWheelSimulation, measured on
    /// WinUI 3's ScrollViewer): a 220ms quadratic ease-out whose first frame covers 22% of the distance. Every notch moves the target by
    /// the distance it carries and restarts the curve from the current position.
    /// </summary>
    internal struct DataGridWheelMotion
    {
        // Duration of the curve, in seconds.
        private const double Duration = 0.22;

        // Share of the distance the curve covers on its first frame.
        private const double FirstFrameShare = 0.22;

        private bool _isRunning;
        private double _position;
        private double _from;
        private double _target;

        // The curve restarts from the first frame after a notch.
        private bool _hasStartTimestamp;
        private long _startTimestampInTicks;

        public double Position => _position;

        // Where the motion in flight will come to rest, ignoring bounds.
        public double ProjectedEnd => _target;

        public void Start(double position)
        {
            _position = position;
            _target = position;
            _isRunning = false;
        }

        public void AddDistance(double distance)
        {
            if (distance == 0)
            {
                return;
            }

            _isRunning = true;
            _target += distance;
            _from = _position;
            _hasStartTimestamp = false;
        }

        // Advances to the timestamp, clamped to [min, max]. Returns false once the motion has settled.
        public bool Tick(long timestampInTicks, double min, double max)
        {
            if (!_isRunning)
            {
                return false;
            }

            if (!_hasStartTimestamp)
            {
                _hasStartTimestamp = true;
                _startTimestampInTicks = timestampInTicks;
            }

            double s = Math.Clamp((timestampInTicks - _startTimestampInTicks) / (double)TimeSpan.TicksPerSecond / Duration, 0, 1);
            double progress = FirstFrameShare + ((1 - FirstFrameShare) * (1 - ((1 - s) * (1 - s))));
            _position = _from + ((_target - _from) * progress);

            if (s >= 1)
            {
                _position = _target;
                _isRunning = false;
            }

            if (_position <= min || _position >= max)
            {
                _position = _target = Math.Clamp(_position, min, max);
                _isRunning = false;
            }

            return _isRunning;
        }

        public void Stop()
        {
            _isRunning = false;
            _target = _position;
        }
    }
}
