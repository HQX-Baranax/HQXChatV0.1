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
        DB bag = new DB();//bağlantımız için classımızı oluşturuyoruz
        DataTable table = new DataTable();//bir adette datatable
        MySqlCommand kmt = new MySqlCommand();//işlemlerimiz içinde bir komut nesnesi    

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
            //komut textini girdik
            kmt.ExecuteNonQuery();
            //global olarak tanımladımız mysqlcommand kmt nesnesini çalıştırdık
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
