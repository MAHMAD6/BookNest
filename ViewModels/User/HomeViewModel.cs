using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace BookNest.ViewModels.User
{
    public partial class HomeViewModel : ObservableObject
    {

        #region PROPERTIES

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
        private bool isPopupVisible = false;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now;

        #endregion


        private Book? tempBook;



        public HomeViewModel()
        {
            LoadBooks();
        }



        #region COMMANDS

        [RelayCommand]
        private void BorrowBookConfirm()
        {

            if (tempBook != null)
            {
                Transaction newTransaction = new Transaction
                {
                    UserId = 1,
                    BookId = tempBook.Id,
                    Status = "InProcessing",

                };

            }
        }


        [RelayCommand]
        private void BorrowBook(Book book)
        {
            IsPopupVisible = true;
            tempBook = book;
        }

        [RelayCommand]
        private void ClosePopup()
        {
            IsPopupVisible = false;
            selectedDate = DateTime.Now;
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
    }
}
