using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.AddEdit;

public partial class AddAdmin : ContentPage
{
    private readonly ApplicationContext _context;
    public AddAdmin(ApplicationContext context)
	{
		InitializeComponent();
        _context = context;
    }
        public async Task AdminCreated()
        {
            var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (adminRole == null)
            {
                adminRole = new Role { Name = "Admin" };
                _context.Roles.Add(adminRole);
                await _context.SaveChangesAsync();
            }

            var adminUser = await _context.Users
                                          .Include(u => u.Role)
                                          .FirstOrDefaultAsync(u => u.Role.Name == "Admin");

            if (adminUser == null)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new CreateAdmin(_context, adminRole));
            }
        }
    }
}