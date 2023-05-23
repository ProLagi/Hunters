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
    /// Логика взаимодействия для AddAnimal_Window.xaml
    /// </summary>
    public partial class AddAnimal_Window : Window
    {
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        public string idAnimalUpdate = "";        
        public AddAnimal_Window()
        {
            InitializeComponent();
        }

        private void BackAnimals_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddNewAnimals_btn_Click(object sender, RoutedEventArgs e) // Добавить дичь
        {
            if (NameAnimals_tb.Text.Trim() != ""
               && TypePermission.Text.Trim() != ""
               && DateStarAnimal_date.Text.Trim() != ""
               && DateFinishAnimal_date.Text.Trim() != ""
               && MaksDay_tb.Text.Trim() != ""
               && PerDay_tb.Text.Trim() != ""
               && PerSeason_tb.Text.Trim() != ""
               && Collection_tb.Text.Trim() != "")
            {
                string sql = string.Format("INSERT INTO [dbo].[Животные]"
           + "([Наименование]"
           + ",[Вид разрешения]"
           + ",[Дата началы охоты]"
           + ",[Дата окончания охоты]"
           + ",[Макс дней]"
           + ",[За день]"
           + ",[За сезон]"
           + ",[Сбор]) VALUES " //---------
           + "(@NameAnimals_tb"
           + ",@TypePermission"
           + ",@DateStarAnimal_date"
           + ",@DateFinishAnimal_date"
           + ",@MaksDay_tb"
           + ",@PerDay_tb"
           + ",@PerSeason_tb"
           + ",@Collection_tb)");

                SqlCommand command = new SqlCommand(sql, dataBaseConnect.getConnection());
                command.Parameters.AddWithValue("@NameAnimals_tb", SqlDbType.Text);
                command.Parameters["@NameAnimals_tb"].Value = NameAnimals_tb.Text;

                command.Parameters.AddWithValue("@TypePermission", SqlDbType.Date);
                command.Parameters["@TypePermission"].Value = TypePermission.Text;

                command.Parameters.AddWithValue("@DateStarAnimal_date", SqlDbType.Date);
                command.Parameters["@DateStarAnimal_date"].Value = DateStarAnimal_date.Text;

                command.Parameters.AddWithValue("@DateFinishAnimal_date", SqlDbType.Date);
                command.Parameters["@DateFinishAnimal_date"].Value = DateFinishAnimal_date.Text;

                command.Parameters.AddWithValue("@MaksDay_tb", SqlDbType.Date);
                command.Parameters["@MaksDay_tb"].Value = MaksDay_tb.Text;

                command.Parameters.AddWithValue("@PerDay_tb", SqlDbType.Date);
                command.Parameters["@PerDay_tb"].Value = PerDay_tb.Text;

                command.Parameters.AddWithValue("@PerSeason_tb", SqlDbType.Date);
                command.Parameters["@PerSeason_tb"].Value = PerSeason_tb.Text;

                command.Parameters.AddWithValue("@Collection_tb", SqlDbType.Date);
                command.Parameters["@Collection_tb"].Value = Collection_tb.Text;

                dataBaseConnect.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Запись была успешно добавлена!");
                    dataBaseConnect.closeConnection();
                    Animals_Window animals_Window = new Animals_Window();
                    animals_Window.Show();
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

        private void EditSaveAnimals_btn_Click(object sender, RoutedEventArgs e) // Редактировать и схранить дичь
        {
            if (NameAnimals_tb.Text.Trim() != ""
                && TypePermission.Text.Trim() != ""
                && DateStarAnimal_date.Text.Trim() != ""
                && DateFinishAnimal_date.Text.Trim() != ""
                && MaksDay_tb.Text.Trim() != ""
                && PerDay_tb.Text.Trim() != ""
                && PerSeason_tb.Text.Trim() != ""
                && Collection_tb.Text.Trim() != "")
            {
                SqlCommand commandEdit = new SqlCommand($@"UPDATE [dbo].[Животные]
                                           SET [Наименование] = '{NameAnimals_tb.Text}'
                                              ,[Вид разрешения] = '{TypePermission.Text}'
                                              ,[Дата началы охоты] = '{DateStarAnimal_date.SelectedDate}'
                                              ,[Дата окончания охоты] = '{DateFinishAnimal_date.SelectedDate}'
                                              ,[Макс дней] = '{MaksDay_tb.Text}'
                                              ,[За день] = '{PerDay_tb.Text}'
                                              ,[За сезон] = '{PerSeason_tb.Text}'
                                              ,[Сбор] = '{Collection_tb.Text}'
                                         WHERE [Код животного] = {idAnimalUpdate}", dataBaseConnect.getConnection());
                dataBaseConnect.openConnection();
                if (commandEdit.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Запись была успешно сохранена!");
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
