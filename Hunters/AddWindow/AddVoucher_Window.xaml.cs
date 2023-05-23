using Hunters.Class;
using Hunters.Forms;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Hunters.AddWindow
{
    /// <summary>
    /// Логика взаимодействия для AddVoucher_Window.xaml
    /// </summary>
    public partial class AddVoucher_Window : Window
    {
        DataBaseConnect dataBaseConnect = new DataBaseConnect();
        string textCommand = "SELECT * FROM [БД Охотники].[dbo].[Животные]";
        int key = 0;
        string num1, num2, num3, num4, num5, num6, num7;
        int vouchNum;
        public AddVoucher_Window()
        {
            InitializeComponent();
        }

        private void LoadComboBox()
        {
            SqlCommand cmd = new SqlCommand(textCommand, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListAnimals_cb.Items.Add(reader[1]);
                }
            }
            dataBaseConnect.closeConnection();
        }

        private void Cancell_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddHunterVoucher() // добавление через вызов мтодов addVoucherAndAnimal(добовляет путевку) и addAnimalsAndOutId(добавляет код путевки и код животного)
        {
            if (Label1_lb.Content.ToString() != "")
            {
                if (NumberVoucher_tb.Text.Trim() != ""
                    && DateStart_tb.Text.Trim() != ""
                    && DateFinish_tb.Text.Trim() != ""
                    && FullNameHunter_tb.Text.Trim() != ""
                    && SeriesBilet_tb.Text.Trim() != ""
                    && NumberBilet_tb.Text.Trim() != ""
                    && DateIssued_tb.Text.Trim() != ""
                    && DatePay_tb.Text.Trim() != ""
                    && DateSignature_tb.Text.Trim() != ""
                    && Label1_lb.Content.ToString() != "")
                {
                    addVoucherAndAnimal();
                    addAnimalsAndOutId();
                }
                else
                {
                    MessageBox.Show("Ошибка! Заполните все поля!");
                }                
            }
            else
            {
                MessageBox.Show("Ошибка! Добавьте хотя бы одно животное!");
            }
        }

        private void addVoucherAndAnimal()  // добавление путевки
        {
            string sql = string.Format("Insert Into dbo.Путевки ([Номер путевки], [Дата начала]"
                    + ",[Дата окончания]"
                    + ",[ФИО охотника]"
                    + ",[Серия билета]"
                    + ",[Номер билета]"
                    + ",[Дата выдачи]"
                    + ",[Дата расчета]"
                    + ",[Дата подписи]) "
                    + "Values (@Id, @DStart, @DFinish, @FullNmae, @SerBilet, @NumBilet, @DIssued, "
                    + "@DPay, @DSign)");

            SqlCommand command = new SqlCommand(sql, dataBaseConnect.getConnection());
            command.Parameters.AddWithValue("@Id", SqlDbType.Text);
            command.Parameters["@Id"].Value = NumberVoucher_tb.Text;

            command.Parameters.AddWithValue("@DStart", SqlDbType.Date);
            command.Parameters["@DStart"].Value = DateStart_tb.SelectedDate;

            command.Parameters.AddWithValue("@DFinish", SqlDbType.Date);
            command.Parameters["@DFinish"].Value = DateFinish_tb.SelectedDate;

            command.Parameters.AddWithValue("@FullNmae", SqlDbType.Text);
            command.Parameters["@FullNmae"].Value = FullNameHunter_tb.Text;

            command.Parameters.AddWithValue("@SerBilet", SqlDbType.Text);
            command.Parameters["@SerBilet"].Value = SeriesBilet_tb.Text;

            command.Parameters.AddWithValue("@NumBilet", SqlDbType.Text);
            command.Parameters["@NumBilet"].Value = NumberBilet_tb.Text;

            command.Parameters.AddWithValue("@DIssued", SqlDbType.Date);
            command.Parameters["@DIssued"].Value = DateIssued_tb.SelectedDate;

            command.Parameters.AddWithValue("@DPay", SqlDbType.Date);
            command.Parameters["@DPay"].Value = DatePay_tb.SelectedDate;

            command.Parameters.AddWithValue("@DSign", SqlDbType.Date);
            command.Parameters["@DSign"].Value = DateSignature_tb.SelectedDate;

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
            dataBaseConnect.closeConnection();
        }

        private void addAnimalsAndOutId() // добавление животных
        {
            vouchNum = Convert.ToInt32(NumberVoucher_tb.Text);
            addOutId();
            if (Label1_lb.Content.ToString() != "" && Label2_lb.Content.ToString() == "")
            {
                string sqlAnimal = string.Format("INSERT INTO [dbo].[Путевка-животные] ([Номер путевки],[Код животного]) VALUES (@idVoucher, @idAnimal)");
                SqlCommand cmd = new SqlCommand(sqlAnimal, dataBaseConnect.getConnection());

                cmd.Parameters.AddWithValue("@idVoucher", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal", SqlDbType.Int).Value = num1;

                dataBaseConnect.openConnection();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Животные были успешно добавлены!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
                dataBaseConnect.closeConnection();
            }
            else if(Label1_lb.Content.ToString() != "" && Label2_lb.Content.ToString() != "" && Label3_lb.Content.ToString() == "") //------------------------------------
            {
                string sqlAnimal = string.Format("INSERT INTO [dbo].[Путевка-животные] ([Номер путевки],[Код животного]) VALUES (@idVoucher, @idAnimal), " +
                                                                                                                                "(@idVoucher2, @idAnimal2)");
                SqlCommand cmd = new SqlCommand(sqlAnimal, dataBaseConnect.getConnection());

                cmd.Parameters.AddWithValue("@idVoucher", SqlDbType.Int).Value = vouchNum; 
                cmd.Parameters.AddWithValue("@idAnimal", SqlDbType.Int).Value = num1;

                cmd.Parameters.AddWithValue("@idVoucher2", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal2", SqlDbType.Int).Value = num2;

                dataBaseConnect.openConnection();
                if (cmd.ExecuteNonQuery() == 2)
                {
                    MessageBox.Show("Животные были успешно добавлены!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
                dataBaseConnect.closeConnection();
            }
            else if (Label1_lb.Content.ToString() != "" && Label2_lb.Content.ToString() != "" && Label3_lb.Content.ToString() != "" && Label4_lb.Content.ToString() == "") //------------------------------------
            {
                string sqlAnimal = string.Format("INSERT INTO [dbo].[Путевка-животные] ([Номер путевки],[Код животного]) VALUES (@idVoucher, @idAnimal), " +
                                                                                                                                "(@idVoucher2, @idAnimal2)," +
                                                                                                                                "(@idVoucher3, @idAnimal3)");
                SqlCommand cmd = new SqlCommand(sqlAnimal, dataBaseConnect.getConnection());

                cmd.Parameters.AddWithValue("@idVoucher", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal", SqlDbType.Int).Value = num1;

                cmd.Parameters.AddWithValue("@idVoucher2", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal2", SqlDbType.Int).Value = num2;

                cmd.Parameters.AddWithValue("@idVoucher3", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal3", SqlDbType.Int).Value = num3;

                dataBaseConnect.openConnection();
                if (cmd.ExecuteNonQuery() == 3)
                {
                    MessageBox.Show("Животные были успешно добавлены!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
                dataBaseConnect.closeConnection();
            }
            else if (Label1_lb.Content.ToString() != "" && Label2_lb.Content.ToString() != "" && Label3_lb.Content.ToString() != "" && Label4_lb.Content.ToString() != "" && Label5_lb.Content.ToString() == "") //------------------------------------
            {
                string sqlAnimal = string.Format("INSERT INTO [dbo].[Путевка-животные] ([Номер путевки],[Код животного]) VALUES (@idVoucher, @idAnimal), " +
                                                                                                                                "(@idVoucher2, @idAnimal2)," +
                                                                                                                                "(@idVoucher3, @idAnimal3)," +
                                                                                                                                "(@idVoucher4, @idAnimal4)");
                SqlCommand cmd = new SqlCommand(sqlAnimal, dataBaseConnect.getConnection());

                cmd.Parameters.AddWithValue("@idVoucher", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal", SqlDbType.Int).Value = num1;

                cmd.Parameters.AddWithValue("@idVoucher2", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal2", SqlDbType.Int).Value = num2;

                cmd.Parameters.AddWithValue("@idVoucher3", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal3", SqlDbType.Int).Value = num3;

                cmd.Parameters.AddWithValue("@idVoucher4", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal4", SqlDbType.Int).Value = num4;

                dataBaseConnect.openConnection();
                if (cmd.ExecuteNonQuery() == 4)
                {
                    MessageBox.Show("Животные были успешно добавлены!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
                dataBaseConnect.closeConnection();
            }
            else if (Label1_lb.Content.ToString() != "" && Label2_lb.Content.ToString() != "" && Label3_lb.Content.ToString() != "" && Label4_lb.Content.ToString() != "" && Label5_lb.Content.ToString() != "" && Label6_lb.Content.ToString() == "") //------------------------------------
            {
                string sqlAnimal = string.Format("INSERT INTO [dbo].[Путевка-животные] ([Номер путевки],[Код животного]) VALUES (@idVoucher, @idAnimal), " +
                                                                                                                                "(@idVoucher2, @idAnimal2)," +
                                                                                                                                "(@idVoucher3, @idAnimal3)," +
                                                                                                                                "(@idVoucher4, @idAnimal4)," +
                                                                                                                                "(@idVoucher5, @idAnimal5)");
                SqlCommand cmd = new SqlCommand(sqlAnimal, dataBaseConnect.getConnection());

                cmd.Parameters.AddWithValue("@idVoucher", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal", SqlDbType.Int).Value = num1;

                cmd.Parameters.AddWithValue("@idVoucher2", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal2", SqlDbType.Int).Value = num2;

                cmd.Parameters.AddWithValue("@idVoucher3", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal3", SqlDbType.Int).Value = num3;

                cmd.Parameters.AddWithValue("@idVoucher4", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal4", SqlDbType.Int).Value = num4;

                cmd.Parameters.AddWithValue("@idVoucher5", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal5", SqlDbType.Int).Value = num5;

                dataBaseConnect.openConnection();
                if (cmd.ExecuteNonQuery() == 5)
                {
                    MessageBox.Show("Животные были успешно добавлены!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
                dataBaseConnect.closeConnection();
            }
            else if (Label1_lb.Content.ToString() != "" && Label2_lb.Content.ToString() != "" && Label3_lb.Content.ToString() != "" && Label4_lb.Content.ToString() != "" && Label5_lb.Content.ToString() != "" && Label6_lb.Content.ToString() != "" && Label7_lb.Content.ToString() == "") //------------------------------------
            {
                string sqlAnimal = string.Format("INSERT INTO [dbo].[Путевка-животные] ([Номер путевки],[Код животного]) VALUES (@idVoucher, @idAnimal), " +
                                                                                                                                "(@idVoucher2, @idAnimal2)," +
                                                                                                                                "(@idVoucher3, @idAnimal3)," +
                                                                                                                                "(@idVoucher4, @idAnimal4)," +
                                                                                                                                "(@idVoucher5, @idAnimal5)," +
                                                                                                                                "(@idVoucher6, @idAnimal6)");
                SqlCommand cmd = new SqlCommand(sqlAnimal, dataBaseConnect.getConnection());

                cmd.Parameters.AddWithValue("@idVoucher", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal", SqlDbType.Int).Value = num1;

                cmd.Parameters.AddWithValue("@idVoucher2", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal2", SqlDbType.Int).Value = num2;

                cmd.Parameters.AddWithValue("@idVoucher3", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal3", SqlDbType.Int).Value = num3;

                cmd.Parameters.AddWithValue("@idVoucher4", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal4", SqlDbType.Int).Value = num4;

                cmd.Parameters.AddWithValue("@idVoucher5", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal5", SqlDbType.Int).Value = num5;

                cmd.Parameters.AddWithValue("@idVoucher6", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal6", SqlDbType.Int).Value = num6;

                dataBaseConnect.openConnection();
                if (cmd.ExecuteNonQuery() == 6)
                {
                    MessageBox.Show("Животные были успешно добавлены!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
                dataBaseConnect.closeConnection();
            }
            else if (Label1_lb.Content.ToString() != "" && Label2_lb.Content.ToString() != "" && Label3_lb.Content.ToString() != "" && Label4_lb.Content.ToString() != "" && Label5_lb.Content.ToString() != "" && Label6_lb.Content.ToString() != "" && Label7_lb.Content.ToString() != "") //------------------------------------
            {
                string sqlAnimal = string.Format("INSERT INTO [dbo].[Путевка-животные] ([Номер путевки],[Код животного]) VALUES (@idVoucher, @idAnimal), " +
                                                                                                                                "(@idVoucher2, @idAnimal2)," +
                                                                                                                                "(@idVoucher3, @idAnimal3)," +
                                                                                                                                "(@idVoucher4, @idAnimal4)," +
                                                                                                                                "(@idVoucher5, @idAnimal5)," +
                                                                                                                                "(@idVoucher6, @idAnimal6)," +
                                                                                                                                "(@idVoucher7, @idAnimal7)");
                SqlCommand cmd = new SqlCommand(sqlAnimal, dataBaseConnect.getConnection());

                cmd.Parameters.AddWithValue("@idVoucher", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal", SqlDbType.Int).Value = num1;

                cmd.Parameters.AddWithValue("@idVoucher2", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal2", SqlDbType.Int).Value = num2;

                cmd.Parameters.AddWithValue("@idVoucher3", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal3", SqlDbType.Int).Value = num3;

                cmd.Parameters.AddWithValue("@idVoucher4", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal4", SqlDbType.Int).Value = num4;

                cmd.Parameters.AddWithValue("@idVoucher5", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal5", SqlDbType.Int).Value = num5;

                cmd.Parameters.AddWithValue("@idVoucher6", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal6", SqlDbType.Int).Value = num6;

                cmd.Parameters.AddWithValue("@idVoucher7", SqlDbType.Int).Value = vouchNum;
                cmd.Parameters.AddWithValue("@idAnimal7", SqlDbType.Int).Value = num7;

                dataBaseConnect.openConnection();
                if (cmd.ExecuteNonQuery() == 7)
                {
                    MessageBox.Show("Животные были успешно добавлены!");
                    dataBaseConnect.closeConnection();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Проверьте данные!");
                }
                dataBaseConnect.closeConnection();
            }
        }

        private void Show_btn_Click(object sender, RoutedEventArgs e)
        {
            addOutId();
            MessageBox.Show($"{num1} - {num2} - {num3} - {num4} - {num5} - {num6} - {num7}");
        }

        private void SaveVoucher_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Label1_lb.Content.ToString() != "")
            {
                if (NumberVoucher_tb.Text.Trim() != ""
                    && DateStart_tb.Text.Trim() != ""
                    && DateFinish_tb.Text.Trim() != ""
                    && FullNameHunter_tb.Text.Trim() != ""
                    && SeriesBilet_tb.Text.Trim() != ""
                    && NumberBilet_tb.Text.Trim() != ""
                    && DateIssued_tb.Text.Trim() != ""
                    && DatePay_tb.Text.Trim() != ""
                    && DateSignature_tb.Text.Trim() != ""
                    && Label1_lb.Content.ToString() != "")
                {
                    SqlCommand commandEdit = new SqlCommand($@"UPDATE [dbo].[Путевки]
                                                           SET [Номер путевки] = '{NumberVoucher_tb.Text}'
                                                              ,[Дата начала] = '{DateStart_tb.SelectedDate}'
                                                              ,[Дата окончания] = '{DateFinish_tb.SelectedDate}'
                                                              ,[ФИО охотника] = '{FullNameHunter_tb.Text}'
                                                              ,[Серия билета] = '{SeriesBilet_tb.Text}'
                                                              ,[Номер билета] = '{NumberBilet_tb.Text}'
                                                              ,[Дата выдачи] = '{DateIssued_tb.SelectedDate}'
                                                              ,[Дата расчета] = '{DatePay_tb.SelectedDate}'
                                                              ,[Дата подписи] = '{DateSignature_tb.Text}'
                                                         WHERE [Номер путевки] = {NumberVoucher_tb.Text}", dataBaseConnect.getConnection());
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
            else
            {
                MessageBox.Show("Ошибка! Добавьте хотя бы одно животное!");
            }
        }

        private void addOutId() //вытаскивает ID из базы данных
        {
            string sqlID_lb1 = string.Format($@"SELECT TOP (1000) [Код животного] FROM [БД Охотники].[dbo].[Животные] where Наименование like '%{Label1_lb.Content.ToString()}%'");
            SqlCommand cmdlb1 = new SqlCommand(sqlID_lb1, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader = cmdlb1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    num1 = reader[0].ToString();
                }
            }
            dataBaseConnect.closeConnection();

            string sqlID_lb2 = string.Format($@"SELECT TOP (1000) [Код животного] FROM [БД Охотники].[dbo].[Животные] where Наименование like '%{Label2_lb.Content.ToString()}%'");
            SqlCommand cmdlb2 = new SqlCommand(sqlID_lb2, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader2 = cmdlb2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    num2 = reader2[0].ToString();
                }
            }
            dataBaseConnect.closeConnection();

            string sqlID_lb3 = string.Format($@"SELECT TOP (1000) [Код животного] FROM [БД Охотники].[dbo].[Животные] where Наименование like '%{Label3_lb.Content.ToString()}%'");
            SqlCommand cmdlb3 = new SqlCommand(sqlID_lb3, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader3 = cmdlb3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    num3 = reader3[0].ToString();
                }
            }
            dataBaseConnect.closeConnection();

            string sqlID_lb4 = string.Format($@"SELECT TOP (1000) [Код животного] FROM [БД Охотники].[dbo].[Животные] where Наименование like '%{Label4_lb.Content.ToString()}%'");
            SqlCommand cmdlb4 = new SqlCommand(sqlID_lb4, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader4 = cmdlb4.ExecuteReader();
            if (reader4.HasRows)
            {
                while (reader4.Read())
                {
                    num4 = reader4[0].ToString();
                }
            }
            dataBaseConnect.closeConnection();

            string sqlID_lb5 = string.Format($@"SELECT TOP (1000) [Код животного] FROM [БД Охотники].[dbo].[Животные] where Наименование like '%{Label5_lb.Content.ToString()}%'");
            SqlCommand cmdlb5 = new SqlCommand(sqlID_lb5, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader5 = cmdlb5.ExecuteReader();
            if (reader5.HasRows)
            {
                while (reader5.Read())
                {
                    num5 = reader5[0].ToString();
                }
            }
            dataBaseConnect.closeConnection();

            string sqlID_lb6 = string.Format($@"SELECT TOP (1000) [Код животного] FROM [БД Охотники].[dbo].[Животные] where Наименование like '%{Label6_lb.Content.ToString()}%'");
            SqlCommand cmdlb6 = new SqlCommand(sqlID_lb6, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader6 = cmdlb6.ExecuteReader();
            if (reader6.HasRows)
            {
                while (reader6.Read())
                {
                    num6 = reader6[0].ToString();
                }
            }
            dataBaseConnect.closeConnection();

            string sqlID_lb7 = string.Format($@"SELECT TOP (1000) [Код животного] FROM [БД Охотники].[dbo].[Животные] where Наименование like '%{Label7_lb.Content.ToString()}%'");
            SqlCommand cmdlb7 = new SqlCommand(sqlID_lb7, dataBaseConnect.getConnection());
            dataBaseConnect.openConnection();
            SqlDataReader reader7 = cmdlb7.ExecuteReader();
            if (reader7.HasRows)
            {
                while (reader7.Read())
                {
                    num7 = reader7[0].ToString();
                }
            }
            dataBaseConnect.closeConnection();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e) // добавление охотника
        {
            AddHunterVoucher();
        }

        private void AddAndPrint_btn_Click(object sender, RoutedEventArgs e) // добавление охотника + печать
        {
            AddHunterVoucher();

        }

        private void ListAnimals_cb_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }

        private void ListAnimals_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (key < 8)
            {
                string str = ListAnimals_cb.Items[ListAnimals_cb.SelectedIndex].ToString();
                if (key == 0)
                {
                    Label1_lb.Content = str;
                    key++;
                }
                else if (key == 1)
                {
                    Label2_lb.Content = str;
                    key++;
                }
                else if (key == 2)
                {
                    Label3_lb.Content = str;
                    key++;
                }
                else if (key == 3)
                {
                    Label4_lb.Content = str;
                    key++;
                }
                else if (key == 4)
                {
                    Label5_lb.Content = str;
                    key++;
                }
                else if (key == 5)
                {
                    Label6_lb.Content = str;
                    key++;
                }
                else if (key == 6)
                {
                    Label7_lb.Content = str;
                    key++;
                }
                else
                {
                    MessageBox.Show("Все поля добавлены! Очистите поля или \nдобавьте животных в новую путевку");
                }
            }
            else
            {
                MessageBox.Show("Все поля добавлены! Очистите поля или \nдобавьте животных в новую путевку");
            }
        }

        private void ClearAnimal_btn_Click(object sender, RoutedEventArgs e)
        {
            Label1_lb.Content = "";
            Label2_lb.Content = "";
            Label3_lb.Content = "";
            Label4_lb.Content = "";
            Label5_lb.Content = "";
            Label6_lb.Content = "";
            Label7_lb.Content = "";
            key = 0;
        }
    }
}
