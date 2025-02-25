using BookNest.ViewModels.User;

namespace BookNest.Views.User;

public partial class BookRequestResponses : ContentPage
{
	public BookRequestResponses()
	{
		InitializeComponent();
		this.BindingContext = new BookRequestResponsesViewModel();

    }
}