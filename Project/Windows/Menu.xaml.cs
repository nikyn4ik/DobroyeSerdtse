using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Project.AddEdit;
using System.Linq;

namespace Project.Windows
{
    public partial class Menu : ContentPage
    {
        private User _currentUser;

        public Menu(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            UserFullNameLabel.Text = _currentUser.FullName;

            if (_currentUser.Role.Name == "Admin")
            {
                AddEventButton.IsVisible = true;
            }

            LoadEvents();
        }

        private void LoadEvents()
        {
            using (var context = new ApplicationDbContext())
            {
                var events = context.Events.ToList();
                EventListView.ItemsSource = events;
            }
        }

        private async void OnRegisterEventClicked(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                var button = sender as Button;
            var eventItem = button?.BindingContext as Event;
            if (eventItem != null)
            {
                var eventRegistration = new EventRegistration
                {
                    UserId = _currentUser.Id,
                    EventId = eventItem.Id
                };

                context.EventRegistrations.Add(eventRegistration);
                await context.SaveChangesAsync();
                await DisplayAlert("Успех", "Вы успешно откликнулись на мероприятие", "OK");
                }
            }
        }

        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEventPage());
        }
    }
}
