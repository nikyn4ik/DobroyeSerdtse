using Database.Models;
using Database;

namespace Project.AddEdit
{
    public partial class AddFeedback : ContentPage
    {
        private Event _event;

        public AddFeedback(Event eventItem)
        {
            InitializeComponent();
            _event = eventItem;
        }

        private async void OnLeaveFeedbackClicked(object sender, EventArgs e)
        {
            double rating = RatingSlider.Value;
            string likes = LikesEditor.Text;
            string improvements = ImprovementsEditor.Text;

            var review = new Review
            {
                EventId = _event.Id,
                Content = $"Оценка: {rating:F1}, Понравилось: {likes}, Улучшения: {improvements}",
                Date = DateTime.Now
            };

            using (var context = new ApplicationContext())
            {
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            await DisplayAlert("Успех", "Отзыв успешно оставлен", "OK");

            await Navigation.PopAsync();
        }
    }
}
