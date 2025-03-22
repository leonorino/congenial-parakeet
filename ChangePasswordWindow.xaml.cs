using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using working.Models;

namespace working
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly int _userId;
        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private async void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = OldPassword.Password;
            string newPassword = NewPassword.Password;
            string passwordConfirmation = ConfirmPassword.Password;

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(passwordConfirmation))
            {
                MessageBox.Show("Please, fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new DataContext())
            {
                var user = await context.Users.Where(u => u.id == _userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    MessageBox.Show("Internal error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }

                if (user.password != oldPassword)
                {
                    MessageBox.Show("Old password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (newPassword != passwordConfirmation)
                {
                    MessageBox.Show("New passwords do not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                user.IsFirstLogin = false;
                user.password = newPassword;
                await context.SaveChangesAsync();
                MessageBox.Show("Password updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
