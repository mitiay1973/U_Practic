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
        public registr(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
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
            frame1.Navigate(new avtoriz(frame1));
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
                            int count = Entities.GetContext().users.Count();
                            user[0].id = count + 1;
                            user[0].login = log;
                            user[0].ip= Dns.GetHostName();
                            user[0].password = pas;
                            user[0].name = names;
                            Entities.GetContext().users.Add(user[0]);
                            Entities.GetContext().SaveChanges();
                            frame1.Navigate(new avtoriz(frame1));
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
