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

namespace Up
{
    /// <summary>
    /// Логика взаимодействия для add_service.xaml
    /// </summary>
    public partial class add_service : Page
    {
        Frame frame1;
        string user;
        public add_service(string User, Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
        }

        private void AddS_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(ServiceA.Text!="" && PriceA.Text!="")
            {
                try
                {
                    List<Service> services = new List<Service> { new Service() };
                    List<Service> services1 = new List<Service> { new Service() };
                    services1 = Entities.GetContext().Service.ToList();
                    services[0].Service1 = ServiceA.Text;
                    services[0].Price = Convert.ToDouble(PriceA.Text);
                    BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, Convert.ToString(services1.Last().id + 1));
                    var imageType = "Png";
                    // установить разрешение
                    generator.Parameters.Resolution = 400;
                    string imagePath = "barcode" + (services1.Last().id + 1) + ".Png";
                    string path = System.IO.Path.GetFullPath(imagePath);
                    // сгенерировать штрих-код          
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    services[0].barcode = path;
                    Entities.GetContext().Service.Add(services[0]);
                    Entities.GetContext().SaveChanges();
                    frame1.Navigate(new glavn(user,frame1,1));
                }
                catch(Exception)
                {
                    MessageBox.Show("Проверьте формат введенных данных");
                }
            }
        }

        private void back_add_s_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new glavn(user, frame1,1));
        }
    }
}
