// <copyright file="Progress.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    /// <summary>
    /// The <see cref="Progress" /> class.
    /// </summary>
    [TemplatePart(Name = TrackPartName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = IndicatorPartName, Type = typeof(FrameworkElement))]

    // ReSharper disable once InheritdocConsiderUsage
    public class Progress : Control
    {
        /// <summary>
        /// Identifies the <see cref="IsErrored" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsErroredProperty = DependencyProperty.Register("IsErrored", typeof(bool), typeof(Progress), new UIPropertyMetadata(false, OnIsErroredChanged));

        /// <summary>
        /// Identifies the <see cref="IsInProgress" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsInProgressProperty = DependencyProperty.Register("IsInProgress", typeof(bool), typeof(Progress), new UIPropertyMetadata(false, OnIsInProgressChanged));

        private const string TrackPartName = "PART_Track";
        private const string IndicatorPartName = "PART_Indicator";

        private FrameworkElement _track;
        private FrameworkElement _indicator;

        /// <summary>
        /// Initializes static members of the <see cref="Progress"/> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        static Progress()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Progress), new FrameworkPropertyMetadata(typeof(Progress)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Progress" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public Progress()
        {
            IsVisibleChanged += (sender, e) => { UpdateAnimation(); };
            IsErroredChanged += (sender, e) => { UpdateAnimation(); };
            IsInProgressChanged += (sender, e) => { UpdateAnimation(); };
        }

        /// <summary>
        /// Occurs when <see cref="IsErrored "/> changed.
        /// </summary>
        public event DependencyPropertyChangedEventHandler IsErroredChanged;

        /// <summary>
        /// Occurs when <see cref="IsInProgress" /> changed.
        /// </summary>
        public event DependencyPropertyChangedEventHandler IsInProgressChanged;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrored
        {
            get
            {
                return (bool)GetValue(IsErroredProperty);
            }

            set
            {
                SetValue(IsErroredProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in progress.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in progress; otherwise, <c>false</c>.
        /// </value>
        public bool IsInProgress
        {
            get
            {
                return (bool)GetValue(IsInProgressProperty);
            }

            set
            {
                SetValue(IsInProgressProperty, value);
            }
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_track != null)
            {
                _track.SizeChanged -= TrackOnSizeChanged;
            }

            _track = GetTemplateChild(TrackPartName) as FrameworkElement;
            _indicator = GetTemplateChild(IndicatorPartName) as FrameworkElement;

            if (_track != null)
            {
                _track.SizeChanged += TrackOnSizeChanged;
            }
        }

        private static void OnIsErroredChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            (dependencyObject as Progress)?.IsErroredChanged?.Invoke(dependencyObject, dependencyPropertyChangedEventArgs);
        }

        private static void OnIsInProgressChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            (dependencyObject as Progress)?.IsInProgressChanged?.Invoke(dependencyObject, dependencyPropertyChangedEventArgs);
        }

        private void TrackOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            if (_track == null || _indicator == null)
            {
                return;
            }

            _indicator.Width = Math.Min(.5 * _track.ActualWidth, 50);

            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (_track == null || _indicator == null)
            {
                return;
            }

            if (!IsVisible || !IsInProgress || IsErrored)
            {
                _indicator.BeginAnimation(MarginProperty, null);

                return;
            }

            var to = _track.ActualWidth;
            var from = -1 * _indicator.Width;

            var translateTime = TimeSpan.FromSeconds((int)(to - from) / 200.0);
            var pauseTime = TimeSpan.FromSeconds(1.0);

            TimeSpan startTime;

            if (_indicator.Margin.Left > from && _indicator.Margin.Left < to - 1)
            {
                startTime = TimeSpan.FromSeconds(-1 * (_indicator.Margin.Left - from) / 200.0);
            }
            else
            {
                startTime = TimeSpan.Zero;
            }

            var animation = new ThicknessAnimationUsingKeyFrames
            {
                BeginTime = startTime,
                Duration = new Duration(translateTime + pauseTime),
                RepeatBehavior = RepeatBehavior.Forever
            };

            animation.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(from, 0, 0, 0), TimeSpan.FromSeconds(0)));
            animation.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(to, 0, 0, 0), translateTime));

            _indicator.BeginAnimation(MarginProperty, animation);
        }
    }
}