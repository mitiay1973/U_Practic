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
    /// Логика взаимодействия для Diagram.xaml
    /// </summary>
    public partial class Diagram : Window
    {
        public Diagram()
        {
            InitializeComponent();
        }
        private void ChartsButton_Click(object sender, RoutedEventArgs e)
        {
            // Удаляем прежний график.
            GridForChart.Children.OfType<Canvas>().ToList().ForEach(p => GridForChart.Children.Remove(p));

            Chart chart = null;

            Button button = sender as Button;

            // Создаём новый график выбранного вида.
            switch (button.Name)
            {
                case "BarsButton":
                    if ((chart is BarChart) == false)
                    {
                        chart = new BarChart();
                    }

                    break;
                case "LineButton":
                    if ((chart is LineChart) == false)
                    {
                        chart = new LineChart();
                    }

                    break;
                case "PieButton":
                    if ((chart is PieChart) == false)
                    {
                        chart = new PieChart();
                    }

                    break;
            }

            // Добавляем новую диаграмму на поле контейнера для графиков.
            GridForChart.Children.Add(chart.ChartBackground);

            // Принудительно обновляем размеры контейнера для графика.
            GridForChart.UpdateLayout();

            // Создаём график.
            CreateChart(chart);

        }

        private static void CreateChart(Chart chart)
        {
            chart.Clear();
            List<Results> results = new List<Results>();
            results = Entities.GetContext().Results.ToList();
            List<Workers> work = new List<Workers>();
            work = Entities.GetContext().Workers.ToList();
            int[] d = new int[work.Count];
            string[] dd = new string[work.Count];
            for (int i = 0; i < work.Count; i++)
            {
                d[i] = 0;
            }
            for (int i = 0; i < work.Count; i++)
            {
                for (int j = 0; j < results.Count;j++)
                {
                    if (work[i].name == results[j].Workers.name)
                    {
                        d[i] += 1;
                    }
                }
            }
            for (int i = 0; i < work.Count; i++)
            {
                dd[i]=work[i].name;
            }
            for (int i = 0; i < work.Count;i++)
            {
                chart.AddValue(d[i], dd[i]);
            }
        }
    }
}

