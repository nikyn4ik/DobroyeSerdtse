using Database;
using Microsoft.EntityFrameworkCore;

namespace Project.Windows
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            PasswordEntry.Completed += OnEnterPressed;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await PerformLogin();
        }

        private async void OnEnterPressed(object sender, EventArgs e)
        {
            await PerformLogin();
        }

        private async Task PerformLogin()
        {
            using (var context = new ApplicationContext())
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
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.UserName == login && u.Password == password);

                if (user == null)
                {
                    ErrorLabel.Text = "Неверный логин или пароль.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                await DisplayAlert("Успех", "Вход выполнен успешно", "OK");
                await Navigation.PushAsync(new Menu(user));
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }
    }
}
