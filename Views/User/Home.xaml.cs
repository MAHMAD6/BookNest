using BookNest.ViewModels.User;

namespace BookNest.Views.User;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
		this.BindingContext = new HomeViewModel();
	}
}