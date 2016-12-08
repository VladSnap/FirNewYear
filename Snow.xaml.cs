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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FirNewYear
{
    /// <summary>
    /// Логика взаимодействия для Snow.xaml
    /// </summary>
    public partial class Snow : Window
    {
        Random Rnd;
        DispatcherTimer Timer = new DispatcherTimer();
        DispatcherTimer TimerSnow = new DispatcherTimer();
        List<Snowflake> RemoveList = new List<Snowflake>();
        MainWindow Main;

        public Canvas CanvPoint
        {
            get
            {
                return CanvasPoint;
            }
        }

        public Snow(Random rnd, MainWindow window)
        {
            Main = window;
            Rnd = rnd;
            InitializeComponent();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 4000);
            Timer.Tick += Timer_Tick;

            TimerSnow.Interval = new TimeSpan(0, 0, 0, 0, 50);
            TimerSnow.Tick += TimerSnow_Tick;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            AddSnow();
        }

        public void StopSnow()
        {
            Timer.Stop();
            TimerSnow.Stop();
            Hide();
        }

        public void PlaySnow()
        {
            Timer.Start();
            TimerSnow.Start();
            Show();
        }

        private void CanvasShow_Loaded(object sender, RoutedEventArgs e)
        {
            Timer.Start();
            TimerSnow.Start();
            AddSnow();
        }

        public void AddSnow()
        {
            for (int i = 0; i < Rnd.Next(10, 25); i++)
            {
                CanvasShow.Children.Add(new Snowflake(CanvasShow, Rnd, Main, this));
            }
        }

        double TopSF;
        double LeftSF;
        int CounterClearSnow;

        void TimerSnow_Tick(object sender, EventArgs e)
        {
            TimerSnow.Stop();
            //Canvas.SetLeft(this, Left);
            foreach (Snowflake snowflake in CanvasShow.Children)
            {
                TopSF = Canvas.GetTop(snowflake);
                LeftSF = Canvas.GetLeft(snowflake);

                if (LeftSF > CanvasShow.ActualWidth)
                {
                    LeftSF = (snowflake.Width - 10) * -1;
                }
                else if (LeftSF < snowflake.Width * -1)
                {
                    LeftSF = CanvasShow.ActualWidth + snowflake.Width - 10;
                }

                if (TopSF > CanvasShow.ActualHeight - 150 || snowflake.IsDeleted)
                {
                    snowflake.Opacity -= 0.05;
                }

                if (snowflake.Opacity <= 0 && !snowflake.IsDeleted)
                {
                    snowflake.IsDeleted = true;
                    RemoveList.Add(snowflake);
                }

                snowflake.Counter++;

                if (snowflake.Speed > 0 && snowflake.Counter >= Rnd.Next(6, 12))
                {
                    snowflake.CurrentLeftSpeed = (snowflake.SpeedLeft * Math.Sin(LeftSF)) / 5;
                    LeftSF += snowflake.CurrentLeftSpeed;
                    snowflake.Counter = 0;
                }
                else
                {
                    LeftSF += snowflake.CurrentLeftSpeed / 2;
                }

                snowflake.RotateSnow();

                TopSF += snowflake.Speed;

                Canvas.SetTop(snowflake, TopSF);
                Canvas.SetLeft(snowflake, LeftSF);
            }

            CounterClearSnow++;
            if (CounterClearSnow > 150)
            {
                foreach (Snowflake snowflake in RemoveList)
                {
                    CanvasShow.Children.Remove(snowflake);
                }
                RemoveList.Clear();
                CounterClearSnow = 0;
            }

            TimerSnow.Start();
        }
    }
}
