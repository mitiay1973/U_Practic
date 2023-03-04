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
        private DispatcherTimer _timer;

        public static readonly DependencyProperty TickCounterProperty = DependencyProperty.Register(
            "TickCounter", typeof(int), typeof(glavn), new PropertyMetadata(default(int)));
        Frame frame1;
        string user;
        List<Up.history> historys = new List<Up.history>();
        public glavn( string User,Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
            int count_hh = Entities.GetContext().history.Count();
            historys = Entities.GetContext().history.ToList();
            int time = 0;
            for (int j = count_hh - 1; j >= 0; j--)
            {          
                if (historys[j].login == user)
                {
                    DateTime b = (DateTime)historys[j].block;
                    DateTime d = (DateTime)historys[j].date;
                    int h = b.Hour-d.Hour;
                    int m = b.Minute-d.Minute;
                    time = 60 * h + m;
                    break;
                }
            }
            DateTime dateTime = DateTime.Now;
            TickCounter = time;
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
        public int soxr = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (--TickCounter <= 0)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                if (soxr == 0)
                {
                    if (MessageBox.Show("Чтобы закончить работу и закрыть кабинет на кварцевание нажмите да, если хотите продолжить работу на 5 минут нажмите нет", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        TickCounter = 5;
                        _timer = new DispatcherTimer();
                        _timer.Interval = TimeSpan.FromMinutes(1d);
                        _timer.Tick += new EventHandler(Timer_Tick);
                        _timer.Start();
                        soxr = 1;
                    }
                    else
                    {
                        MessageBox.Show("Закрытие программы");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show("Закрытие программы");
                    Environment.Exit(0);
                }
            }
        }
    }
}
