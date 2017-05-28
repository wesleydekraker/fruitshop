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
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Window
    {
        private static List<ProductType> products = new List<ProductType>();
        
        public Store()
        {
            InitializeComponent();
            RefreshAll();
        }

        private void Click_Buy(object sender, RoutedEventArgs e)
        {
            ProductType selectedItem = null;

            try
            {
                selectedItem = (ProductType) ProductListBox.SelectedItem;
            }
            catch(NullReferenceException)
            {
                // Do nothing.
            }

            if (selectedItem != null)
            {
                var message = App.service.BuyProduct(Login.LoginDetails, selectedItem.Id);

                if (!message.IsError)
                {
                    RefreshAll();
                }

                MessageBox.Show(message.Content);
            } else
            {
                MessageBox.Show("Please select a product!");
            }
        }

        private void Click_Refresh(object sender, RoutedEventArgs e)
        {
            RefreshAll();
        }

        private void RefreshMyInventory()
        {
            var myInventory = App.service.GetBoughtProducts(Login.LoginDetails);

            string inventory = "";
            foreach (OwnedProductType ownedProduct in myInventory)
            {
                inventory += ownedProduct.ProductDetails.Name + " " + 
                    ownedProduct.Quantity + "\n";
            }

            Inventory.Text = inventory;
        }

        private void RefreshStock()
        {
            var availableProducts = App.service.GetAvailableProducts();

            ProductListBox.ItemsSource = availableProducts;

            ProductListBox.UpdateLayout();
            ProductListBox.Items.Refresh();
        }

        private void RefreshBalance()
        {
            Double balance = App.service.GetBalance(Login.LoginDetails);
            Balance.Text = "$" + balance + " USD";
        }

        private void RefreshAll()
        {
            RefreshMyInventory();
            RefreshStock();
            RefreshBalance();
        }
    }
}

