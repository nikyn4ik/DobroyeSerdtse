using Database;
using Microsoft.EntityFrameworkCore;

namespace Project.Windows
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            PasswordEntry.Completed += EnterPressed;
        }

        private async void LoginB(object sender, EventArgs e)
        {
            await PerformLogin();
        }

        private async void EnterPressed(object sender, EventArgs e)
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
                    ErrorLabel.Text = "����� � ������ ������ ���� ���������.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                var user = await context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.UserName == login);

                if (user == null || !user.VerifyP(password))
                {
                    ErrorLabel.Text = "�������� ����� ��� ������.";
                    ErrorLabel.IsVisible = true;
                    return;
                }
                await Navigation.PushAsync(new Menu(user));
            }
        }

        private async void RegisterB(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }
    }
}
