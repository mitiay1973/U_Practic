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
    /// Логика взаимодействия для upd_result.xaml
    /// </summary>
    public partial class upd_result : Page
    {
        Frame frame1;
        string user;
        object item;
        List<Results> results = new List<Results> {};
        List<Results> r = new List<Results> { new Results() };
        public upd_result(string User, Frame frame, object itemm)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
            item = itemm;
            results = Entities.GetContext().Results.ToList();
            var allP = Entities.GetContext().users.ToList();
            allP.Insert(0, new users
            {
                name = "Пациент"
            });
            Patient_Upd.ItemsSource = allP;
            var allL = Entities.GetContext().Workers.ToList();
            allL.Insert(0, new Workers
            {
                name = "Лаборант"
            });
            Laborant_Upd.ItemsSource = allL;
            var allS = Entities.GetContext().Service.ToList();
            allS.Insert(0, new Service
            {
                Service1 = "Услуга"
            });
            Service_Upd.ItemsSource = allS;
            r[0] = (Results)item;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].id == r[0].id)
                {
                    Patient_Upd.SelectedIndex = results[i].id_user;
                    Laborant_Upd.SelectedIndex = (int)results[i].id_work;
                    Service_Upd.SelectedIndex = (int)results[i].id_service;
                    Result_Upd.Text = results[i].result;
                    Date_Upd.Text = Convert.ToString(results[i].date);
                }
            }
        }

        private void Update_R_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Patient_Upd.SelectedIndex != 0 && Laborant_Upd.SelectedIndex != 0 && Service_Upd.SelectedIndex != 0 && Result_Upd.Text != "" && Date_Upd.Text != null)
            {
                try
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].id == r[0].id)
                        {
                            results[i].id_user = Patient_Upd.SelectedIndex;
                            results[i].id_work = Laborant_Upd.SelectedIndex;
                            results[i].id_service = Service_Upd.SelectedIndex;
                            results[i].result = Result_Upd.Text;
                            results[i].date = Convert.ToDateTime(Date_Upd.Text);
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

        private void Delete_R_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].id == r[0].id)
                {
                    Entities.GetContext().Results.Remove(results[i]);
                    Entities.GetContext().SaveChanges();
                    frame1.Navigate(new glavn(user, frame1));
                }
            }
        }
    }
}
