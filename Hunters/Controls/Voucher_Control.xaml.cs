using Hunters.AddWindow;
using Hunters.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
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

namespace Hunters.Controls
{
    /// <summary>
    /// Логика взаимодействия для Voucher_Control.xaml
    /// </summary>
    public partial class Voucher_Control : UserControl
    {
        DataBaseConnect _dataBaseConnect = new DataBaseConnect();
        string textCommand = "SELECT [Путевка-животные].[Номер путевки], [Путевка-животные].[Код животного], Животные.Наименование FROM Животные CROSS JOIN [Путевка-животные] where [Путевка-животные].[Код животного] = Животные.[Код животного] ";
        public Voucher_Control()
        {
            InitializeComponent();
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            AddVoucher_Window addVoucher_Window = new AddVoucher_Window();
            addVoucher_Window.Add_btn.Visibility = Visibility.Hidden;
            addVoucher_Window.AddAndPrint_btn.Visibility = Visibility.Hidden;
            addVoucher_Window.SaveVoucher_btn.Visibility = Visibility.Visible;

            addVoucher_Window.NumberVoucher_tb.Text = TicketNumber.Content.ToString();
            addVoucher_Window.DateStart_tb.Text = DateStart.Content.ToString();
            addVoucher_Window.DateFinish_tb.Text = DateFinish.Content.ToString();
            addVoucher_Window.FullNameHunter_tb.Text = FullName.Content.ToString();
            addVoucher_Window.SeriesBilet_tb.Text = TicketSeries.Content.ToString();
            addVoucher_Window.NumberBilet_tb.Text = NumberOfTicket.Content.ToString();
            addVoucher_Window.DateIssued_tb.Text = DateOfIssue.Content.ToString();
            addVoucher_Window.DatePay_tb.Text = SettlementDate.Content.ToString();
            addVoucher_Window.DateSignature_tb.Text = SubscriptionDate.Content.ToString();

            SqlCommand command = new SqlCommand(textCommand + $@"and [Путевка-животные].[Номер путевки] like '%{TicketNumber.Content}%'", _dataBaseConnect.getConnection());
            _dataBaseConnect.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    addVoucher_Window.Label1_lb.Content = reader[2];
                }
            }
            _dataBaseConnect.closeConnection();

            addVoucher_Window.Show();
        }
    }
}
