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
    /// Interaction logic for RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        public RoomsPage()
        {
            InitializeComponent();
            UpdateRooms();
        }

        private void UpdateRooms()
        {
            using (var context = new DataContext())
            {
                RoomsDataGrid.ItemsSource = context.Rooms.ToList();
            }
        }
        
        private async void SaveRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DataContext())
            {
                foreach (var room in RoomsDataGrid.ItemsSource as IEnumerable<Room>)
                {
                    var existingRoom = await context.Rooms.FindAsync(room.id);
                    existingRoom.floor = room.floor;
                    existingRoom.number = room.number;
                    existingRoom.status = room.status;
                    existingRoom.category = room.category;
                }

                await context.SaveChangesAsync();
                UpdateRooms();
                MessageBox.Show("Changes saved");
            }
        }
    }
}
