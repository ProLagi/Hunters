using Hunters.Class;
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
    /// Логика взаимодействия для Animals_Control.xaml
    /// </summary>
    public partial class Animals_Control : UserControl
    {
        public Animals_Control()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // переход на окно редактирование
        {
            AddWindow.AddAnimal_Window addAnimal_Window = new AddWindow.AddAnimal_Window();
            addAnimal_Window.Show();

            addAnimal_Window.NameAnimals_tb.Text = NameAnimal_label_lb.Content.ToString();
            addAnimal_Window.TypePermission.Text = TypePermission_lb.Content.ToString();
            addAnimal_Window.DateStarAnimal_date.Text = DateStarAnimal_lb.Content.ToString();
            addAnimal_Window.DateFinishAnimal_date.Text = DateFinishAnimal_lb.Content.ToString();
            addAnimal_Window.MaksDay_tb.Text = MaksDay_lb.Content.ToString();
            addAnimal_Window.PerDay_tb.Text = PerDay_lb.Content.ToString();
            addAnimal_Window.PerSeason_tb.Text = PerSeason_lb.Content.ToString();
            addAnimal_Window.Collection_tb.Text = Collection_lb.Content.ToString();

            addAnimal_Window.idAnimalUpdate = IDAnimals_label_lb.Content.ToString();
            addAnimal_Window.EditSaveAnimals_btn.Visibility = Visibility.Visible;
            addAnimal_Window.AddNewAnimals_btn.Visibility = Visibility.Hidden;

            Forms.Animals_Window animals_Window = new Forms.Animals_Window();
            animals_Window.Close();
        }
    }
}
