using BookNest.ViewModels.Admin;

namespace BookNest.Views.Admin;

public partial class ManageBookRequests : ContentPage
{
	public ManageBookRequests()
	{
		InitializeComponent();
		this.BindingContext = new ManageBookRequestsViewModel();

	}
}