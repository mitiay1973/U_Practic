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
using System.Windows.Threading;

namespace Up
{
    /// <summary>
    /// Логика взаимодействия для glavn.xaml
    /// </summary>
    public partial class glavn : Page
    {
        Frame frame1;
        string user;
        private DispatcherTimer _timer;

        public static readonly DependencyProperty TickCounterProperty = DependencyProperty.Register(
            "TickCounter", typeof(int), typeof(glavn), new PropertyMetadata(default(int)));
        public glavn( string User,Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
            TickCounter = 150;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1d);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public int TickCounter
        {
            get { return (int)GetValue(TickCounterProperty); }
            set { SetValue(TickCounterProperty, value); }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (--TickCounter <= 0)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                MessageBox.Show($"Закончите работу");
            }
        }
    }
}
