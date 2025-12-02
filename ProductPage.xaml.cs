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

namespace salmanova41
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            var currentProduct = Salmanova41Entities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProduct;
            ComboFilter.SelectedIndex = 0;
            UpdateProducts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void UpdateProducts()
        {
            var currentProduct = Salmanova41Entities.GetContext().Product.ToList();

            if (ComboFilter.SelectedIndex == 0)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 0)).ToList();
            }
            if (ComboFilter.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 0 && Convert.ToInt32(p.ProductDiscountAmount) <= 9.99)).ToList();
            }
            if (ComboFilter.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 10 && Convert.ToInt32(p.ProductDiscountAmount) <= 14.99)).ToList();
            }
            if (ComboFilter.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 15)).ToList();
            }

            //поиск
            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            //сорт
            if (RButtonDown.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }
            else if (RButtonUp.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            else
            {
                ProductListView.ItemsSource = currentProduct.ToList();
            }

            //!!!
            int ProductcountRecords = currentProduct.Count; 
            TBProductCountRecords.Text = ProductcountRecords.ToString();

            //всего кол-во
            int totalCount = Salmanova41Entities.GetContext().Product.Count();
            TBProductCountMaxRecords.Text = totalCount.ToString();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }
    }
}