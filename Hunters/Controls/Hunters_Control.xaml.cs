using Hunters.AddWindow;
using Hunters.Forms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hunters.Controls
{
    /// <summary>
    /// Логика взаимодействия для Hunters_Control.xaml
    /// </summary>
    public partial class Hunters_Control : UserControl
    {
        public Hunters_Control()
        {
            InitializeComponent();
        }

        private void GoAddWindowVoucher_Click(object sender, RoutedEventArgs e)
        {
            AddVoucher_Window addVoucher_Window = new AddVoucher_Window();
            addVoucher_Window.FullNameHunter_tb.Text = this.FullNameHunter_lb.Content.ToString();
            addVoucher_Window.SeriesBilet_tb.Text = this.SeriesBilet_lb.Content.ToString();
            addVoucher_Window.NumberBilet_tb.Text = this.NumberBilet_lb.Content.ToString();
            addVoucher_Window.Add_btn.Visibility = Visibility.Visible;
            addVoucher_Window.AddAndPrint_btn.Visibility = Visibility.Visible;
            addVoucher_Window.SaveVoucher_btn.Visibility = Visibility.Hidden;
            addVoucher_Window.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddHunter_Window addHunter_Window = new AddHunter_Window();
            addHunter_Window.Show();
            addHunter_Window.idHunterlKod = IDHunter_lb.Content.ToString();
            addHunter_Window.AddNewHunterInDB_btn.Visibility = Visibility.Hidden;
            addHunter_Window.SaveHunter_btn_Copy.Visibility = Visibility.Visible;

            addHunter_Window.FullNameHunter_lb.Text = FullNameHunter_lb.Content.ToString();
            addHunter_Window.NumberChlenBilet_lb.Text = NumberChlenBilet_lb.Content.ToString();
            addHunter_Window.SeriesChlenBilet_lb.Text = SeriesChlenBilet_lb.Content.ToString();
            addHunter_Window.SeriesBilet_lb.Text = SeriesBilet_lb.Content.ToString();
            addHunter_Window.NumberBilet_lb.Text = NumberBilet_lb.Content.ToString();
            addHunter_Window.DateIssue_lb.Text = DateIssue_lb.Content.ToString();
            addHunter_Window.NPP_lb.Text = NPP_lb.Content.ToString();
            addHunter_Window.NumberPhone_lb.Text = NumberPhone_lb.Content.ToString();
            addHunter_Window.DateBirth_lb.Text = DateBirth_lb.Content.ToString();
            addHunter_Window.INN_lb.Text = INN_lb.Content.ToString();
            addHunter_Window.DateEntry_lb.Text = DateEntry_lb.Content.ToString();
            addHunter_Window.City_lb.Text = City_lb.Content.ToString();
            addHunter_Window.Street_lb.Text = Street_lb.Content.ToString();
            addHunter_Window.Home_lb.Text = Home_lb.Content.ToString();
            addHunter_Window.Entrance_lb.Text = Entrance_lb.Content.ToString();
            addHunter_Window.Apartment_lb.Text = Apartment_lb.Content.ToString();
            addHunter_Window.DeductionType_lb.Text = DeductionType_lb.Content.ToString();
            addHunter_Window.NumberDocument_lb.Text = NumberDocument_lb.Content.ToString();
        }
    }
}
