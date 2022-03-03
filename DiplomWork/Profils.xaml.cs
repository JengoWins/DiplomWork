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
using System.Windows.Shapes;

namespace DiplomWork
{
    /// <summary>
    /// Логика взаимодействия для Profils.xaml
    /// </summary>
    public partial class Profils : Window
    {
        public Profils()
        {
            InitializeComponent();
            panel();
        }
        //Загрузка Профиля пользователя/админа
        public void Loaded_User()
        {
            string conn = "server=localhost;database=diplomwork;user=root;password=root;SSL Mode=None";
            string logins = MainWindow.login;
            //fio.Text = logins;

            DataTable data = new DataTable("user");
            MySqlConnection connection = new MySqlConnection(conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            connection.Open();

            string MySql1 = "SELECT `login`,`fio`,`roles` FROM `user` WHERE `login`= @Log";

            MySqlCommand command1 = new MySqlCommand(MySql1, connection);
            
            command1.Parameters.Add("@Log", MySqlDbType.VarChar).Value = logins;
            //command1.ExecuteNonQuery();
            adapter.SelectCommand = command1;
            adapter.Fill(data);
            //data.Columns();
            //List<string> list = new List<string>();
            MySqlDataReader read = command1.ExecuteReader();

            if (read.HasRows)
            {
                while (read.Read()) 
                {
                    login.Text = read.GetString(0);
                    fio.Text = read.GetString(1);
                    role.Text = read.GetString(2);
                }    
                read.Close();
            }
            else
            {
                MessageBox.Show("Это провал", "Сообщение");
            }

            connection.Close();

        }
        //Загрузка списка всех пользователей
        private void panel()
        {

            string conn = "server=localhost;database=diplomwork;user=root;password=root;SSL Mode=None";


            MySqlConnection connection = new MySqlConnection(conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable("material");
            connection.Open();

            string MySql1 = "SELECT `login`,`fio`,`roles` FROM `user`"; 

            MySqlCommand command = new MySqlCommand(MySql1, connection);

            //command.ExecuteNonQuery();
            adapter.SelectCommand = command;
            DataSet setting = new DataSet();
            adapter.Fill(setting, "User");

            int CountList = setting.Tables["User"].Rows.Count; //Кол-во строк в табл из бд
            int count = 0;

            List<Users> ListMaterials = new List<Users>();
            while (CountList > count)
            {
                ListMaterials.Add(new Users()
                {
                    login = setting.Tables["User"].Rows[count]["login"].ToString(),
                    fio = setting.Tables["User"].Rows[count]["fio"].ToString(),
                    roles = setting.Tables["User"].Rows[count]["roles"].ToString()
                });

                count++;
            }
            //Main Grid

            List.ItemsSource = ListMaterials;
            //adapter.Update(table);
            connection.Close();

        }
        //При запуске окна профиля
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded_User();
        }
        //Главное меню
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainPanel main = new MainPanel();
            main.Show();
            Close();
        }
    }
}
