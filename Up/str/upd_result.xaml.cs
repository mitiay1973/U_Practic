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
        public upd_result(string User, Frame frame, object itemm)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
            item = itemm;
        }
    }
}
