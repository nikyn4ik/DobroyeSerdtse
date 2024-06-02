using DB.Models;
using Microsoft.EntityFrameworkCore;
using DB;

namespace Project.Windows
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                string login = LoginEntry.Text;
                string password = PasswordEntry.Text;

                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    ErrorLabel.Text = "Логин и пароль должны быть заполнены.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.Login == login && u.Password == password);

                if (user == null)
                {
                    ErrorLabel.Text = "Неверный логин или пароль.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                await DisplayAlert("Успех", "Вход выполнен успешно", "OK");
                await Navigation.PushAsync(new Menu());
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }
    }
}
