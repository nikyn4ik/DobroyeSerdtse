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

            if (!await _context.IsAdminUserExists())
            {
                MainPage = new CreateAdmin(_context);
            }
            else
            {
                MainPage = new Login(_context);
            }
        }
    }
}