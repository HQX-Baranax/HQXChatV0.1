using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HQXChatUygulamasi
{
    public partial class DM : Form
    {
        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public DM()
        {
            InitializeComponent();
        }
        byte[] receivedBuf = new byte[1024];
        private void ReceiveData(IAsyncResult ar)
        {

            int listede_yok = 0;
            try
            {

                Socket socket = (Socket)ar.AsyncState;
                int received = socket.EndReceive(ar);
                byte[] dataBuf = new byte[received];
                Array.Copy(receivedBuf, dataBuf, received);
                string gelen = Encoding.ASCII.GetString(dataBuf).ToString();
                if (gelen.Contains("sil*"))
                {
                    string parcala = gelen.Substring(4, (gelen.Length - 4));
                    Console.WriteLine("degerim  " + parcala);
                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {
                        if (listBox1.Items[j].Equals(parcala))
                        {
                            listBox1.Items.RemoveAt(j);

                        }
                    }
                }
                else if (gelen.Contains("@"))
                {

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (listBox1.Items[i].ToString().Equals(gelen))
                        {
                            listede_yok = 1;
                        }
                    }
                    if (listede_yok == 0)
                    {
                        string ben = "@" + Giris.user;
                        if (ben.Equals(gelen))
                        {

                        }
                        else
                        {
                            listBox1.Items.Add(gelen);
                        }
                    }

                }
                else
                {
                    //label3.Text = (gelen);
                    richTextBox1.AppendText(gelen + "\n");
                }



                _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);


            }
            catch (Exception e)
            {
                Console.WriteLine("ReceiveData() metodunda hata " + e.Message);
            }

        }
        private void SendLoop()
        {
            while (true)
            {
                //string req = Console.ReadLine();
                //byte[] buffer = Encoding.ASCII.GetBytes(req);
                //_clientSocket.Send(buffer);

                byte[] receivedBuf = new byte[1024];
                int rev = _clientSocket.Receive(receivedBuf);
                if (rev != 0)
                {
                    byte[] data = new byte[rev];
                    Array.Copy(receivedBuf, data, rev);
                    // label3.Text = ("Received: " + Encoding.ASCII.GetString(data));
                    richTextBox1.AppendText("\nServer: " + Encoding.ASCII.GetString(data) + "\n");
                }
                else _clientSocket.Close();

            }
        }
        private void LoopConnect()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)//server çalışmıyorsa(çalışısaya kadar döngü döner)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect("127.0.0.1", 100);//127.0.0.1=IPAddress.Loopback demek 100 portuna bağlan
                }
                catch (SocketException)
                {
                    //   label3.Text = ("bağlantılar: " + attempts.ToString());
                    Console.WriteLine("bağlantılar: " + attempts.ToString());
                }
            }
            _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);//AsyncCallback thread gibi asenkron eş zamansız çalışıyor
            byte[] buffer = Encoding.ASCII.GetBytes("@@" + Giris.user);
            _clientSocket.Send(buffer);
            label1.Text = ("Servere bağlandı!");
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Secim secim = new Secim();
            this.Hide();
            secim.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            /*if (guna2TextBox1.Text != "")
            {
            listBox1.Items.Add(Giris.user+": "+guna2TextBox1.Text);
            guna2TextBox1.Text = "";
            }*/
            if (_clientSocket.Connected)
            {
                
                string tmpStr = "";
                foreach (var item in listBox1.SelectedItems)
                {

                    tmpStr = listBox1.GetItemText(item);
                    byte[] buffer = Encoding.ASCII.GetBytes(tmpStr + " :" + guna2TextBox1.Text + "*" + Giris.user);
                    _clientSocket.Send(buffer);
                    Thread.Sleep(20);

                }
                if (tmpStr.Equals(""))
                {
                    MessageBox.Show("lütfen listeden değer seçiniz");
                }
                else
                {
                    richTextBox1.AppendText(Giris.user + ": " + guna2TextBox1.Text + "\n");
                }
                guna2TextBox1.Text = "";

            }
        }

        private void Sunucu1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(LoopConnect);
            t1.Start();
        }

        private void Sunucu1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.Text = "DM | " + Giris.user;
        }
    }
}
