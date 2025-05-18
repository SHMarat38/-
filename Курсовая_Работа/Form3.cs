using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography; 
namespace Курсовая_Работа
{
    public partial class Form3: Form
    {
        string connectionString = "server=localhost;user=root;database=mydb;password=3855403marat;";

        public Form3()
        {
            InitializeComponent();
        }
        int userId;
        String name;
        String email;
        String namepol;
        String pass;
        String pass2;
        String hashedpassword;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            name = textBox1.Text;
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
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            email = textBox2.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            namepol = textBox5.Text;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(name != null && email != null && namepol != null && pass != null && pass2 != null)
            {
                if(pass == pass2)
                {
                    using(MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string Check = "SELECT COUNT(*) FROM users WHERE niknaym = @niknaym";
                            using (MySqlCommand check = new MySqlCommand(Check, connection))
                            {
                                check.Parameters.AddWithValue("@niknaym", namepol);
                                int userCount = Convert.ToInt32(check.ExecuteScalar());
                       
                                if (userCount > 0 )
                                {
                                    MessageBox.Show("Такой пользователь уже есть ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return; 
                                }
                                
                            }
                            string Checkemail = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                            using (MySqlCommand check = new MySqlCommand(Checkemail, connection))
                            {
                                check.Parameters.AddWithValue("@Email", email);
                                int useremail = Convert.ToInt32(check.ExecuteScalar());

                                if (useremail > 0)
                                {
                                    MessageBox.Show("Эта почта уже используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                            }
                            string query = "INSERT INTO  users (Name, Email,Password, Role, niknaym) VALUES (@Name , @Email,@Password, @Role, @niknaym)";
                            using (MySqlCommand cm = new MySqlCommand(query, connection))
                            {


                                cm.Parameters.AddWithValue("@Name", name);
                                cm.Parameters.AddWithValue("@Email", email);
                                cm.Parameters.AddWithValue("@Password", hashedpassword);
                                cm.Parameters.AddWithValue("@Role", "User");
                                cm.Parameters.AddWithValue("@niknaym", namepol);

                                cm.ExecuteNonQuery();
                                MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                string zapros2 = "SELECT UsersID from users WHERE  niknaym = @niknaym AND Email = @email AND Password = @password";
                                    using (MySqlCommand zapr2 = new MySqlCommand(zapros2, connection))
                                {
                                    zapr2.Parameters.AddWithValue("@niknaym", namepol);
                                    zapr2.Parameters.AddWithValue("@email", email);
                                    zapr2.Parameters.AddWithValue("@password", hashedpassword);
                                    object result = zapr2.ExecuteScalar();
                                    if (result != null)
                                    {
                                        userId = Convert.ToInt32(result);
                                        this.Hide();
                                        Form6 form6 = new Form6(userId);
                                        form6.ShowDialog();
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
                    MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Вы не заполнили все поля !", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            pass = textBox4.Text;
            hashedpassword = HashPassword(pass);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            pass2 = textBox3.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
