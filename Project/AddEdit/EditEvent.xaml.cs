using Database;
using Database.Models;

namespace Project.AddEdit
{
    public partial class EditEvent : ContentPage
    {
        private Event _event;
        private ApplicationContext _context;

        public EditEvent(Event eventItem)
        {
            InitializeComponent();
            _context = new ApplicationContext();
            LoadEventData(eventItem);
        }

        private async void LoadEventData(Event eventItem)
        {
            _event = await _context.Events.FindAsync(eventItem);
            if (_event != null)
            {
                TitleEntry.Text = _event.Title;
                DescriptionEditor.Text = _event.Description;
                DatePicker.Date = _event.Date.Date;
                TimePicker.Time = _event.Date.TimeOfDay;
                LocationEntry.Text = _event.Location;
                EventImage.Source = _event.ImagePath;
            }
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Выберите изображение для события"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var imagePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    await stream.CopyToAsync(fileStream);
                }

                EventImage.Source = imagePath;
                _event.ImagePath = imagePath;
            }
        }

        private async void OnSaveEventB(object sender, EventArgs e)
        {
            _event.Title = TitleEntry.Text;
            _event.Description = DescriptionEditor.Text;
            _event.Date = DatePicker.Date + TimePicker.Time;
            _event.Location = LocationEntry.Text;

            _context.Events.Update(_event);
            await _context.SaveChangesAsync();

            await DisplayAlert("Успех", "Мероприятие успешно обновлено", "OK");
            await Navigation.PopAsync();
        }
    }
}
