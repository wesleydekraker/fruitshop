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
using FruitShopClient.FruitShopServiceReference;

namespace FruitShopClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static LoginDetails LoginDetails { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String username = textBoxName.Text;
            String password = passwordBox1.Password;

            var loginDetails = new LoginDetails()
            {
                Username = username,
                Password = password,
            };

            if (App.service.LoginCustomer(loginDetails))
            {
                LoginDetails = loginDetails;

                Store store = new Store();
                store.Show();
                Close();
            } else
            {
                MessageBox.Show("Wrong username or password!");
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }
    }
}
