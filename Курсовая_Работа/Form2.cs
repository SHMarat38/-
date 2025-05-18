using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Threading;

namespace Курсовая_Работа
{
    public partial class Form2: Form
    {
        int userId;
        string niknaym;
        string email;
        string password;
        string hashedpassword;
        public Form2()
        {
            InitializeComponent();
        }
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                // Преобразуем байты в строку
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2")); // x2 — 16-ричная запись
                }
                return builder.ToString();
            }
        }
        string connectionString = "server=localhost;user=root;database=mydb;password=3855403marat;";
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            niknaym = textBox3.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            email = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            password = textBox1.Text;
            hashedpassword = HashPassword(password);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(niknaym != null && email != null && password != null)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string zapros = "SELECT * FROM users WHERE niknaym = @niknaym AND Email = @email AND Password = @password";
                        using (MySqlCommand zapr = new MySqlCommand(zapros, connection))
                        {
                            zapr.Parameters.AddWithValue("@niknaym", niknaym);
                            zapr.Parameters.AddWithValue("@email", email);
                            zapr.Parameters.AddWithValue("@password", hashedpassword);

                            using (MySqlDataReader reader = zapr.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Close();
                                    string zapros2 = "SELECT UsersID from users WHERE  niknaym = @niknaym AND Email = @email AND Password = @password";
                                    using (MySqlCommand zapr2 = new MySqlCommand(zapros2, connection))
                                    {
                                        zapr2.Parameters.AddWithValue("@niknaym", niknaym);
                                        zapr2.Parameters.AddWithValue("@email", email);
                                        zapr2.Parameters.AddWithValue("@password", hashedpassword);
                                        object result = zapr2.ExecuteScalar();
                                        if(result != null)
                                        {
                                            userId = Convert.ToInt32(result);
                                            Thread.Sleep(1000);
                                            this.Hide();
                                            Form6 form6 = new Form6(userId);
                                            form6.ShowDialog();
                                        }
                                    }
                                    

                                }
                                else
                                {
                                    MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не заполнили все поля !", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
