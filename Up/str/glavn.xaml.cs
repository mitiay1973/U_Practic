using Aspose.BarCode.Generation;
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
using System.IO;

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
        List<Up.Service> services1 = new List<Up.Service>();
        string imagePath;
        List<string> filtr = new List<string>() { "Фильтрация","До 500 руб.", "от 500 до 1000 руб.", "Больше 1000 руб." };
        public int rol = 0;
        public glavn(string User, Frame frame)
        {
            services1 = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services1.Count; i++)
            {
                BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, Convert.ToString(services1[i].id));
                var imageType = "Png";
                // установить разрешение
                generator.Parameters.Resolution = 400;
                imagePath = "barcode" + i + ".Png";
                string path = System.IO.Path.GetFullPath(imagePath);
                // сгенерировать штрих-код          
                generator.Save(imagePath, BarCodeImageFormat.Png);
                services1[i].barcode = path;
                Entities.GetContext().SaveChanges();
            }
            InitializeComponent();
            frame1 = frame;
            user = User;
            LViewresult.Visibility = Visibility.Collapsed;
            Servis.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Collapsed;
            history.Visibility = Visibility.Collapsed;
            Type1.Visibility = Visibility.Collapsed;
            Type2.Visibility = Visibility.Collapsed;
            Type.ItemsSource = filtr;
            Type.SelectedIndex = 0;
            var all1 = Entities.GetContext().Workers.ToList();
            all1.Insert(0, new Workers
            {
                name = "Лаборант"
            });
            Type1.ItemsSource = all1;
            Type1.SelectedIndex = 0;
            var all2 = Entities.GetContext().users.ToList();
            all2.Insert(0, new users
            {
                name = "Пациент"
            });
            Type2.ItemsSource = all2;
            Type2.SelectedIndex = 0;
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
        private async void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(100);
            Update();
        }
        Strelki sp = new Strelki();
        private void Update()
        {
            for (int i = 0; i < services1.Count; i++)
            {
                imagePath = "barcode" + i + ".Png";
                string path = System.IO.Path.GetFullPath(imagePath);
                services1[i].barcode = path;
                Entities.GetContext().SaveChanges();
            }
            var currentProducts = Entities.GetContext().Service.ToList();
            currentProducts = currentProducts.Where(p => p.Service1.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            for (int i = 0; i < currentProducts.Count; i++)
            {
                if (Type.SelectedIndex != 0)
                {
                    if (Type.SelectedIndex==1)
                    {
                        if (currentProducts[i].Price>=500)
                        {
                            currentProducts.RemoveAt(i);
                            i--;
                        }
                    }
                    if (Type.SelectedIndex == 2)
                    {
                        if (currentProducts[i].Price < 500 || currentProducts[i].Price >= 1000)
                        {
                            currentProducts.RemoveAt(i);
                            i--;
                        }
                    }
                    if (Type.SelectedIndex == 3)
                    {
                        if (currentProducts[i].Price < 1000)
                        {
                            currentProducts.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
            if (LViewTours.Visibility == Visibility.Visible)
            {
                sp.CountPage = 3;
                sp.Countlist = currentProducts.Count;
            }
            services= currentProducts.ToList();
            LViewTours.ItemsSource = currentProducts.Skip(0).Take(sp.CountPage).ToList();
            var currentResult = Entities.GetContext().Results.ToList();
            if(rol!=1)
            {
                for (int i = 0; i < currentResult.Count; i++)
                {
                    if (currentResult[i].Workers.login != user && currentResult[i].users.login != user)
                    {
                        currentResult.RemoveAt(i);
                        i--;
                    }
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
            if(rol==2)
            {
                for (int i = 0; i < currentResult.Count; i++)
                {
                    if (Type2.SelectedIndex != 0)
                    {
                        if (Type2.Text != currentResult[i].users.name)
                        {
                            currentResult.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
            if (rol == 0 || rol==1)
            {
                for (int i = 0; i < currentResult.Count; i++)
                {
                    if (Type1.SelectedIndex != 0)
                    {
                        if (Type1.Text != currentResult[i].Workers.name)
                        {
                            currentResult.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
            if (LViewresult.Visibility == Visibility.Visible)
            {
                sp.CountPage = 3;
                sp.Countlist = currentResult.Count;
            }
            result= currentResult.ToList();
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
                            List<Service> fl = services;
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
                            List<Results> fl = result;
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
            Type.Visibility = Visibility.Collapsed;
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
                Type.SelectedIndex = 0;
                Type2.Visibility = Visibility.Visible;
            }
            if(rol==1)
            {
                Type.SelectedIndex = 0;
                Type1.Visibility = Visibility.Visible;
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
                Type.SelectedIndex = 0;
                Type1.Visibility = Visibility.Visible;
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
            Type.Visibility = Visibility.Visible;
            Type2.Visibility = Visibility.Collapsed;
            Type1.SelectedIndex = 0;
            Type2.SelectedIndex = 0;
            Type1.Visibility = Visibility.Collapsed;
            LViewresult.Visibility = Visibility.Hidden;
            TBoxSearch.Text = "";
            Update();
            Servis.Visibility = Visibility.Collapsed;
        }

        private void Add_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (LViewTours.Visibility == Visibility.Visible)
            {
                frame1.Navigate(new add_service(user, frame1));
            }

            else
            {
                frame1.Navigate(new add_result(user, frame1));
            }
        }

        private async void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(rol!=0)
            {
                await Task.Delay(100);
                object itemm = LViewTours.SelectedItem;
                frame1.Navigate(new upd_service( user, frame1, itemm));
            }
        }

        private async void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (rol != 0)
            {
                await Task.Delay(100);
                object itemm = LViewresult.SelectedItem;
                frame1.Navigate(new upd_result(user, frame1, itemm));
            }
        }
    }
}
