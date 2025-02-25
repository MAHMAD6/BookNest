using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace BookNest.ViewModels.User
{
    public partial class HomeViewModel : ObservableObject
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
            if (SelectedDate < DateTime.Now)
            {
                ShowSnackBar("Invalid Due Date");
                return;
            }

            if (App.CurrentUser != null)
            {
                if (tempBook != null)
                {
                    var alreadyRequested = App.TransactionsRepo.GetItem(t => t.UserId == App.CurrentUser.Id && t.BookId == tempBook.Id && SelectedDate.Date == t.DueDate.Date);
                    if (alreadyRequested != null)
                    {
                        ShowSnackBar("RequestAlreadySent");
                    }

                    Transaction newTransaction = new Transaction
                    {
                        UserId = App.CurrentUser.Id,
                        BookId = tempBook.Id,
                        Status = "InProcessing",
                        DueDate = SelectedDate
                    };
                    App.TransactionsRepo.SaveItem(newTransaction);
                    ShowSnackBar("Request Sent");
                    ClosePopup();
                }
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
            SelectedDate = DateTime.Now;
        }

        [RelayCommand]
        private async Task GoToResponses()
        {
            await Shell.Current.GoToAsync("//BookResponses");
        }




        [RelayCommand]
        private void All()
        {
            TextSearched = string.Empty;
            LoadBooks();
        }

        [RelayCommand]
        private void New()
        {
            var filteredBooks = App.BooksRepo.GetItemsWithChildren(b => b.CreatedAt > DateTime.Now.AddDays(-3));
            foreach (var book in filteredBooks)
            {
                Books.Add(book);
            }
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
