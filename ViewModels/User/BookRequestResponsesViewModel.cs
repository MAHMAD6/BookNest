using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookNest.ViewModels.User
{
    public partial class BookRequestResponsesViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<Transaction> _responses = new();



        public BookRequestResponsesViewModel()
        {
            GetResponses();
        }




        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("//User/Home");
        }

        [RelayCommand]
        private void Delete(Transaction transaction)
        {
            if (transaction != null)
            {
                if (transaction.Status == "Assigned-UserWait")
                {
                    transaction.Status = "Assigned";
                }
                else
                {
                    transaction.Status = "Rejected";
                }
                App.TransactionsRepo.SaveItem(transaction);
                GetResponses();
            }

        }


        private void GetResponses()
        {
            int currentUserId = App.CurrentUser?.Id ?? -1;
            Responses = App.TransactionsRepo.GetItemsWithChildren(t => t.UserId == currentUserId &&
                                                        (t.Status == "Assigned-UserWait" || t.Status == "Rejected-UserWait"));
        }
    }
}
