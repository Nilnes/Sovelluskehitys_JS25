using System.Text;
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
    public partial class MainWindow : Window
    {
        string path = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Omistaja\\Documents\\sovelluskehitys.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public MainWindow()
        {
            InitializeComponent();
            Update_DataGrid("SELECT * FROM products", "product", productlist);
            Update_ComboBox(this, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            string query = "INSERT INTO products (name, price, stock) VALUES ('"+Tex1.Text+"', "+Tex2.Text+","+Tex3.Text+");";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            connection.Close();

            Update_DataGrid("SELECT * FROM products","product", productlist);
            Update_ComboBox(sender, e);

            Tex1.Clear();
            Tex2.Clear();
            Tex3.Clear();
        }

        private void Update_DataGrid(string query, string table_name, DataGrid grid)
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("products");
            adapter.Fill(dataTable);

            grid.ItemsSource = dataTable.DefaultView;

            connection.Close();
        }

        // cb_products = ComboBox
        private void Update_ComboBox(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            string query = "SELECT * FROM products";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            // Datataulu comboboxin sisältöä varten
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(string));
            dataTable.Columns.Add("name", typeof(string));

            /* tehdään sidos että comboxissa näytetään datataulu */
            cb_products.ItemsSource = dataTable.DefaultView;
            cb_products.DisplayMemberPath = "name";
            cb_products.SelectedValuePath = "id";

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                dataTable.Rows.Add(id, name);
            }

            reader.Close();
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Update_DataGrid("SELECT * FROM products", "product", productlist);

        }

        private void delete_product_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            if (cb_products.SelectedValue == null)
            {
                MessageBox.Show("Valitse poistettava tuote.");
                return;
            }

            string id = cb_products.SelectedValue.ToString();
            MessageBox.Show("Poistettu tuote ID: " + id);

            string query = "DELETE FROM products WHERE id = " + id + ";";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();


            connection.Close();

            Update_DataGrid("SELECT * FROM products", "product", productlist);
            Update_ComboBox(sender, e);
        }
    }
}