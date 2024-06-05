using System;
using Database;
using Database.Models;

namespace Project.Windows;

public partial class UserProfile : ContentPage
{
    private User _user;
    public UserProfile(User user)
    {
        InitializeComponent();
        _user = user;

        if (!string.IsNullOrWhiteSpace(_user.FullName))
        {
            string[] fullNameParts = _user.FullName.Split(' ');
            if (fullNameParts.Length == 3)
            {
                _user.LastName = fullNameParts[0];
                _user.FirstName = fullNameParts[1];
                _user.MiddleName = fullNameParts[2];
            }
            else
            { } 
        }

        if (_user.ImageData != null && _user.ImageData.Length > 0)
        {
            UserPhoto.Source = ImageSource.FromStream(() => new MemoryStream(_user.ImageData));
        }
        else
        {
            UserPhoto.IsVisible = false;
        }

        UserEmail.Text = _user.Email;
        UserPhone.Text = _user.PhoneNumber;
        UserLogin.Text = _user.UserName;
        UserPassword.Text = "";
        UserDescription.Text = _user.Description;
        UserAchievements.Text = string.Join(", ", _user.Achievements.Select(a => a.Title));
    }
    private async void OnSelectImageClicked(object sender, EventArgs e)
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
                stream.CopyTo(ms);
                _user.ImageData = ms.ToArray();
            }
            UserPhoto.Source = ImageSource.FromStream(() => stream);
        }
    }

    private async void OnSaveB(object sender, EventArgs e)
    {
        _user.FirstName = UserFullName.Text.Split(' ')[1];
        _user.LastName = UserFullName.Text.Split(' ')[0];
        _user.MiddleName = UserFullName.Text.Split(' ')[2];
        _user.Email = UserEmail.Text;
        _user.PhoneNumber = UserPhone.Text;
        _user.UserName = UserLogin.Text;

        if (!string.IsNullOrWhiteSpace(UserPassword.Text))
        {
            _user.SetP(UserPassword.Text);
        }

        _user.Description = UserDescription.Text;
        var achievements = UserAchievements.Text.Split(',').Select(title => new Achievement { Title = title.Trim() }).ToList();
        _user.Achievements = achievements;

        using (var context = new ApplicationContext())
        {
            context.Users.Update(_user);
            await context.SaveChangesAsync();
        }

        await DisplayAlert("Успех", "Профиль успешно обновлен", "OK");
    }
}