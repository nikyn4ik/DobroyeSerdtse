using Database;
using Project.AddEdit;
using Project.Windows;

namespace Project
{
    public partial class App : Application
    {
        private readonly ApplicationContext _context;

        public App()
        {
            InitializeComponent();
            _context = new ApplicationContext();

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            Page startPage;

            if (!await _context.IsAdminUserExists())
            {
                startPage = new CreateAdmin(_context);
            }
            else
            {
                startPage = new Login(_context);
            }

            MainPage = new NavigationPage(startPage);
        }
    }
}