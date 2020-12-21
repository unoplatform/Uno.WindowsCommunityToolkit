// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.Toolkit.Uwp.UI.Animations.Extensions;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Microsoft.Toolkit.Uwp.UI.Animations
{
    /// <inheritdoc cref="TimedKeyFrameAnimationBuilder{T}"/>
    internal abstract partial class TimedKeyFrameAnimationBuilder<T>
        where T : unmanaged
    {
        /// <summary>
        /// Gets a <see cref="Timeline"/> instance representing the animation to start.
        /// </summary>
        /// <typeparam name="TKeyFrame">The type of keyframes being used to define the animation.</typeparam>
        /// <param name="target">The target <see cref="DependencyObject"/> instance to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="delay">The optional initial delay for the animation.</param>
        /// <param name="duration">The animation duration.</param>
        /// <param name="keyFrames">The list of keyframes to use to build the animation.</param>
        /// <returns>A <see cref="Timeline"/> instance with the specified animation.</returns>
        public static Timeline GetAnimation<TKeyFrame>(
            DependencyObject target,
            string property,
            TimeSpan? delay,
            TimeSpan duration,
            List<TKeyFrame> keyFrames)
            where TKeyFrame : struct, IKeyFrameInfo
        {
            Timeline animation;

            if (typeof(T) == typeof(float))
            {
                DoubleAnimationUsingKeyFrames doubleAnimation = new() { EnableDependentAnimation = true };

                foreach (var keyFrame in keyFrames)
                {
                    doubleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        KeyTime = keyFrame.GetTimedProgress(duration),
                        Value = keyFrame.GetValueAs<float>(),
                        EasingFunction = keyFrame.EasingType.ToEasingFunction(keyFrame.EasingMode)
                    });
                }

                animation = doubleAnimation;
            }
            else if (typeof(T) == typeof(double))
            {
                DoubleAnimationUsingKeyFrames doubleAnimation = new() { EnableDependentAnimation = true };

                foreach (var keyFrame in keyFrames)
                {
                    doubleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame()
                    {
                        KeyTime = keyFrame.GetTimedProgress(duration),
                        Value = keyFrame.GetValueAs<double>(),
                        EasingFunction = keyFrame.EasingType.ToEasingFunction(keyFrame.EasingMode)
                    });
                }

                animation = doubleAnimation;
            }
            else if (typeof(T) == typeof(Point))
            {
                PointAnimationUsingKeyFrames pointAnimation = new() { EnableDependentAnimation = true };

                foreach (var keyFrame in keyFrames)
                {
                    pointAnimation.KeyFrames.Add(new EasingPointKeyFrame()
                    {
                        KeyTime = keyFrame.GetTimedProgress(duration),
                        Value = keyFrame.GetValueAs<Point>(),
                        EasingFunction = keyFrame.EasingType.ToEasingFunction(keyFrame.EasingMode)
                    });
                }

                animation = pointAnimation;
            }
            else if (typeof(T) == typeof(Color))
            {
                ColorAnimationUsingKeyFrames colorAnimation = new() { EnableDependentAnimation = true };

                foreach (var keyFrame in keyFrames)
                {
                    colorAnimation.KeyFrames.Add(new EasingColorKeyFrame()
                    {
                        KeyTime = keyFrame.GetTimedProgress(duration),
                        Value = keyFrame.GetValueAs<Color>(),
                        EasingFunction = keyFrame.EasingType.ToEasingFunction(keyFrame.EasingMode)
                    });
                }

                animation = colorAnimation;
            }
            else
            {
                return ThrowHelper.ThrowInvalidOperationException<Timeline>("Invalid animation type");
            }

            animation.BeginTime = delay;

            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, property);

            return animation;
        }

        /// <summary>
        /// A custom <see cref="TimedKeyFrameAnimationBuilder{T}"/> class targeting the XAML layer.
        /// </summary>
        public sealed class Xaml : TimedKeyFrameAnimationBuilder<T>, AnimationBuilder.IXamlAnimationFactory
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TimedKeyFrameAnimationBuilder{T}.Xaml"/> class.
            /// </summary>
            /// <inheritdoc cref="TimedKeyFrameAnimationBuilder{T}"/>
            public Xaml(string property, TimeSpan? delay)
                : base(property, delay)
            {
            }

            /// <inheritdoc/>
            public Timeline GetAnimation(DependencyObject targetHint)
            {
                return GetAnimation(
                    targetHint,
                    this.property,
                    this.delay,
                    default,
                    this.keyFrames);
            }
        }
    }
}
