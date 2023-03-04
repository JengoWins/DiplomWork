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
    /// Логика взаимодействия для MainPanel.xaml
    /// </summary>
    /// Оп-ха-ха

    public partial class MainPanel : Window
    {
        MySqlDataAdapter adapter;
        DataTable table;
        DataGrid Data;
        
        public MainPanel()
        {
            InitializeComponent();
            LoadUser();
        }

        private void LoadUser()
        {
            UserProfil.Text = MainWindow.login;
        }

        //public static string userpassword;
        private void UserPanel(object sender, RoutedEventArgs e)
        {
            string name = "user";
            Load_DataBases(name, Users.Header.ToString());
        }

        private void MebelPanel(object sender, RoutedEventArgs e)
        {
            string name = "mebel";
            Load_DataBases(name, Mebel.Header.ToString());
        }

        private void Arg_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Profils profil = new Profils();
            profil.Show();
            Close();
        }

        private void Load_DataBases(string tables,string bases)
        {
            string MySql1 = "";

            StackPanel Panel = new StackPanel(); //Создание панельки
            ScrollViewer Scrool = new ScrollViewer(); //создание прокрутки
            Scrool.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto; //Видимость

            Data = new DataGrid(); //Создание таблички с параметрами
            Data.Width = 1046;
            Data.Height = 694;
            Data.AutoGenerateColumns = true;

            string conn = "server=localhost;database=diplomwork;user=root;password=root;SSL Mode=None"; //строка подключения

            MySqlConnection connection = new MySqlConnection(conn); //подключение
            
            table = new DataTable("user");// наши таблица с данными
            connection.Open();

            switch (tables)
            {
                case "user":
                        MySql1 = "SELECT * FROM " + tables;
                    break;
                case "mebel":
                        MySql1 = "SELECT `id`, `articul`, `name_mebel`, `class`, `type`, `length`, `width`, `height`, `color`, `weight`, `Obivka`, `price`, `count` FROM " + tables;
                    break;
                default:
                    MessageBox.Show("Таблица отсутствует");
                    break;
            }


            adapter = new MySqlDataAdapter(MySql1, connection);

            MySqlCommandBuilder myCommandBuilder = new MySqlCommandBuilder(adapter); //Возможность работы с данной таблицей
            adapter.InsertCommand = myCommandBuilder.GetInsertCommand();
            adapter.UpdateCommand = myCommandBuilder.GetUpdateCommand();
            adapter.DeleteCommand = myCommandBuilder.GetDeleteCommand();
            MySqlCommand command1 = new MySqlCommand(MySql1, connection);
            command1.ExecuteNonQuery();
            adapter.SelectCommand = command1;
            adapter.Fill(table);
            Data.ItemsSource = table.DefaultView;

            adapter.Update(table);


            connection.Close();

            Panel.Children.Add(Data);
            Scrool.Content = Panel;
            TabControls.Items.Add(new TabItem
            {
                Name = "Ler",
                Header = new TextBlock { Text = bases }, // установка заголовка вкладки
                Content = Panel,
            });

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CreateTables create1 = new CreateTables();
            create1.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = adapter.Update(table);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            table.DefaultView.RowFilter = $"fio LIKE '%{TextLike.Text}%'";
        }

       

        private void Dok_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Dogovor profil = new Dogovor();
            profil.Show();
            Close();
        }

        private void Exit_Panel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    }
