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
using System.Windows.Threading;

namespace FirNewYear
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random RndMain = new Random();
        MediaElement Player = new MediaElement();

        DispatcherTimer Timer;

        Snow WinSnow;

        private List<Sphere> Spheres = new List<Sphere>();
        private List<Brush> GardBrush = new List<Brush>();
        private List<Brush> LampBrush = new List<Brush>();

        private List<string> PlayList = new List<string>();

        private Random rnd = new Random();

        public int Points { get; set; }

        public Canvas CanvFir
        {
            get
            {
                return CanvasFir;
            }
        }

        private WindowState fCurrentWindowState = WindowState.Normal;
        public WindowState CurrentWindowState
        {
            get { return fCurrentWindowState; }
            set { fCurrentWindowState = value; }
        }

        private bool fVisibleSnow = true;
        public bool VisibleSnow
        {
            get { return fVisibleSnow; }
            set { fVisibleSnow = value; }
        }

        private bool fPlayerPause = false;
        public bool PlayerPause
        {
            get { return fPlayerPause; }
            set { fPlayerPause = value; }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            createTrayIcon();

            ColorSphere.CreateColors();
            RandomLampGard();
            RandomGard();
            CreateSoundList();

            Player.UnloadedBehavior = MediaState.Stop;
            Player.Volume = 0.3d;
            Player.LoadedBehavior = MediaState.Manual;

            Player.MediaEnded += Player_MediaEnded;

            CanvasFir.Children.Add(Player);

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 600);
            Timer.Tick += Timer_Tick;

            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height - 20;

            WinSnow = new Snow(RndMain, this);
            WinSnow.Show();
        }

        void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            NextRandomSoundPlay();
        }

        int CountTick;
        int TickStop = 0;

        void Timer_Tick(object sender, EventArgs e)
        {
            if (CountTick < TickStop + rnd.Next(5, 15))
            {
                if (rnd.Next(1, 100) > 90)
                {
                    Timer.Interval = TimeSpan.FromMilliseconds(200);
                    TickStop = 50;
                }
                foreach (var elips in Spheres)
                {
                    elips.Foreground = LampBrush[rnd.Next(0, ColorSphere.ObjColors.GetLength(0))];
                }
            }
            else
            {
                Timer.Interval = TimeSpan.FromMilliseconds(100);
                Brush br = LampBrush[rnd.Next(0, ColorSphere.ObjColors.GetLength(0))];
                foreach (var elips in Spheres)
                {
                    elips.Foreground = br;
                }
                if (CountTick >= rnd.Next(80, 120))
                {
                    TickStop = 0;
                    Timer.Interval = TimeSpan.FromMilliseconds(600);
                    CountTick = 0;
                }
            }

            Star.Fill = GardBrush[rnd.Next(0, ColorSphere.ObjColors.GetLength(0))];

            CountTick++;
        }

        private void FirGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void FirGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var elips in CanvasFir.Children)
            {
                if (elips.GetType() == typeof(Ellipse))
                {
                    ((Ellipse)elips).Fill = GardBrush[rnd.Next(0, ColorSphere.ObjColors.GetLength(0))];
                    ((Ellipse)elips).MouseDown += Ellipse_MouseDown;
                }

                if (elips.GetType() == typeof(Sphere))
                {
                    Spheres.Add(((Sphere)elips));
                    ((Sphere)elips).Foreground = LampBrush[rnd.Next(0, ColorSphere.ObjColors.GetLength(0))];
                }
            }

            NextRandomSoundPlay();

            Timer.Start();
        }

        void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Ellipse)sender).Fill = GardBrush[rnd.Next(0, ColorSphere.ObjColors.GetLength(0))];
        }

        private void RandomGard()
        {
            for (int i = 0; i < ColorSphere.ObjColors.GetLength(0); i++)
            {
                GradientStopCollection gsc = new GradientStopCollection(3);
                Color[] clr = { ColorSphere.ObjColors[i, 0], ColorSphere.ObjColors[i, 1], ColorSphere.ObjColors[i, 2] };
                gsc.Add(new GradientStop(clr[0], 1D));
                gsc.Add(new GradientStop(clr[1], 0.712D));
                gsc.Add(new GradientStop(clr[2], 0D));
                RadialGradientBrush myLinearGradientBrush = new RadialGradientBrush(gsc);
                myLinearGradientBrush.RadiusX = 0.5;
                myLinearGradientBrush.RadiusY = 0.5;

                GardBrush.Add(myLinearGradientBrush);
            }
        }

        private void RandomLampGard()
        {
            for (int i = 0; i < ColorSphere.ObjColors.GetLength(0); i++)
            {
                GradientStopCollection gsc = new GradientStopCollection(3);
                Color[] clr = { ColorSphere.ObjColors[i, 0], ColorSphere.ObjColors[i, 1], ColorSphere.ObjColors[i, 2] };

                clr[0].A = 30;
                clr[1].A = 150;
                gsc.Add(new GradientStop(clr[0], 1D));
                gsc.Add(new GradientStop(clr[1], 0.5D));
                gsc.Add(new GradientStop(clr[2], 0D));
                RadialGradientBrush myLinearGradientBrush = new RadialGradientBrush(gsc);
                myLinearGradientBrush.RadiusX = 0.5;
                myLinearGradientBrush.RadiusY = 0.5;

                LampBrush.Add(myLinearGradientBrush);
            }
        }

        public void CreateSoundList()
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("sound");
            if (di.Exists)
            {
                foreach (var a in di.GetFiles("sound_*"))
                {
                    PlayList.Add(a.FullName);
                }
            }
        }

        public void NextRandomSoundPlay()
        {
            if (PlayList.Count > 0)
            {
                Player.Source = new Uri(PlayList[rnd.Next(0, PlayList.Count)], UriKind.Absolute);
                Player.Play();
            }
        }

        private System.Windows.Forms.NotifyIcon TrayIcon = null;
        private ContextMenu TrayMenu = null;

        private bool createTrayIcon()
        {
            bool result = false;
            if (TrayIcon == null)
            { // только если мы не создали иконку ранее
                TrayIcon = new System.Windows.Forms.NotifyIcon(); // создаем новую
                TrayIcon.Icon = FirNewYear.Properties.Resources.firicon; // изображение для трея
                // обратите внимание, за ресурсом с картинкой мы лезем в свойства проекта, а не окна,
                // поэтому нужно указать полный namespace
                TrayIcon.Text = "Новогодняя елка. С новым годом =)"; // текст подсказки, всплывающей над иконкой
                TrayMenu = Resources["TrayMenu"] as ContextMenu; // а здесь уже ресурсы окна и тот самый x:Key

                // сразу же опишем поведение при щелчке мыши, о котором мы говорили ранее
                // это будет просто анонимная функция, незачем выносить ее в класс окна
                TrayIcon.Click += delegate(object sender, EventArgs e)
                {
                    if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        Show();
                        // меняем надпись на пункте меню
                        ((TrayMenu.Items[0] as MenuItem).Icon as CheckBox).IsChecked = true;
                        WindowState = CurrentWindowState;
                        Activate(); // обязательно нужно отдать фокус окну,
                    }
                    else
                    {
                        // по правой кнопке (и всем остальным) показываем меню
                        TrayMenu.IsOpen = true;
                        Activate(); // нужно отдать окну фокус, см. ниже
                    }
                };
                result = true;
            }
            else
            { // все переменные были созданы ранее
                result = true;
            }
            TrayIcon.Visible = true; // делаем иконку видимой в трее
            return result;
        }

        private void ShowHideMainWindow(object sender, RoutedEventArgs e)
        {
            TrayMenu.IsOpen = false; // спрячем менюшку, если она вдруг видима
            if (IsVisible)
            {// если окно видно на экране
                // прячем его
                Hide();
                // меняем надпись на пункте меню
                ((TrayMenu.Items[0] as MenuItem).Icon as CheckBox).IsChecked = false;
            }
            else
            { // а если не видно
                // показываем
                Show();
                // меняем надпись на пункте меню
                ((TrayMenu.Items[0] as MenuItem).Icon as CheckBox).IsChecked = true;
                WindowState = CurrentWindowState;
                Activate(); // обязательно нужно отдать фокус окну,
                // иначе пользователь сильно удивится, когда увидит окно

                // но не сможет в него ничего ввести с клавиатуры
            }
        }

        private void ShowHideSnow(object sender, RoutedEventArgs e)
        {
            if (VisibleSnow)
            {
                ((TrayMenu.Items[1] as MenuItem).Icon as CheckBox).IsChecked = false;
                VisibleSnow = false;
                WinSnow.StopSnow();
            }
            else
            {
                ((TrayMenu.Items[1] as MenuItem).Icon as CheckBox).IsChecked = true;
                VisibleSnow = true;
                WinSnow.PlaySnow();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            TrayIcon.Visible = false;
            WinSnow.Close();
            Close();
        }

        private void TopMost(object sender, RoutedEventArgs e)
        {
            ((TrayMenu.Items[2] as MenuItem).Icon as CheckBox).IsChecked = !this.Topmost;
            WinSnow.Topmost = !this.Topmost;
            this.Topmost = !this.Topmost;
        }

        public void Music(object sender, RoutedEventArgs e)
        {
            if (PlayerPause)
            {
                PlayerPause = false;
                ((TrayMenu.Items[3] as MenuItem).Icon as CheckBox).IsChecked = true;
                Player.Play();
            }
            else
            {
                PlayerPause = true;
                ((TrayMenu.Items[3] as MenuItem).Icon as CheckBox).IsChecked = false;
                Player.Pause();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Volume = e.NewValue;
        }
    }
}
