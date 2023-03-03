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
    /// Логика взаимодействия для avtoriz.xaml
    /// </summary>
    public partial class avtoriz : Page
    {
        Frame frame1;
        public avtoriz(Frame frame)
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
        public int vx = 0;
        List<Up.users> Users = new List<Up.users>();
        List<Up.Workers> worker = new List<Up.Workers>();
        List<Up.history> history_login = new List<Up.history>{new history()};
        private void Entre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string user = login.Text;
            string pas = password.Password;
            int count = Entities.GetContext().users.Count();
            int count_w=Entities.GetContext().Workers.Count();
            worker = Entities.GetContext().Workers.ToList();
            Users = Entities.GetContext().users.ToList();
            for (int i = 0; i < count_w; i++)
            {
                if (worker[i].login == user)
                {
                    if (worker[i].password == pas)
                    {
                        frame1.Navigate(new glavn(worker[i].login, frame1));
                        vx = 1;
                        int count_h = Entities.GetContext().history.Count();
                        history_login[0].id = count_h + 1;
                        history_login[0].login = user;
                        history_login[0].date = DateTime.Now;
                        Entities.GetContext().history.Add(history_login[0]);
                        Entities.GetContext().SaveChanges();
                        break;
                    }
                }
            }
            for (int i = 0; i < count; i++)
            {
                if (Users[i].login == user)
                {
                    if (Users[i].password == pas)
                    {
                        frame1.Navigate(new glavn(Users[i].login, frame1));
                        vx = 1;
                        int count_h = Entities.GetContext().history.Count();
                        history_login[0].id = count_h + 1;
                        history_login[0].login = user;
                        history_login[0].date = DateTime.Now;
                        Entities.GetContext().history.Add(history_login[0]);
                        Entities.GetContext().SaveChanges();
                        break;
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

        }
    }
}
