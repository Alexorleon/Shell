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
        //static DateTime current_time = DateTime.Now; // текущее время
        private readonly Timer tmrShow; // таймер для периодической проверки соединения

        // проверка интернет соединения
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        private bool isFirst = true; // чтобы лишний раз не дергать страницу

        string str_pathToPage106 = ("file:///" + Environment.CurrentDirectory.Replace(@"\", @"/") + "/files/106.html");
        string str_pathFromTxt = "";

        public Form1()
        {
            InitializeComponent();
            // на весь экран
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;

            Cursor.Hide();

            tmrShow = new Timer(); // создаем новый таймер
            tmrShow.Interval = 10000; // ставим интервал выполнения единственного события, через 5 секунд
            tmrShow.Tick += check_link; // создаем событие
            tmrShow.Enabled = true;// включаем таймер

            // заменяем пробелы в адресе до локальной страницы
            str_pathToPage106 = str_pathToPage106.Replace(@" ", @"%20");

            // считываем адрес из файла
            System.IO.StreamReader file = new System.IO.StreamReader(Environment.CurrentDirectory + "\\files\\address.txt");
            str_pathFromTxt = file.ReadLine();
            file.Close();
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
            //System.IO.StreamReader file = new System.IO.StreamReader(Environment.CurrentDirectory + "\\files\\address.txt");
            //string line = file.ReadLine();
            /*while ((line = file.ReadLine()) != null)
            {
            }*/

            //file.Close();
            webKitBrowser1.Navigate(str_pathFromTxt);
        }

        // проверяем сеть
        private void check_link(object sender, EventArgs e)
        {
            //bool status = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            //MessageBox.Show("status= " + status);

            int Desc;
            bool statusConnect = InternetGetConnectedState(out Desc, 0);

            // если нет связи, показываем нашу страницу
            if (!statusConnect)
            {
                if (isFirst) // если спрашиваем впервые
                {
                    isFirst = false;
                    webKitBrowser1.Navigate(str_pathToPage106);
                    //MessageBox.Show(statusConnect.ToString());
                }
            }
            else
            {
                if ( !isFirst)
                {
                    isFirst = true;
                    webKitBrowser1.Navigate(str_pathFromTxt);
                    //MessageBox.Show(statusConnect.ToString());
                }
            }
        }
    }
}
