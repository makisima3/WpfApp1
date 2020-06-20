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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для otchet.xaml
    /// </summary>
    public partial class otchet : Window
    {
        AksemovExamEntities dbEntity = new AksemovExamEntities();

        Пользователи currentUser;

        List<Сведения_О_тестировании> tests;
        //dg1.ItemsSource = dbEntity.Сведения_О_Студентах.Where(p => p.Учебная_группа == lb.SelectedItem).ToList();
        public otchet(Пользователи currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            dg1.ItemsSource = dbEntity.Сведения_О_тестировании.ToList();

            tests = dbEntity.Сведения_О_тестировании.ToList();

            foreach (var item in dbEntity.Сведения_О_тестировании.ToList())
            {
                //Добавляем фильтрованную инфу в списки
                if (grouplist.FindName(item.Сведения_О_Студентах.Учебная_группа) == null)
                grouplist.Items.Add(item.Сведения_О_Студентах.Учебная_группа);
                if (datelist.FindName(item.Дата) == null)
                datelist.Items.Add(item.Дата);
            }
        }

        private void grouplist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dg1.ItemsSource = dbEntity.Сведения_О_тестировании.Where(p => p.Сведения_О_Студентах.Учебная_группа == grouplist.SelectedItem).ToList();
        }

        private void datelist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dg1.ItemsSource = dbEntity.Сведения_О_тестировании.Where(p => p.Дата == datelist.SelectedItem).ToList();
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            StudentsInfo st = new StudentsInfo(currentUser);

            st.Show();
            this.Hide();
        }
    }
}
