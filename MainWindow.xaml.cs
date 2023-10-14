using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace HW_16._09._23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.SizeChanged += (s, e) =>
            //{
            //    if (this.ActualWidth != this.ActualHeight)
            //    {
            //        double minSize = Math.Min(this.ActualWidth, this.ActualHeight);
            //        this.Width = minSize;
            //        this.Height = minSize;
            //    }
            //};
        }

        //private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    GridForRectangles.Width = e.NewSize.Width;
        //    GridForRectangles.Height = e.NewSize.Height;
        //}
        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double circleRadius = Math.Min(e.NewSize.Width / 2, e.NewSize.Height / 2);
            double centerX = e.NewSize.Width / 2;
            double centerY = e.NewSize.Height / 2;
            double squareSize = circleRadius / 2; // Размер квадрата - половина радиуса круга

            // Ограничение положения квадратов внутри круга
            double left = Math.Max(centerX - squareSize, 0);
            double top = Math.Max(centerY - squareSize, 0);

            // Проверка, чтобы квадраты не выходили за пределы круга
            if (left + squareSize > circleRadius * 2)
            {
                left = circleRadius * 2 - squareSize;
            }
            if (top + squareSize > circleRadius * 2)
            {
                top = circleRadius * 2 - squareSize;
            }

            foreach (UIElement element in GridForRectangles.Children)
            {
                if (element is Rectangle)
                {
                    Rectangle rect = (Rectangle)element;

                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(rect, top);
                    rect.Width = squareSize;
                    rect.Height = squareSize;
                }
            }
        }







    }

    public class MinConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length > 0)
            {
                // Выбираем минимальное значение из всех переданных значений
                return values.Min(value => (double)value);
            }
            return 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double originalValue && parameter is double offset)
            {
                // Вычисляем новое значение, с учетом смещения
                return originalValue / 2 + offset;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
