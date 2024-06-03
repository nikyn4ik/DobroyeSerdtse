using Database;
using Database.Models;

namespace Project.AddEdit
{
    public partial class AddEventPage : ContentPage
    {
        private Event newEvent = new Event();
        private string _selectedImagePath;
        private string ImageFolderPath => Path.Combine(AppContext.BaseDirectory, "Images");

        public AddEventPage()
        {
            InitializeComponent();
            LoadImages();
        }

        private void LoadImages()
        {
            var imageFolderPath = ImageFolderPath;

            if (Directory.Exists(imageFolderPath))
            {
                var imageFiles = Directory.GetFiles(imageFolderPath);
                foreach (var imagePath in imageFiles)
                {
                    var image = new Image { Source = ImageSource.FromFile(imagePath) };
                    image.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => OnImageSelected(imagePath)) });
                    ImageStackLayout.Children.Add(image);
                }
            }
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    _selectedImagePath = result.FullPath;
                    EventImage.Source = ImageSource.FromFile(_selectedImagePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось выбрать изображение: {ex.Message}", "OK");
            }
        }

        private async void OnImageSelected(string selectedImagePath)
        {
            try
            {
                _selectedImagePath = selectedImagePath;
                await DisplayAlert("Успех", "Изображение выбрано", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось выбрать изображение: {ex.Message}", "OK");
            }
        }

        private async void OnAddEventButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(DescriptionEditor.Text) ||
                DatePicker.Date == null || TimePicker.Time == null || string.IsNullOrWhiteSpace(_selectedImagePath))
            {
                await DisplayAlert("Ошибка", "Заполните все обязательные поля", "OK");
                return;
            }

            using (var context = new ApplicationContext())
            {
                var newEvent = new Event
                {
                    Title = NameEntry.Text,
                    Description = DescriptionEditor.Text,
                    Date = DatePicker.Date,
                    Location = LocationEntry.Text,
                    ImagePath = _selectedImagePath
                };

                context.Events.Add(newEvent);
                await context.SaveChangesAsync();
                await DisplayAlert("Успех", "Мероприятие добавлено", "OK");
                await Navigation.PopAsync();
            
            }}
        }
}

