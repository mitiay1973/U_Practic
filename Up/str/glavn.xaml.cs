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
        List<Up.Workers> workers = new List<Up.Workers>();
        List<Up.Service> services = new List<Up.Service>();
        List<Up.Results> result = new List<Up.Results>();
        public int rol = 0;
        public glavn(string User, Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
            LViewresult.Visibility = Visibility.Collapsed;
            Servis.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            history.Visibility = Visibility.Collapsed;
            workers = Entities.GetContext().Workers.ToList();
            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].login == user && workers[i].dolgnost == "Администратор")
                {
                    history.Visibility = Visibility.Visible;
                    Add.Visibility = Visibility.Visible;
                    rol = 1;
                }
                if (workers[i].login == user && workers[i].dolgnost == "Лаборант")
                {
                    Add.Visibility = Visibility.Visible;
                    rol = 2;
                }
            }
            int count_hh = Entities.GetContext().history.Count();
            historys = Entities.GetContext().history.ToList();
            int time = 0;
            for (int j = count_hh - 1; j >= 0; j--)
            {
                if (historys[j].login == user)
                {
                    DateTime b = (DateTime)historys[j].block;
                    DateTime d = (DateTime)historys[j].date;
                    int h = b.Hour - d.Hour;
                    int m = b.Minute - d.Minute;
                    time = 60 * h + m;
                    break;
                }
            }
            DataContext = sp;
            DateTime dateTime = DateTime.Now;
            TickCounter = time;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1d);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
            int count = Entities.GetContext().Service.Count();
            services = Entities.GetContext().Service.ToList();
            sp.CountPage = 3;
            sp.Countlist = count;
            LViewTours.ItemsSource = services.Skip(0).Take(sp.CountPage).ToList();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        Strelki sp = new Strelki();
        private void Update()
        {
            var currentProducts = Entities.GetContext().Service.ToList();
            currentProducts = currentProducts.Where(p => p.Service1.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            sp.CountPage = 3;
            sp.Countlist = currentProducts.Count;
            LViewTours.ItemsSource = currentProducts.Skip(0).Take(sp.CountPage).ToList();
            var currentResult = Entities.GetContext().Results.ToList();

            for (int i = 0; i < currentResult.Count; i++)
            {
                if (currentResult[i].Workers.login != user && currentResult[i].users.login != user)
                {
                    currentResult.RemoveAt(i);
                    i--;
                }
            }
            if (rol != 2)
            {
                currentResult = currentResult.Where(p => p.Workers.name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            }
            else
            {
                currentResult = currentResult.Where(p => p.users.name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            }
            LViewresult.ItemsSource = currentResult.ToList();
        }



        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LViewTours.Visibility == Visibility.Visible)
            {
                TextBlock tb = (TextBlock)sender;

                switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
                {
                    case "prev":
                        sp.CurrentPage--;
                        break;
                    case "next":
                        sp.CurrentPage++;
                        break;
                    case "prev1":
                        sp.CurrentPage = 1;
                        break;
                    case "next1":
                        {
                            List<Service> fl = Entities.GetContext().Service.ToList();
                            int a = fl.Count;
                            int b = Convert.ToInt32(3);

                            if (a % b == 0)
                            {
                                sp.CurrentPage = a / b;
                            }
                            else
                            {
                                sp.CurrentPage = a / b + 1;
                            }

                        }
                        break;
                    default:
                        sp.CurrentPage = Convert.ToInt32(tb.Text);
                        break;
                }
                LViewTours.ItemsSource = services.Skip(sp.CurrentPage * sp.CountPage - sp.CountPage).Take(sp.CountPage).ToList();
            }

            else
            {
                TextBlock tb = (TextBlock)sender;

                switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
                {
                    case "prev":
                        sp.CurrentPage--;
                        break;
                    case "next":
                        sp.CurrentPage++;
                        break;
                    case "prev1":
                        sp.CurrentPage = 1;
                        break;
                    case "next1":
                        {
                            List<Results> fl = Entities.GetContext().Results.ToList();
                            int a = fl.Count;
                            int b = Convert.ToInt32(3);

                            if (a % b == 0)
                            {
                                sp.CurrentPage = a / b;
                            }
                            else
                            {
                                sp.CurrentPage = a / b + 1;
                            }

                        }
                        break;
                    default:
                        sp.CurrentPage = Convert.ToInt32(tb.Text);
                        break;
                }
                LViewresult.ItemsSource = result.Skip(sp.CurrentPage * sp.CountPage - sp.CountPage).Take(sp.CountPage).ToList();
            }

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

        private void history_Click(object sender, RoutedEventArgs e)
        {
            History_vxod history_Vxod = new History_vxod();
            history_Vxod.Show();
        }

        private void analiz_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LViewTours.Visibility = Visibility.Hidden;
            analiz.Visibility = Visibility.Collapsed;
            LViewresult.Visibility = Visibility.Visible;
            Servis.Visibility = Visibility.Visible;
            result = Entities.GetContext().Results.ToList();
            List<users> use = new List<users>();
            TBoxSearch.Text = "";
            result = Entities.GetContext().Results.ToList();
            use = Entities.GetContext().users.ToList();
            int counts1 = Entities.GetContext().Results.Count();
            if (rol == 2)
            {
                for (int i = 0; i < counts1; i++)
                {
                    if (result[i].Workers.login != user)
                    {
                        result.RemoveAt(i);
                        i--;
                        counts1--;
                    }
                }
            }
            if (rol == 0)
            {
                for (int i = 0; i < counts1; i++)
                {
                    if (result[i].users.login != user)
                    {
                        result.RemoveAt(i);
                        i--;
                        counts1--;
                    }
                }
            }
            stack.UpdateLayout();
            Update();
            sp.CountPage = 3;
            sp.Countlist = counts1;
            LViewresult.ItemsSource = result.Skip(0).Take(sp.CountPage).ToList();
        }

        private void Servis_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LViewTours.Visibility = Visibility.Visible;
            analiz.Visibility = Visibility.Visible;
            LViewresult.Visibility = Visibility.Hidden;
            Update();
            Servis.Visibility = Visibility.Collapsed;
        }

        private void Add_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (LViewTours.Visibility == Visibility.Visible)
            {

            }

            else
            {

            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(rol!=0)
            {
                
            }
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (rol != 0)
            {

            }
        }
    }
}
