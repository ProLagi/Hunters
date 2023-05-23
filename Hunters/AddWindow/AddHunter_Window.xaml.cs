using Hunters.Class;
using Hunters.Forms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Hunters.AddWindow
{
    /// <summary>
    /// Логика взаимодействия для AddHunter_Window.xaml
    /// </summary>
    public partial class AddHunter_Window : Window
    {  
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        public string idHunterlKod = "";
        public AddHunter_Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddNewHunterInDB_btn_Click(object sender, RoutedEventArgs e)
        {
            if (FullNameHunter_lb.Text.Trim() != ""
                && NumberChlenBilet_lb.Text.Trim() != ""
                && SeriesChlenBilet_lb.Text.Trim() != ""
                && SeriesBilet_lb.Text.Trim() != ""
                && NumberBilet_lb.Text.Trim() != ""
                && DateIssue_lb.Text.Trim() != ""
                && NPP_lb.Text.Trim() != ""
                && NumberPhone_lb.Text.Trim() != ""
                && DateBirth_lb.Text.Trim() != ""
                && INN_lb.Text.Trim() != ""
                && DateEntry_lb.Text.Trim() != ""
                && City_lb.Text.Trim() != ""
                && Street_lb.Text.Trim() != ""
                && Home_lb.Text.Trim() != ""
                && Entrance_lb.Text.Trim() != ""
                && Apartment_lb.Text.Trim() != ""
                && DeductionType_lb.Text.Trim() != ""
                && NumberDocument_lb.Text.Trim() != "")
            {
                string sql = string.Format("INSERT INTO [dbo].[Охотники]"
           + "([ФИО]"
           + ",[Номер членского билета]"
           + ",[Серия членского билета]"
           + ",[Серия билета]"
           + ",[Номер билета]"
           + ",[Дата выдачи]"
           + ",[НПП]"
           + ",[Номер телефона]"
           + ",[Дата рождения]"
           + ",[ИНН]"
           + ",[Дата вступления]"
           + ",[Город]"
           + ",[Улица]"
           + ",[Дом]"
           + ",[Подъезд]"
           + ",[Квартира]"
           + ",[Тип вычета]"
           + ",[Номер документа]) VALUES" //---------
           + "(@FullNameHunter_lb"
           + ",@NumberChlenBilet_lb"
           + ",@SeriesChlenBilet_lb"
           + ",@SeriesBilet_lb"
           + ",@NumberBilet_lb"
           + ",@DateIssue_lb"
           + ",@NPP_lb"
           + ",@NumberPhone_lb"
           + ",@DateBirth_lb"
           + ",@INN_lb"
           + ",@DateEntry_lb"
           + ",@City_lb"
           + ",@Street_lb"
           + ",@Home_lb"
           + ",@Entrance_lb"
           + ",@Apartment_lb"
           + ",@DeductionType_lb"
           + ",@NumberDocument_lb)");

                SqlCommand command = new SqlCommand(sql, dataBaseConnect.getConnection());
                command.Parameters.AddWithValue("@FullNameHunter_lb", SqlDbType.Text);
                command.Parameters["@FullNameHunter_lb"].Value = FullNameHunter_lb.Text;

                command.Parameters.AddWithValue("@NumberChlenBilet_lb", SqlDbType.Date);
                command.Parameters["@NumberChlenBilet_lb"].Value = NumberChlenBilet_lb.Text;

                command.Parameters.AddWithValue("@SeriesChlenBilet_lb", SqlDbType.Date);
                command.Parameters["@SeriesChlenBilet_lb"].Value = SeriesChlenBilet_lb.Text;

                command.Parameters.AddWithValue("@SeriesBilet_lb", SqlDbType.Date);
                command.Parameters["@SeriesBilet_lb"].Value = SeriesBilet_lb.Text;

                command.Parameters.AddWithValue("@NumberBilet_lb", SqlDbType.Date);
                command.Parameters["@NumberBilet_lb"].Value = NumberBilet_lb.Text;

                command.Parameters.AddWithValue("@DateIssue_lb", SqlDbType.Date);
                command.Parameters["@DateIssue_lb"].Value = DateIssue_lb.Text;

                command.Parameters.AddWithValue("@NPP_lb", SqlDbType.Date);
                command.Parameters["@NPP_lb"].Value = NPP_lb.Text;

                command.Parameters.AddWithValue("@NumberPhone_lb", SqlDbType.Date);
                command.Parameters["@NumberPhone_lb"].Value = NumberPhone_lb.Text;

                command.Parameters.AddWithValue("@DateBirth_lb", SqlDbType.Date);
                command.Parameters["@DateBirth_lb"].Value = DateBirth_lb.Text;

                command.Parameters.AddWithValue("@INN_lb", SqlDbType.Date);
                command.Parameters["@INN_lb"].Value = INN_lb.Text;

                command.Parameters.AddWithValue("@DateEntry_lb", SqlDbType.Date);
                command.Parameters["@DateEntry_lb"].Value = DateEntry_lb.Text;

                command.Parameters.AddWithValue("@City_lb", SqlDbType.Date);
                command.Parameters["@City_lb"].Value = City_lb.Text;

                command.Parameters.AddWithValue("@Street_lb", SqlDbType.Date);
                command.Parameters["@Street_lb"].Value = Street_lb.Text;

                command.Parameters.AddWithValue("@Home_lb", SqlDbType.Date);
                command.Parameters["@Home_lb"].Value = Home_lb.Text;

                command.Parameters.AddWithValue("@Entrance_lb", SqlDbType.Date);
                command.Parameters["@Entrance_lb"].Value = Entrance_lb.Text;

                command.Parameters.AddWithValue("@Apartment_lb", SqlDbType.Date);
                command.Parameters["@Apartment_lb"].Value = Apartment_lb.Text;

                command.Parameters.AddWithValue("@DeductionType_lb", SqlDbType.Date);
                command.Parameters["@DeductionType_lb"].Value = DeductionType_lb.Text;

                command.Parameters.AddWithValue("@NumberDocument_lb", SqlDbType.Date);
                command.Parameters["@NumberDocument_lb"].Value = NumberDocument_lb.Text;

                dataBaseConnect.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Запись была успешно добавлена!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Заполните все поля!");
            }
        }

        private void SaveHunter_btn_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (FullNameHunter_lb.Text.Trim() != ""
                && NumberChlenBilet_lb.Text.Trim() != ""
                && SeriesChlenBilet_lb.Text.Trim() != ""
                && SeriesBilet_lb.Text.Trim() != ""
                && NumberBilet_lb.Text.Trim() != ""
                && DateIssue_lb.Text.Trim() != ""
                && NPP_lb.Text.Trim() != ""
                && NumberPhone_lb.Text.Trim() != ""
                && DateBirth_lb.Text.Trim() != ""
                && INN_lb.Text.Trim() != ""
                && DateEntry_lb.Text.Trim() != ""
                && City_lb.Text.Trim() != ""
                && Street_lb.Text.Trim() != ""
                && Home_lb.Text.Trim() != ""
                && Entrance_lb.Text.Trim() != ""
                && Apartment_lb.Text.Trim() != ""
                && DeductionType_lb.Text.Trim() != ""
                && NumberDocument_lb.Text.Trim() != "")
            {
                string sql = string.Format("INSERT INTO [dbo].[Охотники]"
           + "([ФИО]"
           + ",[Номер членского билета]"
           + ",[Серия членского билета]"
           + ",[Серия билета]"
           + ",[Номер билета]"
           + ",[Дата выдачи]"
           + ",[НПП]"
           + ",[Номер телефона]"
           + ",[Дата рождения]"
           + ",[ИНН]"
           + ",[Дата вступления]"
           + ",[Город]"
           + ",[Улица]"
           + ",[Дом]"
           + ",[Подъезд]"
           + ",[Квартира]"
           + ",[Тип вычета]"
           + ",[Номер документа]) VALUES" //---------
           + "(@FullNameHunter_lb"
           + ",@NumberChlenBilet_lb"
           + ",@SeriesChlenBilet_lb"
           + ",@SeriesBilet_lb"
           + ",@NumberBilet_lb"
           + ",@DateIssue_lb"
           + ",@NPP_lb"
           + ",@NumberPhone_lb"
           + ",@DateBirth_lb"
           + ",@INN_lb"
           + ",@DateEntry_lb"
           + ",@City_lb"
           + ",@Street_lb"
           + ",@Home_lb"
           + ",@Entrance_lb"
           + ",@Apartment_lb"
           + ",@DeductionType_lb"
           + ",@NumberDocument_lb)");
                SqlCommand commandEdit = new SqlCommand($@"UPDATE [dbo].[Охотники]
                                                       SET [ФИО] = {FullNameHunter_lb.Text}
                                                          ,[Номер членского билета] = {NumberChlenBilet_lb.Text}
                                                          ,[Серия членского билета] = {SeriesChlenBilet_lb.Text}
                                                          ,[Серия билета] = {SeriesBilet_lb.Text}
                                                          ,[Номер билета] = {NumberBilet_lb.Text}
                                                          ,[Дата выдачи] = {DateIssue_lb.Text}
                                                          ,[НПП] = {NPP_lb.Text}
                                                          ,[Номер телефона] = {NumberPhone_lb.Text}
                                                          ,[Дата рождения] = {DateBirth_lb.Text}
                                                          ,[ИНН] = {INN_lb.Text}
                                                          ,[Дата вступления] = {DateEntry_lb.Text}
                                                          ,[Город] = {City_lb.Text}
                                                          ,[Улица] = {Street_lb.Text}
                                                          ,[Дом] = {Home_lb.Text}
                                                          ,[Подъезд] = {Entrance_lb.Text}
                                                          ,[Квартира] = {Apartment_lb.Text}
                                                          ,[Тип вычета] = {DeductionType_lb.Text}
                                                          ,[Номер документа] = {NumberDocument_lb.Text}
                                                     WHERE [Код охотника] = {idHunterlKod}", dataBaseConnect.getConnection());
                dataBaseConnect.openConnection();
                if (commandEdit.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Запись была успешно добавлена!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Заполните все поля!");
            }
        }
    }
}
