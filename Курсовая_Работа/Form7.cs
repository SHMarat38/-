using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Курсовая_Работа
{
    public partial class Form7: Form
    {
        int orderId;
        public string ProductName;
        public string ProductDescription;
        public int ProductPrice;
        public string ProductPrice1;
        public int ProductCategoryID;
        public string ProductCategoryID1;
        public int ProductDeleteId;
        public string UsersID;
        public int OrderID;
        public Form7()
        {
            InitializeComponent();
        }
        string connectionString = "server=localhost;user=root;database=mydb;password=3855403marat;";
        private void button6_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    String zapros = "SELECT * FROM products";
                    using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                    {
                        
                            MySqlDataAdapter adapter = new MySqlDataAdapter(zapr);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                        

                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки категорий: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProductName = textBox2.Text;
            ProductDescription = textBox3.Text;
            ProductPrice1 = textBox4.Text;
            ProductCategoryID1 = textBox5.Text;
            if (!string.IsNullOrWhiteSpace(ProductName) &&  !string.IsNullOrWhiteSpace(ProductDescription) && !string.IsNullOrWhiteSpace(ProductPrice1) && !string.IsNullOrWhiteSpace(ProductCategoryID1))
            {
                ProductPrice = Convert.ToInt32(ProductPrice1);
                ProductCategoryID = Convert.ToInt32(ProductCategoryID1);
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String zapros = "INSERT INTO products (Name, Description, Price, CategoryID) \r\nVALUES (@ProductName, @ProductDescription, @ProductPrice, @ProductCategoryID);\r\n";
                        using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                        {
                            zapr.Parameters.AddWithValue("@ProductName", ProductName);
                            zapr.Parameters.AddWithValue("@ProductDescription", ProductDescription);
                            zapr.Parameters.AddWithValue("@ProductPrice", ProductPrice);
                            zapr.Parameters.AddWithValue("@ProductCategoryID", ProductCategoryID);

                            zapr.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(" Ошибка добавления товара " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                textBox2.Text = null;
                textBox3.Text = null;
                textBox4.Text = null;
                textBox5.Text = null;
            }
            
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox6.Text))

            {
                ProductDeleteId = Convert.ToInt32(textBox6.Text);
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String zapros = "DELETE FROM products WHERE ProductID = @ProductDeleteId";
                        using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                        {
                            zapr.Parameters.AddWithValue("@ProductDeleteId", ProductDeleteId);
                            zapr.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(" Ошибка удаления товара " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                textBox6.Text = null;
            }
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                string UsersID = textBox1.Text;
                int orderId = 0;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();


                        string queryOrderId = "SELECT OrderID FROM orders WHERE UserID = @UserID";
                        using (MySqlCommand cmdOrderId = new MySqlCommand(queryOrderId, connection))
                        {
                            cmdOrderId.Parameters.AddWithValue("@UserID", UsersID);
                            object result = cmdOrderId.ExecuteScalar();

                            if (result != null)
                            {
                                orderId = Convert.ToInt32(result);


                                string deleteOrderItems = "DELETE FROM orderitems WHERE OrderID = @OrderID";
                                using (MySqlCommand cmdDeleteItems = new MySqlCommand(deleteOrderItems, connection))
                                {
                                    cmdDeleteItems.Parameters.AddWithValue("@OrderID", orderId);
                                    cmdDeleteItems.ExecuteNonQuery();
                                }

                                string deleteOrder = "DELETE FROM orders WHERE UserID = @UserID";
                                using (MySqlCommand cmdDeleteOrder = new MySqlCommand(deleteOrder, connection))
                                {
                                    cmdDeleteOrder.Parameters.AddWithValue("@UserID", UsersID);
                                    cmdDeleteOrder.ExecuteNonQuery();
                                }
                            }
                        }


                        string deleteUser = "DELETE FROM users WHERE UsersID = @UsersID";
                        using (MySqlCommand cmdDeleteUser = new MySqlCommand(deleteUser, connection))
                        {
                            cmdDeleteUser.Parameters.AddWithValue("@UsersID", UsersID);
                            cmdDeleteUser.ExecuteNonQuery();
                        }

                        MessageBox.Show("Удаление завершено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления пользователя: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    string zapros = "SELECT orders.OrderID, orders.OrderDate, orders.TotalPrice, orderitems.ProductID, orderitems.Quantity, orderitems.Price FROM orders INNER JOIN orderitems ON orders.OrderID = orderitems.OrderID;";
                    using (MySqlCommand cmd = new MySqlCommand(zapros, connection))
                    {


                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    string zapros = "SELECT * FROM users";
                    using (MySqlCommand cmd = new MySqlCommand(zapros, connection))
                    {


                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
