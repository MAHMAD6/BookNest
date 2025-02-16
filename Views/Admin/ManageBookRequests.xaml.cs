namespace BookNest.Views.Admin;

public partial class ManageBookRequests : ContentPage
{
	public ManageBookRequests()
	{
		InitializeComponent();
		col.ItemsSource = new List<string> { "hello", "why" };

	}
}