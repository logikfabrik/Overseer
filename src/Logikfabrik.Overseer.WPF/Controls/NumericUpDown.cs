// <copyright file="NumericUpDown.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Controls
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// The <see cref="NumericUpDown" /> class.
    /// </summary>
    /// <remarks>
    /// Based on https://github.com/T-Alex/WPFControls, written by T-Alex, https://github.com/T-Alex.
    /// </remarks>
    [DefaultProperty(nameof(Value))]
    [DefaultEvent(nameof(ValueChanged))]
    [TemplatePart(Name = ValuePartName, Type = typeof(TextBox))]
    [TemplatePart(Name = IncreasePartName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = DecreasePartName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = BorderPartName, Type = typeof(Border))]

    // ReSharper disable once InheritdocConsiderUsage
    public class NumericUpDown : Control
    {
        /// <summary>
        /// Identifies the <see cref="Value" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(DefaultMinimum, OnValueChanged, CoerceValue));

        /// <summary>
        /// Identifies the <see cref="Minimum" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof(Minimum), typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(DefaultMinimum, OnMinimumMaximumChanged));

        /// <summary>
        /// Identifies the <see cref="Maximum" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(DefaultMaximum, OnMinimumMaximumChanged, CoerceMaximum));

        /// <summary>
        /// Identifies the <see cref="Increment" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(nameof(Increment), typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(DefaultIncrement), ValidateIncrement);

        /// <summary>
        /// Identifies the <see cref="IsReadOnly" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(NumericUpDown), new FrameworkPropertyMetadata(false, OnIsReadOnlyChanged));

        /// <summary>
        /// Identifies the <see cref="ValueChanged" /> routed event.
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(NumericUpDown));

        private const int DefaultMinimum = 0;
        private const int DefaultMaximum = 100;
        private const int DefaultIncrement = 1;

        private const string ValuePartName = "PART_Value";
        private const string IncreasePartName = "PART_Increase";
        private const string DecreasePartName = "PART_Decrease";
        private const string BorderPartName = "PART_Border";

        private TextBox _valueTextBox;
        private ButtonBase _increaseButton;
        private ButtonBase _decreaseButton;

        private int _value;
        private string _text;

        /// <summary>
        /// Initializes static members of the <see cref="NumericUpDown" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));

            EventManager.RegisterClassHandler(typeof(NumericUpDown), Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericUpDown" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public NumericUpDown()
        {
            _text = string.Empty;
        }

        /// <summary>
        /// Occurs when <see cref="Value" /> changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }

            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value
        {
            get
            {
                return (int)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public int Minimum
        {
            get
            {
                return (int)GetValue(MinimumProperty);
            }

            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public int Maximum
        {
            get
            {
                return (int)GetValue(MaximumProperty);
            }

            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the increment.
        /// </summary>
        /// <value>
        /// The increment.
        /// </value>
        public int Increment
        {
            get
            {
                return (int)GetValue(IncrementProperty);
            }

            set
            {
                SetValue(IncrementProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }

            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_valueTextBox != null)
            {
                _valueTextBox.TextChanged -= ValueTextBoxOnTextChanged;
                _valueTextBox.PreviewKeyDown -= ValueTextBoxOnPreviewKeyDown;
            }

            if (_increaseButton != null)
            {
                _increaseButton.Click -= IncreaseButtonOnClick;
            }

            if (_decreaseButton != null)
            {
                _decreaseButton.Click -= DecreaseButtonOnClick;
            }

            _valueTextBox = GetTemplateChild(ValuePartName) as TextBox;

            if (_valueTextBox != null)
            {
                _valueTextBox.TextChanged += ValueTextBoxOnTextChanged;
                _valueTextBox.PreviewKeyDown += ValueTextBoxOnPreviewKeyDown;
            }

            _increaseButton = GetTemplateChild(IncreasePartName) as ButtonBase;

            if (_increaseButton != null)
            {
                _increaseButton.Click += IncreaseButtonOnClick;
            }

            _decreaseButton = GetTemplateChild(DecreasePartName) as ButtonBase;

            if (_decreaseButton != null)
            {
                _decreaseButton.Click += DecreaseButtonOnClick;
            }

            UpdateText();
            SetIsReadOnly(this, IsReadOnly);
        }

        /// <inheritdoc />
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new NumericUpDownAutomationPeer(this);
        }

        /// <inheritdoc />
        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            var newValue = (bool)e.NewValue;

            if (newValue)
            {
                OnGotFocus();
            }
            else
            {
                OnLostFocus();
            }
        }

        /// <inheritdoc />
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!IsKeyboardFocusWithin || IsReadOnly)
            {
                return;
            }

            if (e.Delta > 0)
            {
                Increase();
            }
            else
            {
                Decrease();
            }
        }

        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<int> e)
        {
            RaiseEvent(e);
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)sender;

            var oldValue = (int)e.OldValue;
            var newValue = (int)e.NewValue;

            var peer = (NumericUpDownAutomationPeer)UIElementAutomationPeer.FromElement(control);

            peer?.RaisePropertyChangedEvent(RangeValuePatternIdentifiers.ValueProperty, oldValue, newValue);

            control.OnValueChanged(new RoutedPropertyChangedEventArgs<int>(oldValue, newValue, ValueChangedEvent));
            control.UpdateText();
        }

        private static void OnMinimumMaximumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)sender;

            control.CoerceValue(ValueProperty);
        }

        private static void OnIsReadOnlyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)sender;

            var newValue = (bool)e.NewValue;

            SetIsReadOnly(control, newValue);
        }

        private static void SetIsReadOnly(NumericUpDown control, bool newValue)
        {
            if (control._valueTextBox != null)
            {
                control._valueTextBox.IsReadOnly = newValue;
            }

            if (control._increaseButton != null)
            {
                control._increaseButton.IsEnabled = !newValue;
            }

            if (control._decreaseButton != null)
            {
                control._decreaseButton.IsEnabled = !newValue;
            }
        }

        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = (NumericUpDown)sender;

            if (!control.IsKeyboardFocusWithin)
            {
                e.Handled = control.Focus() || e.Handled;
            }
        }

        private static object CoerceValue(DependencyObject sender, object value)
        {
            var control = (NumericUpDown)sender;

            var newValue = decimal.ToInt32(Math.Max(control.Minimum, Math.Min(control.Maximum, (int)value)));

            return newValue;
        }

        private static object CoerceMaximum(DependencyObject sender, object value)
        {
            var control = (NumericUpDown)sender;

            var newValue = decimal.ToInt32(Math.Max((int)value, control.Minimum));

            return newValue;
        }

        private static bool ValidateIncrement(object value)
        {
            return (int)value > 0;
        }

        private void ValueTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;

            if (!IsReadOnly)
            {
                var numberFormatInfo = NumberFormatInfo.CurrentInfo;

                if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == numberFormatInfo.NegativeSign)
                {
                    return;
                }

                int value;

                if (int.TryParse(textBox.Text, NumberStyles.AllowLeadingSign, numberFormatInfo, out value))
                {
                    _text = textBox.Text;
                    _value = value;

                    return;
                }

                ReturnPreviousInput(textBox);
            }
            else
            {
                _text = textBox.Text;
                _value = Value;
            }
        }

        private void ValueTextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.Key)
            {
                case Key.Up:
                    Increase();

                    break;

                case Key.Down:
                    Decrease();

                    break;

                case Key.Return:
                    UpdateValue();
                    UpdateText();

                    break;

                default:
                    return;
            }

            e.Handled = true;
        }

        private void DecreaseButtonOnClick(object sender, RoutedEventArgs e)
        {
            Decrease();
        }

        private void IncreaseButtonOnClick(object sender, RoutedEventArgs e)
        {
            Increase();
        }

        private void Increase()
        {
            UpdateValue();

            if (Value + Increment <= Maximum)
            {
                Value += Increment;
            }
        }

        private void Decrease()
        {
            UpdateValue();

            if (Value - Increment >= Minimum)
            {
                Value -= Increment;
            }
        }

        private void OnGotFocus()
        {
            _valueTextBox?.Focus();
            UpdateText();
        }

        private void OnLostFocus()
        {
            UpdateValue();
            UpdateText();
        }

        private void UpdateText()
        {
            if (_valueTextBox == null)
            {
                return;
            }

            var formattedValue = Value.ToString(NumberFormatInfo.CurrentInfo);

            _text = formattedValue;
            _valueTextBox.Text = formattedValue;
        }

        private void UpdateValue()
        {
            if (Value == _value)
            {
                return;
            }

            Value = (int)CoerceValue(this, _value);
        }

        private void ReturnPreviousInput(TextBox textBox)
        {
            var selectionLenght = textBox.SelectionLength;
            var selectionStart = textBox.SelectionStart;

            textBox.Text = _text;
            textBox.SelectionStart = selectionStart == 0 ? 0 : selectionStart - 1;
            textBox.SelectionLength = selectionLenght;
        }
    }
}