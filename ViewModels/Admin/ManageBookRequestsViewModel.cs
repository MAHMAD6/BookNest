using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace BookNest.ViewModels.Admin
{
    public partial class ManageBookRequestsViewModel : ObservableObject
    {
        #region PROPERTIES
        [ObservableProperty]
        private List<Transaction> _requests = new();






        #endregion


        public ManageBookRequestsViewModel()
        {
            //Transaction transaction = new Transaction { 
            //UserId = 1,
            //BookId = 1,
            //Status="InProcessing"
            //};
            //App.TransactionsRepo.SaveItem(transaction);

            GetRequests();
        }



        #region COMMANDS

        [RelayCommand]
        private void Accept(Transaction transaction)
        {
            if (transaction != null)
            {
                transaction.Status = "Assigned-UserWait";
                transaction.BorrowDate = DateTime.Now;
                App.TransactionsRepo.SaveItem(transaction);
                GetRequests();
            }
        }


        [RelayCommand]
        private void Reject(Transaction transaction)
        {
            if (transaction != null)
            {
                transaction.Status = "Rejected-UserWait";
                App.TransactionsRepo.SaveItem(transaction);
                GetRequests();
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("//Admin/ManageBooks");
        }

        #endregion


        private void GetRequests() => Requests = App.TransactionsRepo.GetItemsWithChildren(t => t.Status == "InProcessing");

    }
}
