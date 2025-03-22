using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using working.Models;

namespace working
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            using (var context = new DataContext()) {
                var roles = context.Roles.ToList();
                Role.ItemsSource = roles;
                Role.DisplayMemberPath = "name";
                Role.SelectedValuePath = "id";
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName.Text.Trim();
            string lastName = LastName.Text.Trim();
            string username = Username.Text.Trim();
            string password = Password.Password;
            var roleId = Role.SelectedValue as int?;

            if (string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                !roleId.HasValue)
            {
                MessageBox.Show("Please, fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tmpPassword = username + password;

            UTF8Encoding textConverter = new UTF8Encoding();
            byte[] bytes = textConverter.GetBytes(tmpPassword);

            var passwordBytes = SHA384.Create().ComputeHash(bytes);
            string encodedPassword = Convert.ToBase64String(passwordBytes);

            using (var context = new DataContext())
            {
                var newUser = new User()
                {
                    first_name = firstName,
                    last_name = lastName,
                    username = username,
                    password = encodedPassword,
                    role = roleId.Value
                };

                context.Users.Add(newUser);
                this.Close();
                await context.SaveChangesAsync();
            }
        }
    }
}
