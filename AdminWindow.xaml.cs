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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            UpdateUsers();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddUserWindow();
            addUserWindow.ShowDialog();
            UpdateUsers();
        }

        private async void UnlockUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (Users.SelectedItem is User selectedUser)
            {

                using (var context = new DataContext())
                {
                    var user = await context.Users.FindAsync(selectedUser.id);

                    if (user == null)
                    {
                        return;
                    }

                    user.IsLocked = false;
                    user.LastLoginDate = null;
                    await context.SaveChangesAsync();
                }
                UpdateUsers();
            }
        }

        private async void SaveUsersButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DataContext())
            {
                foreach (var user in Users.ItemsSource as IEnumerable<User>)
                {
                    var existingUser = await context.Users.FindAsync(user.id);
                    existingUser.first_name = user.first_name;
                    existingUser.last_name = user.last_name;
                    existingUser.role = user.role;
                    existingUser.username = user.username;
                    existingUser.IsLocked = user.IsLocked;
                }

                await context.SaveChangesAsync();
                UpdateUsers();
                MessageBox.Show("Changes saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void UpdateUsers()
        {
            using (var context = new DataContext())
            {
                Users.ItemsSource = await context.Users.Include("Role1").ToListAsync();
            }
        }
    }
}
