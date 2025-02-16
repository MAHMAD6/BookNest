using BookNest.ViewModels;

namespace BookNest.Views.Login;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}
}