using BookNest.ViewModels.User;

namespace BookNest.Views.User;

public partial class Library : ContentPage
{
	public Library()
	{
		InitializeComponent();
		BindingContext = new LibraryViewModel();
	}
}