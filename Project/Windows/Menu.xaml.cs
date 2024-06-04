using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Project.AddEdit;
using System.Collections.ObjectModel;

namespace Project.Windows
{
    public partial class Menu : ContentPage
    {
        private User _currentUser;
        public ObservableCollection<EventView> Events { get; set; }
        public bool IsAdmin { get; set; }
        public Menu(User user)
        {
            InitializeComponent();
            _currentUser = user;
            IsAdmin = user.Role.Name == "Admin";
            UserFullNameLabel.Text = user.FullName;

            LoadEvents();
        }
        private void LoadEvents()
        {
            using (var context = new ApplicationContext())
            {
                var events = GetEvents(context);

                Events = new ObservableCollection<EventView>(
                    events.Select(e => new EventView(e, IsAdmin))
                );

                EventListView.ItemsSource = Events;
            }
        }
        private List<Event> GetEvents(ApplicationContext context)
        {
            return context.Events.Include(e => e.UserEvents).ThenInclude(ue => ue.User).ToList();
        }
        private async void OnRegisterEventClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var eventItem = button?.BindingContext as EventView;
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
            var eventItem = button?.BindingContext as EventView;
            if (eventItem != null)
            {
                using (var context = new ApplicationContext())
                {
                    var eventEntity = await context.Events.FindAsync(eventItem.Id);
                    if (eventEntity != null)
                    {
                        await Navigation.PushAsync(new EditEvent(eventEntity));
                    }
                }
            }
        }

        private async void OnViewParticipantsClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var eventItem = button?.BindingContext as EventView;
            if (eventItem != null)
            {
                using (var context = new ApplicationContext())
                {
                    var eventEntity = await context.Events.FindAsync(eventItem.Id);
                    if (eventEntity != null)
                    {
                        await Navigation.PushAsync(new ViewFeedbacks(eventEntity));
                    }
                }
            }
        }
        private async void OnViewFeedbacksClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var eventItem = button?.BindingContext as EventView;
            if (eventItem != null)
            {
                using (var context = new ApplicationContext())
                {
                    var eventEntity = await context.Events.FindAsync(eventItem.Id);
                    if (eventEntity != null)
                    {
                        await Navigation.PushAsync(new ViewFeedbacks(eventEntity));
                    }
                }
            }
        }
        private async void OnLeaveFeedbackClicked(object sender, EventArgs e)
        {
            //var button = sender as Button;
            //var eventItem = button?.BindingContext as EventView;
            //if (eventItem != null)
            //{
            //    await Navigation.PushAsync(new LeaveFeedback(eventItem.Id));
            //}
        }
        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEventPage());
        }
    }
}
