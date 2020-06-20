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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        AksemovExamEntities dbEntity = new AksemovExamEntities();

        List<Пользователи> users;
        public static Пользователи currentUser;
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (tblogin.Text.Trim() == null || tbpassword.Password.Trim() == null)//Проверка на заполнение полей
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }

            users = dbEntity.Пользователи.ToList();

            foreach (var user in users)
            {
                if(user.Логин.Trim() == tblogin.Text.Trim() && user.Пароль.Trim() == tbpassword.Password.Trim())
                {
                    currentUser = user;
                    StudentsInfo studentsInfo = new StudentsInfo(currentUser);
                    studentsInfo.Show();
                    this.Hide();

                    MessageBox.Show("Введенные данные верны!");
                    return;
                }
            }
            MessageBox.Show("Введенные данные не верны!");
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            Window1 reg = new Window1();

            reg.Show();
            this.Hide();
        }
    }
}
