namespace BookNest.Views.Admin;

public partial class Transactions : ContentPage
{
	public Transactions()
	{
		InitializeComponent();
		BindingContext = new BookNest.ViewModels.Admin.TransactionsViewModel();
	}
}