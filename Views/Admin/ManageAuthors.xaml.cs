using BookNest.ViewModels.Admin;

namespace BookNest.Views.Admin;

public partial class ManageAuthors : ContentPage
{
	public ManageAuthors()
	{
		InitializeComponent();
		this.BindingContext = new ManageAuthorsViewModel();
	}
}