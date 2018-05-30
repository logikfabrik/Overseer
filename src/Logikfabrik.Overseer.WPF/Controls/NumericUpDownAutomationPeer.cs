// <copyright file="NumericUpDownAutomationPeer.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Controls
{
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    /// <summary>
    /// The <see cref="NumericUpDownAutomationPeer" /> class.
    /// </summary>
    /// <remarks>
    /// Based on https://github.com/T-Alex/WPFControls, written by T-Alex, https://github.com/T-Alex.
    /// </remarks>
    // ReSharper disable once InheritdocConsiderUsage
    public class NumericUpDownAutomationPeer : FrameworkElementAutomationPeer, IRangeValueProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericUpDownAutomationPeer" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        // ReSharper disable once InheritdocConsiderUsage
        // ReSharper disable once SuggestBaseTypeForParameter
        public NumericUpDownAutomationPeer(NumericUpDown owner)
            : base(owner)
        {
        }

        /// <inheritdoc />
        double IRangeValueProvider.Value => GetOwner().Value;

        /// <inheritdoc />
        double IRangeValueProvider.Minimum => GetOwner().Minimum;

        /// <inheritdoc />
        double IRangeValueProvider.Maximum => GetOwner().Maximum;

        /// <inheritdoc />
        double IRangeValueProvider.SmallChange => GetOwner().Increment;

        /// <inheritdoc />
        double IRangeValueProvider.LargeChange => GetOwner().Increment;

        /// <inheritdoc />
        bool IRangeValueProvider.IsReadOnly => GetOwner().IsReadOnly;

        /// <inheritdoc />
        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.RangeValue ? this : base.GetPattern(patternInterface);
        }

        /// <inheritdoc />
        void IRangeValueProvider.SetValue(double value)
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            var v = (int)value;
            var owner = GetOwner();

            if (v < owner.Minimum || v > owner.Maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            owner.Value = v;
        }

        /// <inheritdoc />
        protected override string GetClassNameCore()
        {
            return nameof(NumericUpDown);
        }

        /// <inheritdoc />
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }

        private NumericUpDown GetOwner()
        {
            return (NumericUpDown)Owner;
        }
    }
}