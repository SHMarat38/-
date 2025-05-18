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
namespace Курсовая_Работа
{
    public partial class Form5 : Form
    {
        string login;
        string password;
        public Form5()
        {
            InitializeComponent();
        }
        string connectionString = "server=localhost;user=root;database=mydb;password=3855403marat;";
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            login = textBox1.Text;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (login != null && password != null)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String zapr = "SELECT Role FROM users WHERE niknaym = @login AND Password = @password";
                        using(MySqlCommand zap = new MySqlCommand(zapr, connection))
                        {
                            zap.Parameters.AddWithValue("@login", login);
                            zap.Parameters.AddWithValue("@password", password);

                            var role = zap.ExecuteScalar();

                            if(role != null && role.ToString() == "Admin")
                            {
                                this.Hide();
                                Form7 form7 = new Form7();
                                form7.ShowDialog();
                                
                                
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
