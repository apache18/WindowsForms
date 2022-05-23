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

namespace WindowsForms
{
    public partial class RegisterForm1 : Form
    {
        public RegisterForm1()
        {
            InitializeComponent();
            textName.Text = "Введите имя";
            textName.ForeColor = Color.Gray;

            textFamily.Text = "Введите Фамилию";
            textFamily.ForeColor = Color.Gray;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void textName_Enter(object sender, EventArgs e)
        {
            if (textName.Text == "Введите имя") 
            {
                textName.Text = "";
                textName.ForeColor = Color.Black;
            }
        }

        private void textName_Leave(object sender, EventArgs e)
        {
            if (textName.Text == "") 
            {
                textName.Text = "Введите имя";
                textName.ForeColor = Color.Gray;
            }
        }

        private void textFamily_Leave(object sender, EventArgs e)
        {
            if (textFamily.Text == "") 
            {
                textFamily.Text = "Введите Фамилию";
                textFamily.ForeColor = Color.Gray;
            }
        }

        private void textFamily_Enter(object sender, EventArgs e)
        {
            if (textFamily.Text == "Введите Фамилию") 
            {
                textFamily.Text = "";
                textFamily.ForeColor = Color.Gray;
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            // Чтобы удалить всех достаточно просто прописать DELETE FROM и указать таблицу
            MySqlCommand command = new MySqlCommand("DELETE FROM `users`", db.getConnection());
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (textName.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (textFamily.Text == "")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }

            Db db = new Db();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO users (login, password, name, family) VALUES (@login, @password, @name, @family)", db.getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textName.Text;
            command.Parameters.Add("@family", MySqlDbType.VarChar).Value = textFamily.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
            else
                MessageBox.Show("Аккаунт не был создан");

            db.closeConnection();

        }
    }
}
