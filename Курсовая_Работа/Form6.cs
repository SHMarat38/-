using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
namespace Курсовая_Работа
{
    public partial class Form6: Form
    {
        int id;
        long newOrderId;
        int productId;
        string name;
        string description;
        string price;
        private int _userId;

        private void LoadCategories()
        {
            listBox1.Items.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string zapros = "SELECT name FROM categories";
                    using (MySqlCommand command = new MySqlCommand(zapros, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listBox1.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string connectionString = "server=localhost;user=root;database=mydb;password=3855403marat;";
        public Form6(int userId)
        {
            InitializeComponent();
             _userId = userId;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            LoadCategories();
            label3.Text = null;
            label3.Text = "Выберите Товар :)";
            using (MySqlConnection connection1 = new MySqlConnection(connectionString))
            {
                string query = @"
SELECT 
    Products.Name AS ProductName,
    Products.Description,
    Products.Price
FROM 
    Users
JOIN Orders ON Users.UsersID = Orders.UserID
JOIN OrderItems ON Orders.OrderID = OrderItems.OrderID
JOIN Products ON OrderItems.ProductID = Products.ProductID
WHERE 
    Users.UsersID = @userId;
";  
                connection1.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection1))
                {
                    cmd.Parameters.AddWithValue("@userId", _userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString("ProductName");
                            string desc = reader.GetString("Description");
                            int price = reader.GetInt32("Price");
                            label3.Text = null;
                            label3.Text = ($"Товар: {name}, Описание: {desc}, Цена: {price}");
                        }
                    }
                }
            }

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString() == "Ноутбуки")
            {
                using(MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String zapros = "SELECT CategoryID FROM categories WHERE Name = 'Ноутбуки'";
                        using(MySqlCommand zapr = new MySqlCommand(zapros, connection))
                        {
                             id = Convert.ToInt32(zapr.ExecuteScalar());
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки категорий: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Name, Description, Price FROM products WHERE CategoryID = @CategoryID";

                   
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@CategoryID", id);

                 
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                   
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }

            }
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString() == "Смартфоны")
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String zapros = "SELECT CategoryID FROM categories WHERE Name = 'Смартфоны'";
                        using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                        {
                            id = Convert.ToInt32(zapr.ExecuteScalar());

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки категорий: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Name, Description, Price FROM products WHERE CategoryID = @CategoryID";


                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@CategoryID", id);


                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);


                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }

            }
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString() == "ПК")
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String zapros = "SELECT CategoryID FROM categories WHERE Name = 'ПК'";
                        using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                        {
                            id = Convert.ToInt32(zapr.ExecuteScalar());

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки категорий: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Name, Description, Price FROM products WHERE CategoryID = @CategoryID";


                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@CategoryID", id);


                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);


                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }

            }
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString() == "Планшеты")
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String zapros = "SELECT CategoryID FROM categories WHERE Name = 'Планшеты'";
                        using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                        {
                            id = Convert.ToInt32(zapr.ExecuteScalar());

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки категорий: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Name, Description, Price FROM products WHERE CategoryID = @CategoryID";


                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@CategoryID", id);


                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);


                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                name = row.Cells[0].Value.ToString();
                description = row.Cells[1].Value.ToString();
                price = row.Cells[2].Value.ToString();
                MessageBox.Show($"{name} {description} {price}");
                //MessageBox.Show($"{_userId}");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string z = "SELECT ProductID FROM Products WHERE Name = @name AND CategoryID = @catId";
                        using (MySqlCommand zapr1 = new MySqlCommand(z, connection))
                        {
                            zapr1.Parameters.AddWithValue("@name", name);
                            zapr1.Parameters.AddWithValue("@catId", id);
                            object result = zapr1.ExecuteScalar();
                            if (result != null)
                            {
                                productId = Convert.ToInt32(result);
                            }
                        }
                        string zaps = "SELECT 1 FROM orders WHERE UserID = @userid";
                        using (MySqlCommand h = new MySqlCommand(zaps, connection))
                        {
                            h.Parameters.AddWithValue("@userid", _userId); 
                            object result = h.ExecuteScalar(); 

                            bool orderExists = result != null;

                            if (orderExists)
                            {
                                MessageBox.Show("Уже есть заказ");
                            }
                            else
                            {
                                // Добавляем в Orders
                                string zapros = "INSERT INTO Orders (UserID, OrderDate, TotalPrice) VALUES (@userid, CURDATE(), @totalprice);";
                                using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                                {
                                    zapr.Parameters.AddWithValue("@userid", _userId);
                                    zapr.Parameters.AddWithValue("@totalprice", Convert.ToDecimal(price));
                                    zapr.ExecuteNonQuery();
                                    newOrderId = zapr.LastInsertedId;
                                    // Добавляем в OrderItems
                                string zapros2 = "INSERT INTO OrderItems (OrderID , ProductID, Quantity, Price) VALUES (@OrderID , @productid, @quantity, @price)";
                                using (MySqlCommand zap = new MySqlCommand(zapros2, connection))
                                {
                                        zap.Parameters.AddWithValue("@OrderID", newOrderId);
                                        zap.Parameters.AddWithValue("@productid", productId);
                                        zap.Parameters.AddWithValue("@quantity", 1);
                                        zap.Parameters.AddWithValue("@price", Convert.ToDecimal(price));
                                        zap.ExecuteNonQuery();
                                }
                                }
                                string query = @"
SELECT 
    Products.Name AS ProductName,
    Products.Description,
    Products.Price
FROM 
    Users
JOIN Orders ON Users.UsersID = Orders.UserID
JOIN OrderItems ON Orders.OrderID = OrderItems.OrderID
JOIN Products ON OrderItems.ProductID = Products.ProductID
WHERE 
    Users.UsersID = @userId;
";

                                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@userId", _userId);

                                    using (var reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            string name = reader.GetString("ProductName");
                                            string desc = reader.GetString("Description");
                                            int price = reader.GetInt32("Price");
                                            label3.Text = null;
                                            label3.Text = ($"Товар: {name}, Описание: {desc}, Цена: {price}");
                                        }
                                    }
                                }


                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки категорий: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string q = "DELETE FROM OrderItems WHERE OrderID IN (SELECT OrderID FROM Orders WHERE UserID = @userId)";
                using (MySqlCommand cmd = new MySqlCommand(q, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", _userId);
                    cmd.ExecuteNonQuery();
                }
                string q1 = "DELETE FROM Orders WHERE UserID = @userId";
                using (MySqlCommand cmd = new MySqlCommand(q1, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", _userId);
                    cmd.ExecuteNonQuery();
                }
                label3.Text = null;
                label3.Text = "Выберите Товар :)";
            }
        }
    }
}
