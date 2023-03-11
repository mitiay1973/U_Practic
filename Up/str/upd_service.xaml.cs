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

namespace Up
{
    /// <summary>
    /// Логика взаимодействия для upd_service.xaml
    /// </summary>
    public partial class upd_service : Page
    {
        Frame frame1;
        string user;
        object item;
        List<Service> services = new List<Service> { };
        public upd_service(string User, Frame frame, object itemm)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
            item = itemm;
            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].Service1 == item.GetType().GetProperty("Service1").GetValue(item))
                {
                    Service_Upd.Text = services[i].Service1;
                    Price_upd.Text = Convert.ToString(services[i].Price);
                }
            }
        }

        private void UpdateS_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Service_Upd.Text != "" && Price_upd.Text != "")
            {
                try
                {
                    for (int i = 0; i < services.Count; i++)
                    {
                        if (services[i].Service1 == item.GetType().GetProperty("Service1").GetValue(item))
                        {
                            services[i].Service1 = Service_Upd.Text;
                            services[i].Price = Convert.ToDouble(Price_upd.Text);
                            Entities.GetContext().SaveChanges();
                            frame1.Navigate(new glavn(user, frame1));
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте формат введенных данных");
                }
            }
        }

        private void DeleteS_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].Service1 == item.GetType().GetProperty("Service1").GetValue(item))
                {
                    Entities.GetContext().Service.Remove(services[i]);
                    Entities.GetContext().SaveChanges();
                    frame1.Navigate(new glavn(user, frame1));
                }
            }
        }

        private void Back_upd_s_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new glavn(user, frame1));
        }
    }
}
