using BookNest.ViewModels.User;

namespace BookNest.Views.User;

public partial class Profile : ContentPage
{
    private readonly ProfileViewModel _viewModel;
    public Profile()
    {
        InitializeComponent();
        _viewModel = new ProfileViewModel();
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        if (App.CurrentUser != null && App.CurrentUser.Role == "admin")
            EditButton.IsVisible = false;
        base.OnAppearing();

        _viewModel.ClosePopup(); // this function acts as refreshing the view's fields
    }
}