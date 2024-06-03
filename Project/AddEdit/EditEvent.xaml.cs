using Database.Models;

namespace Project.AddEdit;

public partial class EditEvent : ContentPage
{
    private Event _event;

    public EditEvent(Event eventItem)
    {
        InitializeComponent();
        _event = eventItem;
    }
}