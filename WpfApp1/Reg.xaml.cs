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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        AksemovExamEntities dbEntity = new AksemovExamEntities();

        public Window1()
        {
            InitializeComponent();
        }
        
        private void reg_Click(object sender, RoutedEventArgs e)
        {
            if(tblogin.Text.Trim() == null || tbpassword.Password.Trim() == null || tbpasswordrep.Password.Trim() == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if(tbpassword.Password.Trim() != tbpasswordrep.Password.Trim())
            {
                MessageBox.Show("Пароль не совпадают!");
                return;
            }

            Пользователи newUser = new Пользователи();
            newUser.ID = Convert.ToString(dbEntity.Пользователи.Count() + 1);
            newUser.Логин = tblogin.Text.Trim();
            newUser.Пароль = tbpasswordrep.Password.Trim();
            newUser.Может_Редактировать = Convert.ToInt32(slid.Value);

            dbEntity.Пользователи.Add(newUser);
            dbEntity.SaveChanges();

            MainWindow mw = new MainWindow();

            mw.Show();
            this.Hide();


        }
    }
}
