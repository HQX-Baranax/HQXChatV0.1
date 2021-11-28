using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HQXChatUygulamasi
{
    class DB
    {
        public MySqlConnection baglan() // class içinde baglan adında fonksiyon oluşturduk
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=dbLogib;Uid=root;Pwd=12345678;");
            //bağlantı yolunu verdik

            baglanti.Open();//bağlantıyı açtık
            MySqlConnection.ClearPool(baglanti);
            MySqlConnection.ClearAllPools();//bundan önceki bağlantıları temizledik
            return (baglanti);//çağırıldığı yere bağlantıyı yolladık
        }
    }
}
