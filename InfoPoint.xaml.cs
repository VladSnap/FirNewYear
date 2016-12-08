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

namespace FirNewYear
{
    /// <summary>
    /// Логика взаимодействия для InfoPoint.xaml
    /// </summary>
    public partial class InfoPoint : UserControl
    {
        public string Text
        {
            get
            {
                return textBlock.Text;
            }
            set
            {
                textBlock.Text = value;
            }
        }

        Snow WinSnow;

        public InfoPoint(Snow snow)
        {
            InitializeComponent();
            WinSnow = snow;
        }

        public void StartAnimation()
        {
            DoubleAnimation anim = new DoubleAnimation(textBlock.FontSize, textBlock.FontSize + 50,
                new Duration(TimeSpan.FromSeconds(0.3)));

            DoubleAnimation animOpacity = new DoubleAnimation(1, 0,
                new Duration(TimeSpan.FromSeconds(0.4)));
            animOpacity.Completed += animOpacity_Completed;

            textBlock.BeginAnimation(FontSizeProperty, anim);
            textBlock.BeginAnimation(OpacityProperty, animOpacity);
        }

        void animOpacity_Completed(object sender, EventArgs e)
        {
            WinSnow.CanvPoint.Children.Remove(this);
        }
    }
}
