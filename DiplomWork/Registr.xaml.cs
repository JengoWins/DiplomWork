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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace DiplomWork
{
    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Window
    {
        public Registr()
        {
            InitializeComponent();
        }

        public bool check;

        public void SelectLog()
        {

            string conn = "server=localhost;database=diplomwork;user=root;password=root;SSL Mode=None";
            string login = TextLogin.Text.Trim();
            

            MySqlConnection connection = new MySqlConnection(conn);
            DataTable table = new DataTable();// наши таблица с данными
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            connection.Open();

            string MySql1 = "SELECT `login` FROM `user` WHERE `login`=@Log";

            MySqlCommand command1 = new MySqlCommand(MySql1, connection);

            command1.Parameters.Add("@Log", MySqlDbType.VarChar).Value = login;
            //string log = command1.ExecuteScalar().ToString();

            adapter.SelectCommand = command1;
            adapter.Fill(table);//записываем данные в таблицу

            if (table.Rows.Count > 0)
            {
                check = true;
            }else 
            {
                check = false;
            }
            connection.Close();
        }
       
        private void Reg_Activate(object sender, RoutedEventArgs e)
        {
            string name = TextName.Text.Trim();
            string login = TextLogin.Text.Trim();
            string password = TextPassword.Password.Trim();
            string password2 = TextPass2.Password.Trim();

         

            if (name == "" || login == "" || password == "" || password2 == "")
            {
                MessageBox.Show("Есть пустые поля","Сообщение",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                if (password.Length<5 || login.Length<5)
                {
                    MessageBox.Show("Логин и пароль должны быть как минимум состоять из 5-ти символов", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if(password!=password2){
                    MessageBox.Show("Пароли не совпадают", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SelectLog();
                    if (check)
                    {
                        MessageBox.Show("Данный пользователь с таким логином существует", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string conn = "server=localhost;user=root;database=diplomwork;password=root;SSL Mode=None";
                        MySqlConnection connection = new MySqlConnection(conn);

                        connection.Open();

                        string MySql2 = "INSERT INTO `user` (`fio`,`login`,`password`,`roles`) values (@Fio,@Login,@Password,'user')";

                        MySqlCommand command2 = new MySqlCommand(MySql2, connection);

                        command2.Parameters.Add("@Fio", MySqlDbType.VarChar).Value = name;
                        command2.Parameters.Add("@Login", MySqlDbType.VarChar).Value = login;
                        command2.Parameters.Add("@Password", MySqlDbType.VarChar).Value = password;

                        if (command2.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Запись успешно завершена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ой. Не получилось записать", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        connection.Close();
                    }

                }
     
            }
            
        }

        private void Open_AutoReg(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow autoreg = new MainWindow();
            autoreg.Show();
            Close();
        }

       
    }
}
