using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HQXChatUygulamasi
{
    public partial class Giris : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader dr;
        public Giris()
        {
            InitializeComponent();
            con = new MySqlConnection("Server=localhost;Database=dbLogib;user=root;Pwd=12345678;SslMode=none");
        }
        public static string user;
        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            user = guna2TextBox1.Text;
            string pass = guna2TextBox2.Text;
            if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
            {
                MessageBox.Show("Lütfen Kullancı Adı Veya Şifre Giriniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tblUser where usr='" + guna2TextBox1.Text + "' AND pwd='" + guna2TextBox2.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Secim s = new Secim();
                this.Hide();
                s.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre Girdiniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();



            /*if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
            {
                MessageBox.Show("Lütfen Kullancı Adı Veya Şifre Giriniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
            
            Secim s = new Secim();
            user = guna2TextBox1.Text;
            this.Hide();
            s.Show();
            
            }*/

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }
    }
}
