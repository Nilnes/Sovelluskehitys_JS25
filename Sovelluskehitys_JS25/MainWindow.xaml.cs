﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sovelluskehitys_JS25
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tekstikentta_1.Text = "Klikattu";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string path = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Omistaja\\Documents\\sovelluskehitys.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            string query = "SELECT * FROM products";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("products");
            adapter.Fill(dataTable);

            productlist.ItemsSource = dataTable.DefaultView;

            connection.Close();


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}