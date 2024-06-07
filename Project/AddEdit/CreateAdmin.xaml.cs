using Database.Models;
using Database;

namespace Project.AddEdit;

public partial class CreateAdmin : ContentPage
{
    private readonly ApplicationContext _context;
    private readonly Role _adminRole;
    public CreateAdmin(ApplicationContext context, Role adminRole)
    {
        InitializeComponent();
        _context = context;
        _adminRole = adminRole;
    }

    private async void CreateAdminBd(object sender, EventArgs e)
    {
        var user = new User
        {
            UserName = UserName.Text,
            Email = Email.Text,
            Role = _adminRole
        };
        user.SetP(Password.Text);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        await DisplayAlert("Успех", "Администратор добавлен в систему!", "OK");
        await Navigation.PopModalAsync();
    }
}