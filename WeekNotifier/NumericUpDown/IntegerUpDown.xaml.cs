// ***********************************************************************
// Assembly         : WeekNotifier
// Author           : Tom Richter
// Created          : 02-28-2021
//
// Last Modified By : Tom Richter
// Last Modified On : 02-28-2021
// ***********************************************************************
// <copyright file="IntegerUpDown.xaml.cs" company="Tom Richter">
//     Copyright (c) 2005-2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WeekNotifier.NumericUpDown
{
    /*
		2021.02.20/jms
		This code started out from this CodeProject article (posted in 2011): 
		https://www.codeproject.com/Articles/151298/WPF-User-Control-NumericBox?msg=5788701#xx5788701xx
		(WPF User Control - NumericBox)

		I change the following:
		- deleted menu and popup components
		- Deleted the timer and associated button click preview events
		- Converted the regular Button controls to RepeatButton controls
		- Converted from double to int support
		- Improved the handling of manually entered text
		- Improved general layout of components
		- Changed button style to reflect my own tastes
		- Removed double-declared events
	*/

    /// <summary>
    /// Class IntegerUpDown.
    /// Implements the <see cref="System.Windows.Controls.UserControl" />
    /// Implements the <see cref="System.Windows.Markup.IComponentConnector" />
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [TemplatePart(Name = "PART_NumericTextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_IncreaseButton", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "PART_DecreaseButton", Type = typeof(RepeatButton))]
    public partial class IntegerUpDown : UserControl
    {
        #region Variables

        // 2021.02.25/jms - removed unused fields

        private string _valueFormat;
        // 2021.02.25/jms - added field to retain initial value set in the xaml. if no value was 
        // set, we use the default value specified in the Value attached property
        private int _initialValue;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerUpDown"/> class.
        /// </summary>
        public IntegerUpDown()
        {
            InitializeComponent();
        }

        #region Properties

        //===========================================================
        // 2021.02.25/jms - new attached property. If set to true, it plays the error sound when 
        // the user makes an invalid edit in the textbox. Default=false.
        /// <summary>
        /// The silent error property
        /// </summary>
        public static readonly DependencyProperty SilentErrorProperty =
            DependencyProperty.Register("SilentErrorSeparator", typeof(bool), 
                typeof(IntegerUpDown), new PropertyMetadata(false));
        
        /// <summary>
        /// Gets or sets a value indicating whether [silent error].
        /// </summary>
        /// <value><c>true</c> if [silent error]; otherwise, <c>false</c>.</value>
        public bool SilentError
        {
            get => (bool)GetValue(SilentErrorProperty);
            set => SetValue(SilentErrorProperty, value);
        }

        //===========================================================
        // 2021.02.25/jms - new attached property. If set to true, allows user to manually edit 
        // value in text box. Default= true.
        /// <summary>
        /// The allow manual edit property
        /// </summary>
        public static readonly DependencyProperty AllowManualEditProperty =
            DependencyProperty.Register("AllowManualEditSeparator", typeof(bool), 
                typeof(IntegerUpDown), new PropertyMetadata(true));
        
        /// <summary>
        /// Gets or sets a value indicating whether [allow manual edit].
        /// </summary>
        /// <value><c>true</c> if [allow manual edit]; otherwise, <c>false</c>.</value>
        public bool AllowManualEdit
        {
            get => (bool)GetValue(AllowManualEditProperty);
            set => SetValue(AllowManualEditProperty, value);
        }

        //===========================================================
        // Specifies the minimum possible value. Default=int.MinValue.
        /// <summary>
        /// The minimum property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), 
                typeof(IntegerUpDown), new PropertyMetadata(int.MinValue));
        
        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get => Math.Max(int.MinValue, (int)GetValue(MinimumProperty));
            set => SetValue(MinimumProperty, value);
        }

        //===========================================================
        // Specifies the maximum possible value. Default=int.MaxValue.
        /// <summary>
        /// The maximum property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), 
                typeof(IntegerUpDown), new PropertyMetadata(int.MaxValue));
        
        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get => Math.Min(int.MaxValue, (int)GetValue(MaximumProperty));
            set => SetValue(MaximumProperty, value);
        }

        //===========================================================
        // Specifies the increment by which the Value increases/decreases when one of the buttons 
        // is clicked. Default=1.
        /// <summary>
        /// The increment property
        /// </summary>
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(int), 
                typeof(IntegerUpDown), new PropertyMetadata(1));
        
        /// <summary>
        /// Gets or sets the increment.
        /// </summary>
        /// <value>The increment.</value>
        public int Increment
        {
            get => Math.Min(int.MaxValue, Math.Abs((int)GetValue(IncrementProperty)));
            set => SetValue(IncrementProperty, value);
        }

        //===========================================================
        // Specifies the value of the control. Default=0.
        /// <summary>
        /// The value property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), 
                typeof(IntegerUpDown), new PropertyMetadata(0, OnValueChanged));
        
        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var numericBoxControl = (IntegerUpDown)sender;
            numericBoxControl.OnValueChanged((int)args.OldValue, (int)args.NewValue);
        }
        
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion

        /// <summary>
        /// The value changed event
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Direct, 
                typeof(RoutedPropertyChangedEventHandler<int>), typeof(IntegerUpDown));

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }

        private void OnValueChanged(int oldValue, int newValue)
        {
            var args = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue)
            {
                RoutedEvent = ValueChangedEvent
            };
            RaiseEvent(args);
        }


        #region Private Methods

        //=============================================================
        /// <summary>
        /// Increase value
        /// </summary>
        private void IncreaseValue()
        {
            Value = Math.Min(Maximum, Value + Increment);
            //2021.02.25/jms - update the text box
            PART_NumericTextBox.Text = Value.ToString(_valueFormat);
        }
        //=============================================================
        /// <summary>
        /// Decrease value
        /// </summary>
        private void DecreaseValue()
        {
            Value = Math.Max(Minimum, Value - Increment);
            //2021.02.25/jms - update the text box
            PART_NumericTextBox.Text = Value.ToString(_valueFormat);
        }

        #endregion

        //=============================================================
        /// <summary>
        /// Apply new templates after setting new style
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_IncreaseButton.Click += increaseBtn_Click;
            PART_DecreaseButton.Click += decreaseBtn_Click;

            //2021.02.25/jms - add support for new AllowManualEdit property
            if (AllowManualEdit)
            {
                PART_NumericTextBox.PreviewTextInput += numericBox_PreviewTextInput;
                PART_NumericTextBox.TextChanged += PART_NumericTextBox_TextChanged;
            }

            PART_NumericTextBox.IsReadOnly = !AllowManualEdit;
            PART_NumericTextBox.MouseWheel += numericBox_MouseWheel;


            // 2021.02.25/jms - this doesn't seem right. why can't I do this in the property setter?
            _valueFormat = "0";
            // 2021.02.25/jms - set the initial value so we have something to set the value to when 
            // the user deletes all of the text in the textbox.
            _initialValue = Value;
            PART_NumericTextBox.Text = Value.ToString(_valueFormat);
        }

        #region Events

        private void numericBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textbox = sender as TextBox;
            var caretIndex = textbox.CaretIndex;
            try
            {
                // 2021.02.25/jms - completely refactored to only validate the entered text, and 
                // NOT update the textbox directly
                var isError = false;
                var text = PART_NumericTextBox.Text.Insert(caretIndex, e.Text);
                isError = (!int.TryParse(text, out var value));
                isError |= (value < Minimum || value > Maximum);
                if (isError)
                {
                    if (!SilentError)
                    {
                        SystemSounds.Hand.Play();
                    }
                    e.Handled = true;
                }
            }
            catch (FormatException)
            {
            }
        }

        // 2021.02.25/jms - Instead of letting the input preview event manage the actual text in 
        // the textbox, I added this event handler to do it 
        private void PART_NumericTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = PART_NumericTextBox.Text;
            // if the text is empty, use the initial Value
            if (string.IsNullOrEmpty(text))
            {
                text = _initialValue.ToString(_valueFormat);
                PART_NumericTextBox.Text = text;
            }
            // personal discovery - int.Parse won't parse negative numbers unless you use the 
            // number styles indicated here. There are a bunch of interesting number styles 
            // available, and you can even parse the numeric values in a mixed-character string. 
            // Interesting stuff.
            Value = (text == "-") ? 0 : int.Parse(text, NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign);
        }

        private void numericBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                IncreaseValue();
            }
            else if (e.Delta < 0)
            {
                DecreaseValue();
            }
        }

        private void increaseBtn_Click(object sender, RoutedEventArgs e)
        {
            IncreaseValue();
        }

        private void decreaseBtn_Click(object sender, RoutedEventArgs e)
        {
            DecreaseValue();
        }

        #endregion Events

    }
}
