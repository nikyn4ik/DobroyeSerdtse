using Database.Models;

namespace Project.Windows;

public partial class ViewFeedbacks : ContentPage
{
    private Event _event;
    public ViewFeedbacks(Event eventItem)
    {
        InitializeComponent();
        _event = eventItem;
    }
}