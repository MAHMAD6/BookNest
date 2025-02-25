using BookNest.Models;
using BookNest.ViewModels.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BookNest.ViewModels.Admin
{
    public partial class ManageBooksViewModel : ObservableObject
    {

        #region Properties

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
                LoadBooks();
            }
        }

        [ObservableProperty]
        private ObservableCollection<Book> books = new ObservableCollection<Book>();

        [ObservableProperty]
        private ObservableCollection<Author> authors = new ObservableCollection<Author>();

        [ObservableProperty]
        private ObservableCollection<Category> categories = new ObservableCollection<Category>();

        [ObservableProperty]
        private Author? selectedAuthor = null;

        [ObservableProperty]
        private Category? selectedCategory = null;

        [ObservableProperty]
        private bool isPopupVisible = false;

        [ObservableProperty]
        private bool isAddEditBookVisible = false;

        [ObservableProperty]
        private bool isDeleteBookVisible = false;

        [ObservableProperty]
        private string addEditBookHeading = string.Empty;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string iSBN = string.Empty;

        [ObservableProperty]
        private string availabilityStatus = string.Empty;



        #endregion

        private bool isAddBook = false;
        private Book? tempBook;



        public ManageBooksViewModel()
        {
            //var newAuthor = new Author { Name = "J.K. Rowling" };
            //App.AuthorsRepo.SaveItem(newAuthor);

            //var newCategory = new Category { Name = "Fantasy" };
            //App.CategoriesRepo.SaveItem(newCategory);

            //var newBook = new Book
            //{
            //    Title = "Harry Potter ",
            //    ISBN = "978-0439708180",
            //    AuthorId = newAuthor.Id,
            //    CategoryId = newCategory.Id,
            //    AvailabilityStatus = "Available",
            //    CreatedAt = DateTime.Now,
            //    ModifiedAt = DateTime.Now,
            //};

            //App.BooksRepo.SaveItem(newBook);


            LoadAuthors();
            LoadCategories();
            LoadBooks();
        }

        #region COMMANDS

        [RelayCommand]
        public void AddBook()
        {
            IsPopupVisible = true;
            isAddBook = true;
            IsAddEditBookVisible = true;
            AddEditBookHeading = "Add Book";

            LoadAuthors();
            LoadCategories();
        }

        [RelayCommand]
        public void EditBook(Book book)
        {

            LoadAuthors();
            LoadCategories();
            IsPopupVisible = true;
            IsAddEditBookVisible = true;
            AddEditBookHeading = "Edit Book";
            tempBook = book;
            Title = tempBook.Title;
            ISBN = tempBook.ISBN;
            AvailabilityStatus = tempBook.AvailabilityStatus;
            foreach (var author in Authors)
            {
                if (book != null &&  book.Author != null && book.Author.Name == author.Name)
                {
                    SelectedAuthor = author;
                }
            }
            foreach (var category in Categories)
            {
                if (book != null && book.Category != null && book.Category.Name == category.Name)
                {
                    SelectedCategory = category;
                }
            }
        }



        [RelayCommand]
        private void AddEditBookConfirm()
        {
            if(!Validation.IsValidBookTitle(Title))
            {
                ShowSnackBar("Invalid Title");
                return;
            }
            if (!Validation.IsValidISBN(ISBN))
            {
                ShowSnackBar("Invalid ISBN");
                return;
            }

            if (isAddBook)
            {
                Book newBook = new Book
                {
                    Title = Title,
                    ISBN = ISBN,
                    AvailabilityStatus = AvailabilityStatus,
                    AuthorId = SelectedAuthor?.Id ?? -1,
                    CategoryId = SelectedCategory?.Id ?? -1
                };

                tempBook = newBook;
                isAddBook = false;
            }
            else
            {
                if (tempBook != null)
                {
                    tempBook.Title = Title;
                    tempBook.ISBN = ISBN;
                    tempBook.AvailabilityStatus = AvailabilityStatus;
                    tempBook.AuthorId = SelectedAuthor?.Id ?? tempBook.AuthorId;
                    tempBook.CategoryId = SelectedCategory?.Id ?? tempBook.CategoryId;
                }
            }
            App.BooksRepo.SaveItem(tempBook);
            LoadBooks();
            IsPopupVisible = false;
            IsAddEditBookVisible = false;
            RefreshControls();
        }

        private void RefreshControls()
        {
            Title = string.Empty;
            ISBN = string.Empty;
            AvailabilityStatus = string.Empty;
            SelectedCategory = null;
            SelectedAuthor = null;

        }

        [RelayCommand]
        public void DeleteBook(Book book)
        {
            if (IsDeleteBookVisible)
            {
                App.BooksRepo.DeleteItem(tempBook);
                IsDeleteBookVisible = false;
                IsPopupVisible = false;
                LoadBooks();
                return;
            }
            tempBook = book;
            IsDeleteBookVisible = true;
            IsPopupVisible = true;
        }

        [RelayCommand]
        private void DeleteCancel()
        {
            IsPopupVisible = false;
            IsDeleteBookVisible = false;
        }

        [RelayCommand]
        private void ClosePopup()
        {
            IsPopupVisible = false;
            IsAddEditBookVisible = false;
            IsDeleteBookVisible = false;
            RefreshControls();
        }

        [RelayCommand]
        private async Task GoToBookRequest()
        {
            await Shell.Current.GoToAsync("//BookRequests");
        }

        #endregion

        private void LoadBooks()
        {
            Books.Clear();
            var filteredBooks = string.IsNullOrWhiteSpace(TextSearched)
                ? App.BooksRepo.GetItemsWithChildren()
                : App.BooksRepo.GetItemsWithChildren()
                    .Where(b => b.Title.Contains(TextSearched, StringComparison.OrdinalIgnoreCase));

            foreach (var book in filteredBooks)
            {
                Books.Add(book);
            }
        }
        private void LoadAuthors()
        {
            Authors.Clear();
            foreach (var author in App.AuthorsRepo.GetItems())
            {
                Authors.Add(author);
            }
        }

        private void LoadCategories()
        {
            Categories.Clear();
            foreach (var category in App.CategoriesRepo.GetItems())
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
