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
using System.Windows.Shapes;

namespace Up
{
    /// <summary>
    /// Логика взаимодействия для History_vxod.xaml
    /// </summary>
    public partial class History_vxod : Window
    {
        public History_vxod()
        {
            InitializeComponent();
            List<Up.history> historys = new List<Up.history>();
            historys = Entities.GetContext().history.ToList();
            Dgrid.ItemsSource = historys;
        }
    }
}
