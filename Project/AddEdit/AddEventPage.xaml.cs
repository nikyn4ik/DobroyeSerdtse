using DB.Models;
using DB;
namespace Project.AddEdit;

public partial class AddEventPage : ContentPage
{
    private string _selectedImagePath;
    private Event newEvent;
    public AddEventPage()
	{
        InitializeComponent();
    }
    private string ImageFolderPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image ");
    private async void OnSelectImageClicked(object sender, EventArgs e)
    {
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    _selectedImagePath = result.FullPath;

                    string fileName = Path.GetFileName(_selectedImagePath);
                    string destinationPath = Path.Combine(ImageFolderPath, fileName);

                    File.Copy(_selectedImagePath, destinationPath, true);

                    _selectedImagePath = destinationPath;

                    EventImage eventImage = new EventImage
                    {
                        Path = _selectedImagePath
                    };
                    newEvent.Images.Add(eventImage);

                    EventImage.Source = ImageSource.FromFile(_selectedImagePath);
                }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Не удалось выбрать изображение: {ex.Message}", "OK");
        }
        }
    }

    private async void OnAddEventButtonClicked(object sender, EventArgs e)
    {
        using (var context = new ApplicationDbContext())
        {
            var newEvent = new Event
        {
            Name = NameEntry.Text,
            Description = DescriptionEditor.Text,
            Date = DatePicker.Date,
            Time = TimePicker.Time.ToString(@"hh\:mm"),
            Location = LocationEntry.Text
        };

        context.Events.Add(newEvent);
        await context.SaveChangesAsync();
        await DisplayAlert("Успех", "Мероприятие добавлено", "OK");
        await Navigation.PopAsync();
        }
    }
}