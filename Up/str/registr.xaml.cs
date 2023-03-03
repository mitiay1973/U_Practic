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
using System.Net;

namespace Up
{
    /// <summary>
    /// Логика взаимодействия для registr.xaml
    /// </summary>
    public partial class registr : Page
    {
        public Frame frame1;
        DateTime date;
        public registr(Frame frame, DateTime d)
        {
            InitializeComponent();
            frame1 = frame;
            date = d;
        }
        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new avtoriz(frame1,date));
        }

        private void registration_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pas = password.Text;
            string pas1 = password_Copy.Text;
            string names = name.Text;
            if (log != "")
            {
                if (pas != "")
                {
                    if (pas1 != "")
                    {
                        if (pas == pas1)
                        {
                            List<Up.users> user = new List<Up.users>() { new users() };
                            List<Up.history> h = new List<Up.history>() { new history() };
                            int count = Entities.GetContext().users.Count();
                            int count_h = Entities.GetContext().history.Count();
                            user[0].id = count + 1;
                            user[0].login = log;
                            user[0].ip= Dns.GetHostName();
                            user[0].password = pas;
                            user[0].name = names;
                            Entities.GetContext().users.Add(user[0]);
                            h[0].id=count_h+1;
                            h[0].login=log;
                            h[0].ip= Dns.GetHostName();
                            h[0].date=DateTime.Now;
                            h[0].block = DateTime.Now.AddMinutes(-30);
                            Entities.GetContext().history.Add(h[0]);
                            Entities.GetContext().SaveChanges();
                            frame1.Navigate(new avtoriz(frame1,date));
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Повторите пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }

        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
