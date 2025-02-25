using BookNest.ViewModels.User;

namespace BookNest.Views.User;

public partial class Profile : ContentPage
{
    public Profile()
    {
        InitializeComponent();
        BindingContext = new ProfileViewModel();
    }
    protected override void OnAppearing()
    {
        if (App.CurrentUser != null && App.CurrentUser.Role == "admin")
            EditButton.IsVisible = false;
        base.OnAppearing();
    }
}