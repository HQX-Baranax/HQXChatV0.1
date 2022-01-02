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
        public MySqlConnection baglan()
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=dbLogin;Uid=root;Pwd=baran2104;");
            

            baglanti.Open();
            MySqlConnection.ClearPool(baglanti);
            MySqlConnection.ClearAllPools();
            return (baglanti);
        }
    }
}
