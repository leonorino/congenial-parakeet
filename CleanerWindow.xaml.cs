using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CleanerWindow.xaml
    /// </summary>
    public partial class CleanerWindow : Window
    {
        private readonly User currentUser;

        public CleanerWindow(User currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            using (var context = new DataContext())
            {
                var schedule = context.Cleaning_Schedule.Include("Room").Where(cs => cs.User.id == currentUser.id).ToList();
                ScheduleDataGrid.ItemsSource = schedule;
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedItem = button.DataContext as Cleaning_Schedule;

            using (var context = new DataContext())
            {
                var selectedScheduleEntry = context.Cleaning_Schedule.Find(selectedItem.id);
                selectedScheduleEntry.status = "Done And Dusted";
                context.SaveChanges();
            }

            LoadSchedule();
        }
    }
}
