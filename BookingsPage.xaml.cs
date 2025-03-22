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
    /// Interaction logic for BookingsPage.xaml
    /// </summary>
    public partial class BookingsPage : Page
    {
        public BookingsPage()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            try
            {
                using (var context = new DataContext())
                {
                    var bookings = context.Reservations.Include("Guest").Include("Room").ToList();

                    if (bookings.Count == 0)
                    {
                        MessageBox.Show("No booking data to display", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    var selectedBookings = bookings.Select(b => new
                    {
                        b.id,
                        FullName = b.Guest != null ? $"{b.Guest.first_name} {b.Guest.last_name}" : "No data",
                        RoomNumber = b.Room != null ? b.Room.number.ToString() : "No data",
                        b.check_in_date,
                        b.check_out_date,
                        b.status,
                        totalPrice = b.Room.price * ((b.check_out_date - b.check_in_date).Days + 1)
                    }).ToList();

                    Bookings.ItemsSource = selectedBookings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when fetching data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBookings();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookingWindow addBookingWindow = new AddBookingWindow();
            addBookingWindow.ShowDialog();
        }
    }
}
