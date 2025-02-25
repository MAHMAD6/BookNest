using BookNest.ViewModels.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookNest.ViewModels.User
{
    public partial class ProfileViewModel : ObservableObject
    {
        // Snack Bar

        [ObservableProperty]
        private float _snackBarOpacity = 0;

        [ObservableProperty]
        private string _snackBarMessage = string.Empty;

        //

        [ObservableProperty]
        private string _userName = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _fullName = string.Empty;

        [ObservableProperty]
        private bool _isPopUpVisible = false;

        public ProfileViewModel()
        {

            ClosePopup();
        }


        [RelayCommand]
        private void Edit()
        {
            IsPopUpVisible = true;
        }

        [RelayCommand]
        private void ClosePopup()
        {
            IsPopUpVisible = false;
            UserName = App.CurrentUser?.Username ?? string.Empty;
            Password = App.CurrentUser?.Password ?? string.Empty;
            FullName = App.CurrentUser?.FullName ?? string.Empty;
        }

        [RelayCommand]
        private void Confirm()
        {
            if (!Validation.IsValidUsername(UserName))
            {
                ShowSnackBar("Invalid username"); 
                return;
            }
            if (!Validation.IsValidPassword(Password))
            {
                ShowSnackBar("Invalid password");
                return;
            }
            if (!Validation.IsValidName(FullName))
            {
                ShowSnackBar("Invalid Full Name");
                return;
            }
            

            if (App.CurrentUser != null)
            {
                App.CurrentUser.Username = UserName;
                App.CurrentUser.Password = Password;
                App.CurrentUser.FullName = FullName;
                App.UserRepo.SaveItem(App.CurrentUser);
                ClosePopup();
            }
        }
        [RelayCommand]
        private async Task Logout()
        {
            await Shell.Current.GoToAsync("//Login");
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
