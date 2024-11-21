using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BootstrapSvgIconControlTest.Controls
{
    public partial class BootstrapSvgIconControl : UserControl
    {
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(
                nameof(Id),
                typeof(string),
                typeof(BootstrapSvgIconControl),
                new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(
                nameof(Scale),
                typeof(double),
                typeof(BootstrapSvgIconControl),
                new FrameworkPropertyMetadata(1.0));

        public string Id
        {
            get => (string)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public BootstrapSvgIconControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Style = Application.Current.Resources[this.Id] as Style;
            this.ScaleTransform.ScaleX = this.Scale;
            this.ScaleTransform.ScaleY = this.Scale;
            this.ScaleTransform.CenterX = 8.0;
            this.ScaleTransform.CenterY = 8.0;

            this.Margin = new Thickness(8 * (this.Scale - 1));
        }
    }
}
