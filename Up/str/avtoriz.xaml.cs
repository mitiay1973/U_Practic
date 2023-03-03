﻿using System;
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
    /// Логика взаимодействия для avtoriz.xaml
    /// </summary>
    public partial class avtoriz : Page
    {
        Frame frame1;
        DateTime date;
        public avtoriz(Frame frame, DateTime d)
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
        public int vx = 0;
        List<Up.users> Users = new List<Up.users>();
        List<Up.Workers> worker = new List<Up.Workers>();
        List<Up.history> history_login = new List<Up.history>{new history()};
        List<Up.history> historys = new List<Up.history>();
        private void Entre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vx = 0;
            string user = login.Text;
            string pas = password.Password;
            int count = Entities.GetContext().users.Count();
            int count_hh = Entities.GetContext().history.Count();
            int count_w = Entities.GetContext().Workers.Count();
            worker = Entities.GetContext().Workers.ToList();
            Users = Entities.GetContext().users.ToList();
            historys = Entities.GetContext().history.ToList();
            for (int i = 0; i < count_w; i++)
            {
                if (worker[i].login == user)
                {
                    if (worker[i].password == pas)
                    {
                        for (int j = count_hh-1; j >=0; j--)
                        {
                            if (historys[j].login == user)
                            {
                                DateTime t = DateTime.Now;
                                if (historys[j].block != null)
                                {
                                    t = (DateTime)historys[j].block;
                                    t = t.AddMinutes(30);
                                }
                                if (DateTime.Now < historys[j].block || t <= DateTime.Now)
                                {
                                    frame1.Navigate(new glavn(worker[i].login, frame1));
                                    vx = 1;
                                    int count_h = Entities.GetContext().history.Count();
                                    history_login[0].id = count_h + 1;
                                    history_login[0].login = user;
                                    history_login[0].date = DateTime.Now;
                                    history_login[0].ip = Dns.GetHostName();
                                    if (historys[j].block<DateTime.Now)
                                    {
                                        history_login[0].block = date.AddHours(2.5);
                                    }
                                    else
                                    {
                                        history_login[0].block = historys[j].block;
                                    }
                                    Entities.GetContext().history.Add(history_login[0]);
                                    Entities.GetContext().SaveChanges();
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Перерыв пол часа");
                                    vx = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
                for (int i = 0; i < count; i++)
                {
                    if (Users[i].login == user)
                    {
                        if (Users[i].password == pas)
                        {
                            for (int j = count_hh-1; j >= 0; j--)
                            {
                                if (historys[j].login == user)
                                {
                                DateTime t=DateTime.Now;
                                if (historys[j].block!=null)
                                {
                                    t = (DateTime)historys[j].block;
                                    t = t.AddMinutes(30);
                                }
                                    if (DateTime.Now < historys[j].block || t <= DateTime.Now)
                                    {
                                        frame1.Navigate(new glavn(Users[i].login, frame1));
                                        vx = 1;
                                        int count_h = Entities.GetContext().history.Count();
                                        history_login[0].id = count_h + 1;
                                        history_login[0].login = user;
                                        history_login[0].date = DateTime.Now;
                                        history_login[0].ip = Dns.GetHostName();
                                        if (historys[j].block < DateTime.Now)
                                        {
                                            history_login[0].block = date.AddHours(2.5);
                                        }
                                        else
                                        {
                                            history_login[0].block = historys[j].block;
                                        }
                                        Entities.GetContext().history.Add(history_login[0]);
                                        Entities.GetContext().SaveChanges();
                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Перерыв пол часа");
                                    vx = 1;
                                    break;
                                    }
                                }


                            }
                        }
                    }
                }
                if (vx == 0)
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        
        private void Reg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new registr(frame1,date));
        }
    }
}
