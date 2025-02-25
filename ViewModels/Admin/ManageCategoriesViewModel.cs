using BookNest.Models;
using BookNest.ViewModels.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace BookNest.ViewModels.Admin
{
    public partial class ManageCategoriesViewModel : ObservableObject
    {
        #region PROPERTIES

        // Snack Bar

        [ObservableProperty]
        private float _snackBarOpacity = 0;

        [ObservableProperty]
        private string _snackBarMessage = string.Empty;

        //

        private string textSearch = string.Empty;
        public string TextSearched
        {
            get { return textSearch; }
            set
            {
                textSearch = value;
                OnPropertyChanged(nameof(TextSearched));
                LoadCategories();
            }
        }

        [ObservableProperty]
        private ObservableCollection<Category> categories = new();

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private bool isPopupVisible = false;

        [ObservableProperty]
        private bool isAddEditCategoryVisible = false;

        [ObservableProperty]
        private bool isDeleteCategoryVisible = false;

        [ObservableProperty]
        private string addEditCategoryHeading = string.Empty;

        #endregion

        private bool isAddCategory = false;
        private Category? tempCategory = null;

        public ManageCategoriesViewModel()
        {
            LoadCategories();
        }


        #region COMMANDS

        [RelayCommand]
        private void AddCategory()
        {
            AddEditCategoryHeading = "Add Category";
            Name = string.Empty;
            IsPopupVisible = true;
            IsAddEditCategoryVisible = true;
            isAddCategory = true;
        }


        [RelayCommand]
        private void EditCategory(Category category)
        {
            tempCategory = category;
            Name = tempCategory.Name;
            AddEditCategoryHeading = "Edit Category";
            IsPopupVisible = true;
            IsAddEditCategoryVisible = true;
            tempCategory = category;
        }

        [RelayCommand]
        private void AddEditConfirm()
        {
            if (!Validation.IsValidName(Name))
            {
                ShowSnackBar("Invalid Category");
                return;
            }
            if (isAddCategory)
            {
                tempCategory = new Category { Name = Name };
                isAddCategory = false;
            }
            else
            {
                if (tempCategory != null)
                {
                    tempCategory.Name = Name;
                }
            }
            App.CategoriesRepo.SaveItem(tempCategory);
            LoadCategories();
            IsPopupVisible = false;
            IsAddEditCategoryVisible = false;
        }

        [RelayCommand]
        private void DeleteCategory(Category category)
        {
            if (IsDeleteCategoryVisible)
            {
                App.CategoriesRepo.DeleteItem(tempCategory);
                IsPopupVisible = false;
                IsDeleteCategoryVisible = false;
                LoadCategories();
                return;
            }
            tempCategory = category;
            IsPopupVisible = true;
            IsDeleteCategoryVisible = true;
        }

        [RelayCommand]
        private void DeleteCancel()
        {
            IsPopupVisible = false;
            IsDeleteCategoryVisible = false;
        }

        [RelayCommand]
        private void ClosePopup()
        {
            IsPopupVisible = false;
            IsAddEditCategoryVisible = false;
            IsDeleteCategoryVisible = false;
            isAddCategory = false;
            Name = string.Empty;
        }
        #endregion


        private void LoadCategories()
        {
            Categories.Clear();
            var filteredCategories = string.IsNullOrWhiteSpace(TextSearched)
                ? App.CategoriesRepo.GetItemsWithChildren()
                : App.CategoriesRepo.GetItemsWithChildren()
                    .Where(c => c.Name.Contains(TextSearched, StringComparison.OrdinalIgnoreCase));

            foreach (var category in filteredCategories)
            {
                Categories.Add(category);
            }
        }

        private async void ShowSnackBar(string message)
        {
            SnackBarMessage = message;


            for (float i = 0.0f; i <= 0.9f; i += 0.1f)
            {
                SnackBarOpacity = i;
                await Task.Delay(50);
            }

            await Task.Delay(4000);


            for (float i = 0.9f; i >= 0.0f; i -= 0.1f)
            {
                SnackBarOpacity = i;
                await Task.Delay(50);
            }
            SnackBarOpacity = 0;
        }
    }
}
