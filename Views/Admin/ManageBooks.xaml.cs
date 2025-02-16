using BookNest.ViewModels.Admin;

namespace BookNest.Views.Admin;

public partial class Books : ContentPage
{
	public Books()
	{
		InitializeComponent();
		this.BindingContext = new ManageBooksViewModel();
		
	}
}