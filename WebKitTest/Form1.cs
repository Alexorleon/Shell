using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.InteropServices;

namespace WebKitTest
{
    public partial class Form1 : Form
    {
        static DateTime current_time = DateTime.Now; // текущее время
        private readonly Timer tmrShow; // таймер для периодической проверки соединения

        // проверка интернет соединения
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        private bool isFirst = true; // чтобы лишний раз не дергать страницу

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;

            Cursor.Hide();

            tmrShow = new Timer(); // создаем новый таймер
            tmrShow.Interval = 5000; // ставим интервал выполнения единственного события, через 5 секунд
            tmrShow.Tick += check_link; // создаем событие
            tmrShow.Enabled = true;// включаем таймер
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (e.KeyChar == '\r')
            {
                webKitBrowser1.Navigate(txtBox.Text);
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void webKitBrowser1_Load(object sender, EventArgs e)
        {
            //int counter = 0;
            
            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(Environment.CurrentDirectory + "\\files\\address.txt");
            string line = file.ReadLine();
            /*while ((line = file.ReadLine()) != null)
            {
            }*/

            file.Close();
            webKitBrowser1.Navigate(line);
        }

        // проверяем сеть
        private void check_link(object sender, EventArgs e)
        {
            //bool status = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            //MessageBox.Show("status= " + status);

            int Desc;
            bool statusConnect = InternetGetConnectedState(out Desc, 0);
            //MessageBox.Show(statusConnect.ToString());
            
            // если нет связи, показываем нашу страницу
            if (!statusConnect)
            {
                if (isFirst)
                {
                    string strpath = ("file:///" + Environment.CurrentDirectory.Replace(@"\", @"/") + "/files/106.html");
                    webKitBrowser1.Navigate(strpath.Replace(@" ", @"%20"));
                    isFirst = false;
                }
            }
            else
            {
                if ( !isFirst)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(Environment.CurrentDirectory + "\\files\\address.txt");
                    string line = file.ReadLine();

                    file.Close();
                    webKitBrowser1.Navigate(line);
                    isFirst = true;
                }
            }
        }
    }
}
