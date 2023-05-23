using Hunters.AddWindow;
using Hunters.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Hunters.Forms
{
    /// <summary>
    /// Логика взаимодействия для Animals_Window.xaml
    /// </summary>
    public partial class Animals_Window : Window
    {
        DataBaseConnect _dataBaseConnect = new DataBaseConnect();
        string textCommand = "SELECT * FROM [БД Охотники].[dbo].[Животные]";
        public Animals_Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vouchers_Window vouchers_Window = new Vouchers_Window();
            vouchers_Window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hunters_Window hunters_Window = new Hunters_Window();
            hunters_Window.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Data("");
        }

        public void Load_Data(string temp)
        {
            Animals_wrap.Children.Clear();
            SqlCommand command = new SqlCommand(textCommand + temp, _dataBaseConnect.getConnection());
            _dataBaseConnect.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Controls.Animals_Control animals = new Controls.Animals_Control();
                    animals.IDAnimals_label_lb.Content = reader[0];
                    animals.NameAnimal_label_lb.Content = reader[1];
                    animals.TypePermission_lb.Content = reader[2];
                    animals.DateStarAnimal_lb.Content = reader[3];
                    animals.DateFinishAnimal_lb.Content = reader[4];
                    animals.MaksDay_lb.Content = reader[5];
                    animals.PerDay_lb.Content = reader[6];
                    animals.PerSeason_lb.Content = reader[7];
                    animals.Collection_lb.Content = reader[8];
                    Animals_wrap.Children.Add(animals);
                }
            }
            _dataBaseConnect.closeConnection();
        }

        private void SearchNameAnimals_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@" where Наименование like '%{SearchNameAnimals_tb.Text}%'");
        }

        private void CreateAnimal_btn_Click(object sender, RoutedEventArgs e)
        {
            AddAnimal_Window addAnimal_Window = new AddAnimal_Window();
            addAnimal_Window.EditSaveAnimals_btn.Visibility = Visibility.Hidden;
            addAnimal_Window.AddNewAnimals_btn.Visibility = Visibility.Visible;
            addAnimal_Window.Show();
        }
    }
}
