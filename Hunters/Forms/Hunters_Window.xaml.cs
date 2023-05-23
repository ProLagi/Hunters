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
    /// Логика взаимодействия для Hunters_Window.xaml
    /// </summary>
    public partial class Hunters_Window : Window
    {
        DataBaseConnect _dataBaseConnect = new DataBaseConnect();
        string textCommand = "SELECT * FROM [БД Охотники].[dbo].[Охотники]";
        public Hunters_Window()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Data(textCommand);
        }
        public void Load_Data(string temp)
        {
            Hunters_wrap.Children.Clear();
            SqlCommand command = new SqlCommand(temp, _dataBaseConnect.getConnection());
            _dataBaseConnect.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Controls.Hunters_Control hunt = new Controls.Hunters_Control();
                    hunt.IDHunter_lb.Content = reader[0];
                    hunt.FullNameHunter_lb.Content = reader[1];
                    hunt.NumberChlenBilet_lb.Content = reader[2];
                    hunt.SeriesChlenBilet_lb.Content = reader[3];
                    hunt.SeriesBilet_lb.Content = reader[4];
                    hunt.NumberBilet_lb.Content = reader[5];
                    hunt.DateIssue_lb.Content = reader[6];
                    hunt.NPP_lb.Content = reader[7];
                    hunt.NumberPhone_lb.Content = reader[8];
                    hunt.DateBirth_lb.Content = reader[9];
                    hunt.INN_lb.Content = reader[10];
                    hunt.DateEntry_lb.Content = reader[11];
                    hunt.DeductionType_lb.Content = reader[12];
                    hunt.City_lb.Content = reader[13];
                    hunt.Street_lb.Content = reader[14];
                    hunt.Home_lb.Content = reader[15];
                    hunt.Entrance_lb.Content = reader[16];
                    hunt.Apartment_lb.Content = reader[17];
                    hunt.NumberDocument_lb.Content = reader[18];
                    Hunters_wrap.Children.Add(hunt);
                }
            }
            _dataBaseConnect.closeConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vouchers_Window vouchers_Window = new Vouchers_Window();
            vouchers_Window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Animals_Window animals_Window = new Animals_Window();
            animals_Window.Show();
            this.Close();
        }

        private void SearchFullName_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@"SELECT * FROM [БД Охотники].[dbo].[Охотники] where ФИО like '%{SearchFullName_tb.Text}%'");
            if (SearchFullName_tb.Text == "")
            {
                Load_Data(textCommand);
            }
            SearchNumberPhone_tb.Text = "";
        }

        private void SearchNumberPhone_tb_TextChanged(object sender, TextChangedEventArgs e)
        { 
            Load_Data($@"SELECT * FROM [БД Охотники].[dbo].[Охотники] where [Номер телефона] like '%{SearchNumberPhone_tb.Text}%'");

            if (SearchNumberPhone_tb.Text == "")
            {
                Load_Data(textCommand);
            }
            SearchFullName_tb.Text = "";
        }

        private void AddNewHunter_btn_Click(object sender, RoutedEventArgs e)
        {
            AddHunter_Window addHunter_Window = new AddHunter_Window();
            addHunter_Window.AddNewHunterInDB_btn.Visibility = Visibility.Visible;
            addHunter_Window.SaveHunter_btn_Copy.Visibility = Visibility.Hidden;
            addHunter_Window.Show();
            this.Close();
        } 
    }
}
