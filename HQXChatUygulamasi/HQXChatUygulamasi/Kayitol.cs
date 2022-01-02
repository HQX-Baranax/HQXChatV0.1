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
    public partial class Kayitol : Form
    {

        public Kayitol()
        {
            InitializeComponent();
        }
        DB bag = new DB();
        DataTable table = new DataTable();
        MySqlCommand kmt = new MySqlCommand(); 

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            kmt.Connection = bag.baglan();
            if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "" || guna2TextBox3.Text == "")
            {
                MessageBox.Show("Lütfen Kullancı Adı Veya Şifre Giriniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }
            if (guna2TextBox2.Text != guna2TextBox3.Text || guna2TextBox3.Text != guna2TextBox2.Text)
            {
                 MessageBox.Show("Şifreler Uyuşmuyor Lütfen Tekrar Deneyiniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }
            kmt.CommandText = ("INSERT INTO tblUser(usr, pwd) VALUES ('"+guna2TextBox1.Text+"', '"+guna2TextBox2.Text+"')");
            kmt.ExecuteNonQuery();
            MessageBox.Show("Kayıt İşlemi Başarılı");
            Giris giris = new Giris();
            this.Hide();
            giris.Show();


        }

        private void Kayitol_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }
    }
}
