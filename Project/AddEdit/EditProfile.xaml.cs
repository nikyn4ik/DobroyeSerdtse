using Database;
using Database.Models;

namespace Project.Windows
{
    public partial class EditProfile : ContentPage
    {
        private readonly User _user;

        public EditProfile(User user)
        {
            InitializeComponent();
            _user = user ?? throw new ArgumentNullException(nameof(user));

            InitializeEditProfile();
        }

        private void InitializeEditProfile()
        {
            if (!string.IsNullOrWhiteSpace(_user.FullName))
            {
                UserFullName.Text = _user.FullName;
            }

            if (_user.ImageData != null && _user.ImageData.Length > 0)
            {
                UserPhoto.Source = ImageSource.FromStream(() => new MemoryStream(_user.ImageData));
            }
            else
            {
                UserPhoto.IsVisible = false;
            }

            UserEmail.Text = _user.Email ?? string.Empty;
            UserPhone.Text = _user.PhoneNumber ?? string.Empty;
            UserLogin.Text = _user.UserName ?? string.Empty;
            UserPassword.Text = string.Empty;
            UserDescription.Text = _user.Description ?? string.Empty;
        }

        private async void SelectImageB(object sender, EventArgs e)
        {
            var file = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Выберите изображение"
            });

            if (file != null)
            {
                var stream = await file.OpenReadAsync();

                using (MemoryStream ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    _user.ImageData = ms.ToArray();
                }

                UserPhoto.Source = ImageSource.FromStream(() => new MemoryStream(_user.ImageData));
                UserPhoto.IsVisible = true;
            }
        }

        private async void OnSaveB(object sender, EventArgs e)
        {
            var fullNameParts = UserFullName.Text.Split(' ');
            if (fullNameParts.Length == 3)
            {
                _user.LastName = fullNameParts[0];
                _user.FirstName = fullNameParts[1];
                _user.MiddleName = fullNameParts[2];
            }

            _user.Email = UserEmail.Text;
            _user.PhoneNumber = UserPhone.Text;
            _user.UserName = UserLogin.Text;

            if (!string.IsNullOrWhiteSpace(UserPassword.Text))
            {
                _user.SetP(UserPassword.Text);
            }

            _user.Description = UserDescription.Text;

            using (var context = new ApplicationContext())
            {
                context.Users.Update(_user);
                await context.SaveChangesAsync();
            }

            await DisplayAlert("Успех", "Профиль успешно обновлен", "OK");
            await Navigation.PopAsync();
        }
    }
}
