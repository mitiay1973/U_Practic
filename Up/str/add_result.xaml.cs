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

namespace Up
{
    /// <summary>
    /// Логика взаимодействия для add_result.xaml
    /// </summary>
    public partial class add_result : Page
    {
        Frame frame1;
        string user;
        public add_result(string User, Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
        }
    }
}
