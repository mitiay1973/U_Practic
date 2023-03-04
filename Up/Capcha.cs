using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice
{
    public partial class Capcha : Form
    {
        
        private string text = String.Empty;
        public Capcha()
        {
            InitializeComponent();
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }
    
        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();
       
            Bitmap result = new Bitmap(Width, Height);   //Создадим изображение
        
            int Xpos = rnd.Next(0, Width - 125);   //Вычислим позицию текста
            int Ypos = rnd.Next(15, Height - 25);
         
            Brush[] colors = { Brushes.Black,   //Добавим различные цвета
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };
       
            Graphics g = Graphics.FromImage((Image)result);    //Укажем где рисовать
           
            g.Clear(Color.Gray); //Пусть фон картинки будет серым
    
            text = String.Empty;   //Сгенерируем текст
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];
       
            g.DrawString(text,    //Нарисуем сгенирируемый текст
                         new Font("Arial", 25),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black, 
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);
          
            return result;
        }


        public int pr=0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == this.text)
            {
                MessageBox.Show("Верно!");
                pr++;
                this.Close();
            }       
            else
                MessageBox.Show("Ошибка!");
           
        }
    }
}
