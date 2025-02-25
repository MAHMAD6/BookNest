using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BookNest.ViewModels.Admin
{
    public partial class TransactionsViewModel : ObservableObject
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
                LoadTransactions();
            }
        }

        [ObservableProperty]
        private ObservableCollection<Transaction> _transactions = new ObservableCollection<Transaction>();

        #endregion



        public TransactionsViewModel()
        {
            All();
        }


        #region COMMANDS
        [RelayCommand]
        private void Reserved()
        {
            TextSearched = string.Empty;
            LoadTransactions(t => t.Status == "Assigned-UserWait" || t.Status == "Assigned");
        }

        [RelayCommand]
        private void All()
        {
            TextSearched = string.Empty;
            LoadTransactions();
        }

        [RelayCommand]
        private void Fined()
        {
            TextSearched = string.Empty;
            LoadTransactions(t => t.FineDetails != null || t.Fine != null);
        }

        [RelayCommand]
        private void ReturnBook(Transaction transaction)
        {
            if (transaction != null)
            {
                transaction.Status = "Returned";
                transaction.ReturnDate = DateTime.Now;
                App.TransactionsRepo.SaveItem(transaction);
                LoadTransactions();
            }
        }

        #endregion

        private void LoadTransactions(Func<Transaction, bool>? predicate = null)
        {
            Transactions.Clear();

            List<Transaction>? transactions = App.TransactionsRepo.GetItemsWithChildren();

            var filteredTransactions = string.IsNullOrWhiteSpace(TextSearched)
                ? transactions
                : transactions.Where(t => t.Book != null &&
                                          t.Book.Title != null &&
                                          t.Book.Title.Contains(TextSearched, StringComparison.OrdinalIgnoreCase)
                                          );

            if (predicate != null)
            {
                filteredTransactions = filteredTransactions.Where(predicate);
            }

            foreach (var book in filteredTransactions)
            {
                Transactions.Add(book);
            }
        }

    }
}
