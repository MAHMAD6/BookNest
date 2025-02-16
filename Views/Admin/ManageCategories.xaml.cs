using BookNest.ViewModels.Admin;

namespace BookNest.Views.Admin;

public partial class ManageCategories : ContentPage
{
    public ManageCategories()
    {
        InitializeComponent();
        this.BindingContext = new ManageCategoriesViewModel();
    }
}