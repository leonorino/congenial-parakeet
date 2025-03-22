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
using working.Models;

namespace working
{
    /// <summary>
    /// Interaction logic for AddBookingWindow.xaml
    /// </summary>
    public partial class AddBookingWindow : Window
    {
        public AddBookingWindow()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            using (var context = new DataContext())
            {
                var availableRooms = context.Rooms.Where(r => r.status == "Available").Select(r => new { r.id, r.number }).ToList();
                if (availableRooms.Any())
                {
                    Room.ItemsSource = availableRooms;
                    Room.DisplayMemberPath = "number";
                    Room.SelectedValuePath = "id";
                }
                else
                {
                    MessageBox.Show("No available rooms");
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var guestLastName = LastName.Text.Trim();
            var guestFirstName = FirstName.Text.Trim();
            var guestDocument = Document.Text.Trim();

            var selectedRoomId = Room.SelectedValue as int?;
            var checkInDate = CheckInDate.SelectedDate;
            var checkOutDate = CheckOutDate.SelectedDate;

            if (string.IsNullOrEmpty(guestLastName) || string.IsNullOrEmpty(guestFirstName)
                || string.IsNullOrEmpty(guestDocument) || selectedRoomId == null || !checkInDate.HasValue
                || !checkOutDate.HasValue)
            {
                MessageBox.Show("Fill in all fields!");
                return;
            }

            if (checkInDate >= checkOutDate)
            {
                MessageBox.Show("What the hell are you doing?");
                return;
            }

            using (var context = new DataContext())
            {
                var newGuest = new Guest
                {
                    first_name = guestFirstName,
                    last_name = guestLastName,
                    document_number = guestDocument
                };
                context.Guests.Add(newGuest);
                context.SaveChanges();

                var newReservation = new Reservation
                {
                    guest_id = newGuest.id,
                    room_id = selectedRoomId.Value,
                    check_in_date = checkInDate.Value,
                    check_out_date = checkOutDate.Value,
                    status = "Approved"
                };

                context.Reservations.Add(newReservation);
                context.SaveChanges();

                MessageBox.Show("Booked the booking in the book, yeah?");
                this.Close();
            }
        }
    }
}
