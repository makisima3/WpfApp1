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
{//ehj
    /// <summary>
    /// Логика взаимодействия для StudentsInfo.xaml
    /// </summary>
    public partial class StudentsInfo : Window
    {
        AksemovExamEntities dbEntity = new AksemovExamEntities();

        List<Сведения_О_Студентах> groups;

        Пользователи currentUser;

        int tmpID;

        public StudentsInfo(Пользователи currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            if (currentUser.Может_Редактировать == 0)
            {
                Name.Visibility = System.Windows.Visibility.Hidden;
                Otchestvo.Visibility = System.Windows.Visibility.Hidden;
                SecondName.Visibility = System.Windows.Visibility.Hidden;
                zachNumber.Visibility = System.Windows.Visibility.Hidden;
                Group.Visibility = System.Windows.Visibility.Hidden;
                delete.Visibility = System.Windows.Visibility.Hidden;
                add.Visibility = System.Windows.Visibility.Hidden;
                Update.Visibility = System.Windows.Visibility.Hidden;
               
            }

            dg1.ItemsSource = dbEntity.Сведения_О_Студентах.ToList();

            groups = dbEntity.Сведения_О_Студентах.ToList();

            foreach (var item in dbEntity.Сведения_О_Студентах.ToList())
            {
                if (lb.FindName(item.Учебная_группа) == null)
                lb.Items.Add(item.Учебная_группа);
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dg1.ItemsSource = dbEntity.Сведения_О_Студентах.Where(p => p.Учебная_группа == lb.SelectedItem).ToList();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dbEntity.Сведения_О_Студентах.Find(tmpID).Номер_зачетной_книжки = Convert.ToInt32(zachNumber.Text);
            dbEntity.Сведения_О_Студентах.Find(tmpID).Учебная_группа = Group.Text;
            dbEntity.Сведения_О_Студентах.Find(tmpID).Имя = Name.Text;
            dbEntity.Сведения_О_Студентах.Find(tmpID).Фамилия = SecondName.Text;
            dbEntity.Сведения_О_Студентах.Find(tmpID).Отчество = Otchestvo.Text;

            dbEntity.SaveChanges();
            dg1.ItemsSource = dbEntity.Сведения_О_Студентах.ToList();
        }

        //Двойной клик по строке таблицы для вывода данных для замены
        private void dg1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (currentUser.Может_Редактировать == 1)
            {
                tmpID = (dg1.SelectedItem as Сведения_О_Студентах).ID_студента;
                Name.Text = (dg1.SelectedItem as Сведения_О_Студентах).Имя;
                zachNumber.Text = Convert.ToString((dg1.SelectedItem as Сведения_О_Студентах).Номер_зачетной_книжки);
                Group.Text = (dg1.SelectedItem as Сведения_О_Студентах).Учебная_группа;
                SecondName.Text = (dg1.SelectedItem as Сведения_О_Студентах).Фамилия;
                Otchestvo.Text = (dg1.SelectedItem as Сведения_О_Студентах).Отчество;
            }

        }

        private void vopr_Click(object sender, RoutedEventArgs e)
        {
            Questions questions = new Questions(currentUser);

            questions.Show();
            this.Hide();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Сведения_О_Студентах sd = new Сведения_О_Студентах();

            sd.ID_студента = dbEntity.Сведения_О_Студентах.Count();
            sd.Номер_зачетной_книжки = Convert.ToInt32(zachNumber.Text);
            sd.Учебная_группа = Group.Text;
            sd.Имя = Name.Text;
            sd.Фамилия = SecondName.Text;
            sd.Отчество = Otchestvo.Text;

            dbEntity.Сведения_О_Студентах.Add(sd);
            dbEntity.SaveChanges();
            dg1.ItemsSource = dbEntity.Сведения_О_Студентах.ToList();//123

            foreach (var item in dbEntity.Сведения_О_Студентах.ToList())
            {
                if (lb.FindName(item.Учебная_группа) == null)
                    lb.Items.Add(item.Учебная_группа);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            // запоминаем id студента для удаления, после чего удаляем его и у всех студентов чей ID уменьшаем его на 1;
            int tmpid2 = dbEntity.Сведения_О_Студентах.Find((dg1.SelectedItem as Сведения_О_Студентах).ID_студента).ID_студента;
            dbEntity.Сведения_О_Студентах.Remove(dbEntity.Сведения_О_Студентах.Find((dg1.SelectedItem as Сведения_О_Студентах).ID_студента));
            foreach (var item in dbEntity.Сведения_О_Студентах.ToList())
            {
                if (item.ID_студента > tmpid2)
                item.ID_студента = item.ID_студента - 1;
            }

            dbEntity.SaveChanges();
            dg1.ItemsSource = dbEntity.Сведения_О_Студентах.ToList();
            
        }
    }
}
