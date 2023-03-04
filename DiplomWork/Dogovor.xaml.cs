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
    /// Логика взаимодействия для Dogovor.xaml
    /// </summary>
    public partial class Dogovor : Window
    {
        public Dogovor()
        {
            InitializeComponent();
            panel1();
        }

        string add1; //Данные из выпадающего списка
        string add2; //Скидка, указанная пользователем
        string summ; //Сумма оплаты
        
        //Загружает ListView данные выбранной мебели
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            add1 = ComboBox.Text;
                
                //Проверка Скидки на ввод число 
                int b1 = int.Parse(Skid.Text) / 1;
                if (b1 == Convert.ToInt32(Skid.Text))
                {
                    add2 = Skid.Text;
                    //add2 = "0";
                }
                else
                {
                    add2 = "0";
                }
                panel();
           
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Вставляет в выпадающий список всю мебель из бд для выборки
        private void panel1()
        {
            string conn = "server=localhost;database=diplomwork;user=root;password=root;SSL Mode=None";


            MySqlConnection connection = new MySqlConnection(conn);
            //MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable("material");
            connection.Open();

            string MySql1 = "SELECT `name_mebel` FROM `mebel`";

            MySqlCommand command = new MySqlCommand(MySql1, connection);

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {

                string result = reader.GetString(0);
                ComboBox.Items.Add(result);
                ComboBox.Text = result;

            }
            connection.Close();
        }

        //Выводит на ListView данные выбранной мебели из бд  Проблема заключается в этом части кода <---
        private void panel()
        {

            string conn = "server=localhost;database=diplomwork;user=root;password=root;SSL Mode=None";

            MySqlConnection connection = new MySqlConnection(conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable("material");
            connection.Open();

            string MySql1 = "SELECT `articul`,`name_mebel`,`price`,`count` FROM `mebel` where `name_mebel`=@Dor";

            MySqlCommand command = new MySqlCommand(MySql1, connection);
            command.Parameters.Add("@Dor", MySqlDbType.VarChar).Value = add1;
            //command.ExecuteNonQuery();
            adapter.SelectCommand = command;
            DataSet setting = new DataSet();
            adapter.Fill(setting, "Tovar");

            int CountList = setting.Tables["Tovar"].Rows.Count; //Кол-во строк в табл из бд
            int count = 0;
            List<Tovar> ListMaterials = new List<Tovar>(); 
            int sum = 0;
            while (CountList > count)
            {
                ListMaterials.Add(new Tovar()
                {
                    num = count.ToString(),
                    articul = setting.Tables["Tovar"].Rows[count]["articul"].ToString(),
                    tovar = setting.Tables["Tovar"].Rows[count]["name_mebel"].ToString(),
                    price = setting.Tables["Tovar"].Rows[count]["price"].ToString(),
                    count = setting.Tables["Tovar"].Rows[count]["count"].ToString(),
                    skid = add2
                });
                sum += (int)setting.Tables["Tovar"].Rows[count]["price"];
                count++;
                
            }
            

            //Main Grid
            List.ItemsSource = ListMaterials;
            List2.ItemsSource = ListMaterials;
            summ = sum.ToString();
            //adapter.Update(table);
            connection.Close();
            

        }
        //Переход в Предпросмотр
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Img_Text_Main.Content = N.Text;
            Postav.Content = NameCop.Text+" "+INN.Text+" "+KPP.Text + " "+City.Text + " " + street.Text + " ";
            Zakaz.Content = FIO_Z.Text + " ";

            Value.Content = summ;
            Valuess.Content = summ;

            Canva.Visibility = Visibility.Hidden;
            Canva1.Visibility = Visibility.Visible;
        }
        //Переход в режим редактирования
        private void Button_Show(object sender, RoutedEventArgs e)
        {
            Canva1.Visibility = Visibility.Hidden;
            Canva.Visibility = Visibility.Visible;
        }
        //Печать чека
        private void Button_Print(object sender, RoutedEventArgs e)
        {
            PrintDialog PD = new PrintDialog();

            if (PD.ShowDialog() == true)
            {
                PD.PrintVisual(Flore, "Печать");
                Print.Visibility = Visibility.Hidden;
                Edit.Visibility = Visibility.Hidden;
            }
            else
            {
                Print.Visibility = Visibility.Visible;
                Edit.Visibility = Visibility.Visible;
            }
        }
        //Переход в главное меню
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hide();
            MainPanel main = new MainPanel();
            main.Show();
            Close();
        }
    }
}
