using Database;
using Database.Models;
using Project.AddEdit;

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

            LoadEvents();
        }
        private void LoadEvents()
        {
            using (var context = new ApplicationContext())
            {
                var events = context.Events.ToList();

                //foreach (var eventItem in events)
                //{
                //    eventItem.IsAdmin = _currentUser.Role.Name == "Admin";
                //}

                EventListView.ItemsSource = events;
            }
        }
        private async void OnRegisterEventClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var eventItem = button?.BindingContext as Event;
            if (eventItem != null)
            {
                using (var context = new ApplicationContext())
                {
                    var userEvent = new UserEvent
                    {
                        UserId = _currentUser.Id,
                        EventId = eventItem.Id
                    };

                    context.UserEvents.Add(userEvent);
                    await context.SaveChangesAsync();
                    await DisplayAlert("Успех", "Вы успешно откликнулись на мероприятие", "OK");
                }
            }
        }

        private async void OnEditEventClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var eventItem = button?.BindingContext as Event;
            if (eventItem != null)
            {
                await Navigation.PushAsync(new EditEvent(eventItem));
            }
        }

        private async void OnViewParticipantsClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var eventItem = button?.BindingContext as Event;
            if (eventItem != null)
            {
                await Navigation.PushAsync(new ViewFeedbacks(eventItem));
            }
        }

        private async void OnViewFeedbacksClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var eventItem = button?.BindingContext as Event;
            if (eventItem != null)
            {
                await Navigation.PushAsync(new ViewFeedbacks(eventItem));
            }
        }
        private async void OnLeaveFeedbackClicked(object sender, EventArgs e)
        {
        }
        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEventPage());
        }
    }
}
