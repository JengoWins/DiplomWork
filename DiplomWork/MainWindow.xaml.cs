using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static String login;
        bool check;

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            string logins = TextLogin.Text.Trim();
            string password = TextPassword.Password.Trim();

            if (logins == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (password.Length < 5 || logins.Length < 5)
                {
                    MessageBox.Show("Логин и пароль должны быть как минимум состоять из 5-ти символов", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    SelectLog();
                    if (check)
                    {
                        login = logins;
                        //MessageBox.Show("Это победа", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Hide();
                        MainPanel panel = new MainPanel();
                        panel.Show();
                        Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Это провал", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void Registr_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            Registr reg = new Registr();
            reg.Show();
            Close();
        }

        public void SelectLog()
        {

            string conn = "server=localhost;database=diplomwork;user=root;password=root;SSL Mode=None";
            string logins = TextLogin.Text.Trim();


            MySqlConnection connection = new MySqlConnection(conn);
            DataTable table = new DataTable();// наши таблица с данными
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            connection.Open();

            string MySql1 = "SELECT `login` FROM `user` WHERE `login`=@Log";

            MySqlCommand command1 = new MySqlCommand(MySql1, connection);

            command1.Parameters.Add("@Log", MySqlDbType.VarChar).Value = logins;
            //string log = command1.ExecuteScalar().ToString();

            adapter.SelectCommand = command1;
            adapter.Fill(table);//записываем данные в таблицу

            if (table.Rows.Count > 0)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            connection.Close();
        }

    }
}
