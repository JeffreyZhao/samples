using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfUserControl.ViewModels;

namespace WpfUserControl.Views
{
    /// <summary>
    /// Interaction logic for ValueInput.xaml
    /// </summary>
    public partial class ValueInput : UserControl
    {
        public ValueInput()
            : this(new ValueInputViewModel())
        { }

        public ValueInput(ValueInputViewModel viewModel)
        {
            InitializeComponent();

            viewModel.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == "Text")
                {
                    if (!String.Equals(viewModel.Text, this.Text))
                    {
                        this.Text = viewModel.Text;
                    }
                }
                else if (args.PropertyName == "Number")
                {
                    if (!viewModel.Number.Equals(this.Number))
                    {
                        this.Number = viewModel.Number;
                    }
                }
            };

            this.ViewModel = viewModel;
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(ValueInput),
                new FrameworkPropertyMetadata(OnTextPropertyChanged) { BindsTwoWayByDefault = true });

        private static void OnTextPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var valueInput = (ValueInput)o;
            if (!String.Equals(valueInput.ViewModel.Text, valueInput.Text))
            {
                valueInput.ViewModel.Text = valueInput.Text;
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register(
                "Number",
                typeof(int),
                typeof(ValueInput),
                new FrameworkPropertyMetadata(OnNumberPropertyChanged) { BindsTwoWayByDefault = true });

        private static void OnNumberPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var valueInput = (ValueInput)o;
            if (!valueInput.ViewModel.Number.Equals(valueInput.Number))
            {
                valueInput.ViewModel.Number = valueInput.Number;
            }
        }

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ValueInputViewModel), typeof(ValueInput));

        public ValueInputViewModel ViewModel
        {
            get { return (ValueInputViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ReadOnlyValueProperty =
            DependencyProperty.Register("ReadOnlyValue", typeof(int), typeof(ValueInput));

        public int ReadOnlyValue
        {
            private get { return (int)GetValue(ReadOnlyValueProperty); }
            set { SetValue(ReadOnlyValueProperty, value); }
        }
    }
}
