using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CreateTables.xaml
    /// </summary>
    public partial class CreateTables : Window
    {
        public CreateTables()
        {
            InitializeComponent();
        }
        int i = 1;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            TextBox box = new TextBox();
            MainTexts.Children.Add(box);
            box.Width = 150;
            box.Height = 40;
            box.Margin = new Thickness(10, 30 * i, 10, 10);
            box.RenderSize = new Size(200,50);


            List<string> variation = new List<string>();
            variation.Add("int");
            variation.Add("string");
            variation.Add("flost");
            variation.Add("nstring");
            //variation.Add("");
            ComboBox combo = new ComboBox();
            MainTexts.Children.Add(combo);
            combo.Margin = new Thickness(100, 30 * i, 10, 10);
            combo.RenderSize = new Size(200, 50);
            combo.ItemsSource = variation;
            combo.Width = 150;
            combo.Height = 40;

            i++;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
