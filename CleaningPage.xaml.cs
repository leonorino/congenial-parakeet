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
using working.Models;

namespace working
{
    /// <summary>
    /// Interaction logic for CleaningPage.xaml
    /// </summary>
    public partial class CleaningPage : Page
    {
        public CleaningPage()
        {
            InitializeComponent();
            LoadPickers();
            UpdateData();
        }

        private void LoadPickers()
        {
            using (var context = new DataContext())
            {
                var cleaners = context.Users.Where(u => u.Role1.name == "Cleaning Staff").ToList();
                var rooms = context.Rooms.ToList();

                if (!cleaners.Any())
                {
                    MessageBox.Show("No cleaning staff", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!rooms.Any())
                {
                    MessageBox.Show("No rooms to clean", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedCleaners = cleaners.Select(c => new
                {
                    FullName = $"{c.first_name} {c.last_name}",
                    c.id
                }).ToList();

                CleanerComboBox.ItemsSource = selectedCleaners;
                CleanerComboBox.DisplayMemberPath = "FullName";
                CleanerComboBox.SelectedValuePath = "id";
                RoomComboBox.ItemsSource = rooms;
                RoomComboBox.DisplayMemberPath = "number";
                RoomComboBox.SelectedValuePath = "id";
            }
        }
        private void UpdateData()
        {
            using (var context = new DataContext())
            {
                var cleaningData = context.Cleaning_Schedule.Include("Room").Include("User").ToList();
                CleaningDataGrid.ItemsSource = cleaningData;
            }
        }

        private async void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            int? selectedCleaner = CleanerComboBox.SelectedValue as int?;
            int? selectedRoom = RoomComboBox.SelectedValue as int?;
            DateTime? cleaningDate = CleaningDatePicker.SelectedDate;
            
            if (!selectedCleaner.HasValue || !selectedRoom.HasValue || !cleaningDate.HasValue)
            {
                MessageBox.Show("Please, select all the values", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (var context = new DataContext())
            {
                var thingo = new Cleaning_Schedule
                {
                    room_id = selectedRoom.Value,
                    cleaner_id = selectedCleaner.Value,
                    status = "Cleaning Assigned",
                    cleaning_date = cleaningDate.Value,
                };

                context.Cleaning_Schedule.Add(thingo);

                await context.SaveChangesAsync();
            }

            UpdateData();
        }
    }
}
