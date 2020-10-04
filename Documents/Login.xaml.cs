using Documents.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Documents
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LogCon logcon;
        DocContext docCon;
        public Login()
        {
            InitializeComponent();

            docCon = new DocContext();
            docCon.LoginsBD.Load(); // загружаем данные входящих
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {

            var _login = docCon.LoginsBD.Where(a => a.Lg == LoginText.Text);
            if (_login != null)
            {
                var _loginList = _login.ToList();
                if (_loginList.Count !=0)
                {
                    LoginText.BorderBrush = Brushes.SlateBlue;
                    string hash = HashPassword(PasswordText.Password);
                    var _passList = _login.Where(p => p.Ps == hash).ToList();

                    if (_passList.Count != 0)
                    {
                        PasswordText.BorderBrush = Brushes.SlateBlue;
                        MainWindow mainWindow = new MainWindow(_passList);//
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        PasswordText.BorderBrush = Brushes.Red;
                    }
                }
                else
                {
                    LoginText.BorderBrush = Brushes.Red;
                }

            }

            

        }

        private string HashPassword(string str)
        {
            byte[] data = Encoding.ASCII.GetBytes(str);   
            byte[] result;

            SHA1 sha = new SHA1CryptoServiceProvider();
            
            string hash = string.Empty;
            result = sha.ComputeHash(data);
            foreach (byte b in result) hash += string.Format("{0:x2}", b);
            return hash;
        }


       
    }

}
