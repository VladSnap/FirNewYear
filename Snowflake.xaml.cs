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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FirNewYear
{
    /// <summary>
    /// Логика взаимодействия для Snowflake.xaml
    /// </summary>
    public partial class Snowflake : UserControl
    {
        public double Speed { get; set; }
        public double SpeedLeft { get; set; }
        public double SpeedRotate { get; set; }

        public bool IsDeleted { get; set; }

        RotateTransform Rotate;

        public int Counter { get; set; }
        public double CurrentLeftSpeed { get; set; }

        MainWindow WinMain;
        Snow WinSnow;

        public Snowflake(Canvas canvas, Random rnd, MainWindow window, Snow snow)
        {
            InitializeComponent();

            WinMain = window;
            WinSnow = snow;

            double Left;
            double Top;

            Rotate = new RotateTransform();
            this.RenderTransform = Rotate;
            this.RenderTransformOrigin = new Point(rnd.NextDouble(), rnd.NextDouble());

            Speed = rnd.NextDouble() * 4 + 1;
            SpeedLeft = (int)(Speed * 4);
            CurrentLeftSpeed = rnd.Next(0, 3);

            SpeedRotate = rnd.Next(-6, 6);

            double size = rnd.Next(32, 64);
            this.Width = size;
            this.Height = size;

            Opacity = (size - 32.0d) / (64.0d - 32.0d) + 0.1;

            this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/sf" + rnd.Next(1, 12) + ".png", UriKind.Absolute)));

            Top = size * -1;
            Canvas.SetTop(this, Top);
            Left = rnd.Next(0, (int)(canvas.ActualWidth - (size / 2) - 20));
            Canvas.SetLeft(this, Left);
        }

        public void RotateSnow()
        {
            if (SpeedRotate != 0)
            {
                Rotate.Angle += SpeedRotate;
                this.RenderTransform = Rotate;
            }
        }

        private void SnowFlakeGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                WinMain.Points++;
                InfoPoint text = new InfoPoint(WinSnow);
                text.Text = WinMain.Points.ToString();
                WinSnow.CanvPoint.Children.Add(text);
                Point point = e.GetPosition(WinSnow.CanvPoint);
                Canvas.SetLeft(text, point.X - this.ActualWidth / 2 - text.ActualWidth / 2);
                if (point.Y > 100)
                {
                    Canvas.SetTop(text, point.Y - this.ActualHeight / 2 - 80 - text.ActualHeight / 2);
                }
                else
                {
                    Canvas.SetTop(text, point.Y - this.ActualHeight / 2 + 50 - text.ActualHeight / 2);
                }
                text.StartAnimation();
            }
        }
    }
}
