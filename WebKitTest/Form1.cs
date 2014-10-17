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

using System.Net;
using System.IO;

namespace WebKitTest
{
    public partial class Form1 : Form
    {
        private readonly Timer tmrShow; // таймер для периодической проверки соединения

        // проверка интернет соединения
        //[DllImport("wininet.dll")]
        //private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        private bool isFirst = true; // чтобы лишний раз не дергать страницу

        string str_pathToPage106 = ("file:///" + Environment.CurrentDirectory.Replace(@"\", @"/") + "/files/106.html");
        string str_pathFromTxt = "";

        public Form1()
        {
            InitializeComponent();
            webKitBrowser1.IsScriptingEnabled = true;
            // на весь экран
            /*FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;

            Cursor.Hide();*/

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

            //int counter = 0;

            // Read the file and display it line by line.
            //System.IO.StreamReader file = new System.IO.StreamReader(Environment.CurrentDirectory + "\\files\\address.txt");
            //string line = file.ReadLine();
            /*while ((line = file.ReadLine()) != null)
            {
            }*/
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void webKitBrowser1_Load(object sender, EventArgs e)
        {
            // сразу переходим на страницу авторизации
            webKitBrowser1.Navigate(str_pathFromTxt);
        }

        // проверяем сеть
        private void check_link(object sender, EventArgs e)
        {
            // проверка интернет соединения
            /*int Desc;
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
                else
                {
                    MessageBox.Show(statusConnect.ToString());
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
            }*/

            //HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.1.80");
            
            //HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            //myHttpWebRequest.AllowAutoRedirect = false;
            //MessageBox.Show(myHttpWebRequest.HaveResponse.ToString());
            //HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();*/
            /*HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://google.com");
            request.AllowAutoRedirect = false;
            request.Method = "HEAD";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("true");
            }
            else
            {
                MessageBox.Show("false");
            }*/

            /*try
            {
                // Create a web request for an invalid site. Substitute the "invalid site" strong in the Create call with a invalid name.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.1.80");

                // Get the associated response for the above request.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                myHttpWebResponse.Close();
            }
            catch (WebException ex)
            {
                MessageBox.Show("This program is expected to throw WebException on successful run." +
                                    "\n\nException Message :" + ex.Message);
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    MessageBox.Show("Status Code: " + ((HttpWebResponse)ex.Response).StatusCode.ToString() + "Status Description: " + ((HttpWebResponse)ex.Response).StatusDescription.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/

            bool status = true;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(str_pathFromTxt);
                myHttpWebRequest.AllowAutoRedirect = false;
                myHttpWebRequest.Timeout = 5000;
                // Get the associated response for the above request.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                myHttpWebResponse.Close();
                status = true;
            }
            catch(WebException ex)
            {
                //MessageBox.Show(ex.Message);
                status = false;
            }

            if ( !status)
            {
                if (isFirst) // если спрашиваем впервые
                {
                    isFirst = false;
                    webKitBrowser1.Navigate(str_pathToPage106);
                    //MessageBox.Show(statusConnect.ToString());
                }
                /*else
                {
                    MessageBox.Show(statusConnect.ToString());
                }*/
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
