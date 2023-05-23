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
    /// Логика взаимодействия для Vouchers_Window.xaml
    /// </summary>
    public partial class Vouchers_Window : Window
    {
        DataBaseConnect _dataBaseConnect = new DataBaseConnect();
        string textCommand = "SELECT * FROM [БД Охотники].[dbo].[Путевки]";
        public Vouchers_Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hunters_Window hunters_Window = new Hunters_Window();
            hunters_Window.Show();
            this.Close();    
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Animals_Window animals_Window = new Animals_Window();
            animals_Window.Show();
            this.Close();
        }

        public void Load_Data(string temp)
        {
            Vouchers_wrap.Children.Clear();
            SqlCommand command = new SqlCommand(textCommand + temp, _dataBaseConnect.getConnection());
            _dataBaseConnect.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Controls.Voucher_Control voucher_Control = new Controls.Voucher_Control();
                    voucher_Control.TicketNumber.Content = reader[0];
                    voucher_Control.TicketSeries.Content = reader[4];
                    voucher_Control.NumberOfTicket.Content = reader[5];
                    voucher_Control.FullName.Content = reader[3];
                    voucher_Control.DateStart.Content = reader[1];
                    voucher_Control.DateFinish.Content = reader[2];
                    voucher_Control.DateOfIssue.Content = reader[6];
                    voucher_Control.SettlementDate.Content = reader[7];
                    voucher_Control.SubscriptionDate.Content = reader[8];
                    Vouchers_wrap.Children.Add(voucher_Control);
                }
            }
            _dataBaseConnect.closeConnection();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Data("");
        }

        private void SearchNumberVoucher_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@" where [Номер путевки] like '%{SearchNumberVoucher_tb.Text}%'");
        }

        private void SearchDateStart_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@" where [Дата начала] like '%{SearchDateStart_tb.Text}%'");
        }

        private void SearchDateFinish_tb_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@" where [Дата окончания] like '%{SearchDateFinish_tb_Copy.Text}%'");
        }

        private void SearchSeriesBilet_tb_Copy1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@" where [Серия билета] like '%{SearchSeriesBilet_tb_Copy1.Text}%'");
        }

        private void SearchNumberBilet_tb_Copy2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@" where [Номер билета] like '%{SearchNumberBilet_tb_Copy2.Text}%'");
        }

        private void SearchFullName_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data($@" where [ФИО охотника] like '%{SearchFullName_tb.Text}%'");
        }
    }
}
