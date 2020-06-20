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
    /// Логика взаимодействия для Questions.xaml
    /// </summary>
    public partial class Questions : Window
    {
        AksemovExamEntities dbEntity = new AksemovExamEntities();

        int tmpID;

        Пользователи currentUser;

        public Questions(Пользователи currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            if(currentUser.Может_Редактировать == 0)
            {
                vid.Visibility = System.Windows.Visibility.Hidden;
                vopr.Visibility = System.Windows.Visibility.Hidden;
                n1.Visibility = System.Windows.Visibility.Hidden;
                n2.Visibility = System.Windows.Visibility.Hidden;
                n3.Visibility = System.Windows.Visibility.Hidden;
                n4.Visibility = System.Windows.Visibility.Hidden;
                prav.Visibility = System.Windows.Visibility.Hidden;
                Delete.Visibility = System.Windows.Visibility.Hidden;
                add.Visibility = System.Windows.Visibility.Hidden;
                Update.Visibility = System.Windows.Visibility.Hidden;
            }


            dg1.ItemsSource = dbEntity.Сведения_О_Вопросах_Теста.ToList();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dbEntity.Сведения_О_Вопросах_Теста.Find(tmpID).Вид_вопроса = vid.Text;
            dbEntity.Сведения_О_Вопросах_Теста.Find(tmpID).вопрос = vopr.Text;
            dbEntity.Сведения_О_Вопросах_Теста.Find(tmpID).Вариант_ответа_1 = n1.Text;
            dbEntity.Сведения_О_Вопросах_Теста.Find(tmpID).Вариант_ответа_2 = n2.Text;
            dbEntity.Сведения_О_Вопросах_Теста.Find(tmpID).Вариант_ответа_3 = n3.Text;
            dbEntity.Сведения_О_Вопросах_Теста.Find(tmpID).Вариант_ответа_4 = n4.Text;
            dbEntity.Сведения_О_Вопросах_Теста.Find(tmpID).Правильный_ответ = prav.Text;

            dbEntity.SaveChanges();
            dg1.ItemsSource = dbEntity.Сведения_О_Вопросах_Теста.ToList();

        }

        private void dg1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (currentUser.Может_Редактировать == 1)
            {
                tmpID = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).ID;
                vid.Text = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).Вид_вопроса;
                vopr.Text = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).вопрос;
                n1.Text = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).Вариант_ответа_1;
                n2.Text = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).Вариант_ответа_2;
                n3.Text = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).Вариант_ответа_3;
                n4.Text = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).Вариант_ответа_4;
                prav.Text = (dg1.SelectedItem as Сведения_О_Вопросах_Теста).Правильный_ответ;
            }
        }

        private void otch_Click(object sender, RoutedEventArgs e)
        {
            otchet otchet = new otchet(currentUser);

            otchet.Show();
            this.Hide();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Сведения_О_Вопросах_Теста sv = new Сведения_О_Вопросах_Теста();

            sv.ID = dbEntity.Сведения_О_Вопросах_Теста.Count();
            sv.Вид_вопроса = vid.Text;
            sv.вопрос = vopr.Text;
            sv.Вариант_ответа_1 = n1.Text;
            sv.Вариант_ответа_2 = n2.Text;
            sv.Вариант_ответа_3 = n3.Text;
            sv.Вариант_ответа_4 = n4.Text;
            sv.Правильный_ответ = prav.Text;

            dbEntity.Сведения_О_Вопросах_Теста.Add(sv);
            dbEntity.SaveChanges();
            dg1.ItemsSource = dbEntity.Сведения_О_Вопросах_Теста.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int tmpid2 = dbEntity.Сведения_О_Вопросах_Теста.Find((dg1.SelectedItem as Сведения_О_Вопросах_Теста).ID).ID;
            dbEntity.Сведения_О_Вопросах_Теста.Remove(dbEntity.Сведения_О_Вопросах_Теста.Find((dg1.SelectedItem as Сведения_О_Вопросах_Теста).ID));
            foreach (var item in dbEntity.Сведения_О_Вопросах_Теста.ToList())
            {
                if (item.ID > tmpid2)
                    item.ID = item.ID - 1;
            }

            dbEntity.SaveChanges();
            dg1.ItemsSource = dbEntity.Сведения_О_Студентах.ToList();
        }
    }
}
