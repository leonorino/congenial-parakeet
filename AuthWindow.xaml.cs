using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
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
using working.Models;

namespace working
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private async void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text.Trim();
            string password = Password.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password");
                return;
            }
            using (var context = new DataContext())
            {
                var user = await context.Users.Where(u => u.username == username).FirstOrDefaultAsync();
                var adminRole = await context.Roles.Where(r => r.name == "Admin").FirstOrDefaultAsync();

                if (user == null)
                {
                    MessageBox.Show("Incorrect username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (user.IsLocked.HasValue && user.IsLocked.Value)
                {
                    MessageBox.Show("You are blocked, please contact system administrator", "Access denied", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (user.LastLoginDate.HasValue && (DateTime.Now - user.LastLoginDate.Value).TotalDays > 30 && user.role != adminRole.id)
                {
                    user.IsLocked = true;
                    await context.SaveChangesAsync();
                    MessageBox.Show("Your account is blocked due to prolonged inactivity", "Access denied", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var tmpPassword = username + password;
                UTF8Encoding textConverter = new UTF8Encoding();
                byte[] bytes = textConverter.GetBytes(tmpPassword);
                var passwordBytes = new SHA384Managed().ComputeHash(bytes);
                string encodedPassword = Convert.ToBase64String(passwordBytes);

                if (user.password == encodedPassword)
                {
                    user.LastLoginDate = DateTime.Now;
                    await context.SaveChangesAsync();

                    if (user.Role1.name == "Admin")
                    {

                        var adminWindow = new AdminWindow();
                        adminWindow.Show();
                    } 
                    else if (user.Role1.name == "Cleaning Staff")
                    {
                        var cleanerWindow = new CleanerWindow(user);
                        cleanerWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show($"Welcome, {user.first_name} {user.last_name}", "Access granted", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (user.IsFirstLogin.HasValue && user.IsFirstLogin.Value)
                        {
                            var changePasswordWindow = new ChangePasswordWindow(user.id);
                            changePasswordWindow.Show();
                        }
                        else
                        {
                            ManagerWindow managerWindow = new ManagerWindow();
                            managerWindow.Show();
                        }
                    }

                    this.Close();
                }
                else
                {
                    user.FailedLoginAttempts++;
                    await context.SaveChangesAsync();
                    if (user.FailedLoginAttempts >= 3)
                    {
                        user.IsLocked = true;
                        await context.SaveChangesAsync();
                        MessageBox.Show("Your account was blocked three incorrect log in attempts", "Access denied", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show($"Incorrect password, attempts left: {3 - user.FailedLoginAttempts}", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
