using Database;
using Database.Models;

namespace Project.AddEdit;

public partial class LeaveFeedback : ContentPage
{
    private Event _event;
    private ApplicationContext _context;
    public LeaveFeedback(Event eventItem)
    {
		InitializeComponent();
	}
}