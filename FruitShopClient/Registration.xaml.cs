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
using System.Windows.Shapes;
using FruitShopClient.FruitShopServiceReference;

namespace FruitShopClient
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxName.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string username = textBoxName.Text;
            string password = passwordBox1.Password;

            if (passwordBox1.Password.Length == 0)
            {
                errormessage.Text = "Enter password.";
                passwordBox1.Focus();
            }
            else if (passwordBoxConfirm.Password.Length == 0)
            {
                errormessage.Text = "Enter Confirm password.";
                passwordBoxConfirm.Focus();
            }
            else if (passwordBox1.Password != passwordBoxConfirm.Password)
            {
                errormessage.Text = "Confirm password must be same as password.";
                passwordBoxConfirm.Focus();
            }
            else
            {
                errormessage.Text = "";

                var loginDetails = new LoginDetails()
                {
                    Username = username,
                    Password = password
                };

                Message message = App.service.RegisterCustomer(loginDetails);
                MessageBox.Show(message.Content);
            }

        }
    }
}
