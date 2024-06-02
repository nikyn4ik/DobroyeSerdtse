using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Windows
{
    public partial class Registration : ContentPage
    {

        public Registration()
        {
            InitializeComponent();
        }

        private async void RegisterB(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
            string lastName = LastNameEntry.Text;
            string firstName = FirstNameEntry.Text;
            string middleName = MiddleNameEntry.Text;
            string email = EmailEntry.Text;
            string phoneNumber = PhoneNumberEntry.Text;
            string login = LoginEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

                var existingUser = await context.Users
    .FirstOrDefaultAsync(u => u.Login == login);

            var newUser = new User
            {
                FullName = $"{lastName} {firstName} {middleName}",
                Login = login,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                RoleId = 2
            };

                context.Users.Add(newUser);
            await context.SaveChangesAsync();

            await DisplayAlert("Успех", "Регистрация прошла успешно", "OK");
        }
    }
    }
}
