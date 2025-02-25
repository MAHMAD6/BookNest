using BookNest.Models;
using BookNest.ViewModels.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookNest.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        #region PROPERTIES

        // Snack Bar

        [ObservableProperty]
        private float _snackBarOpacity = 0;

        [ObservableProperty]
        private string _snackBarMessage = string.Empty;

        //

        [ObservableProperty]
        private bool isSignIn = false;

        [ObservableProperty]
        private string loginChangeHeading = string.Empty;

        [ObservableProperty]
        private string loginChangeMenuName = string.Empty;

        [ObservableProperty]
        private string mainHeading = string.Empty;

        [ObservableProperty]
        private string loginChangeButton = string.Empty;

        [ObservableProperty]
        private string menuName = string.Empty;

        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;
        #endregion

        public LoginViewModel()
        {
            // Admin
            CheckAdmin();




            // UI 
            IsSignIn = true;
            UpdatePage();
        }




        #region COMMANDS

        [RelayCommand]
        private async Task ConfirmLogin()
        {
            if (!Validation.IsValidUsername(UserName))
            {
                ShowSnackBar("Invalid Username");
                return;
            }

            if (!Validation.IsValidPassword(Password))
            {
                ShowSnackBar("Invalid Username");
                return;
            }

            BookNest.Models.User user = App.UserRepo.GetItem(user => user.Username == UserName);

            if (IsSignIn) // Sign In
            {
                if (user != null)
                {
                    if (Password == user.Password)
                    {
                        App.CurrentUser = user;
                        if (user.Role.ToLower() == "admin")
                        {
                            await Shell.Current.GoToAsync("//Admin/ManageBooks");
                            ClearControls();
                        }
                        else
                        {
                            // In case of Regular user
                            await Shell.Current.GoToAsync("//User/Home");
                            ClearControls();    
                        }
                    }
                    else
                    {
                        ShowSnackBar("Wrong Password");
                    }
                }
                else
                {
                    ShowSnackBar("User doesn't exist");
                }
            }
            else // Sign Up
            {
                if (user == null) // If user is null that's means the username name is not already existed
                {
                    BookNest.Models.User newUser = new BookNest.Models.User
                    {
                        CreatedAt = DateTime.Now,
                        Username = UserName,
                        Password = Password,
                        Role = "user",
                        FullName = "n/a"
                    };
                    App.UserRepo.SaveItem(newUser);
                    App.CurrentUser = newUser;
                    await Shell.Current.GoToAsync("//User/Home");
                    ClearControls();

                }
                else
                {
                    ShowSnackBar("User Already Exists");
                }
            }
        }

        [RelayCommand]
        private void ChangeMenu()
        {
            IsSignIn = !IsSignIn;
            UpdatePage();
        }

        #endregion



        private void UpdatePage()
        {
            if (IsSignIn)
            {
                LoginChangeHeading = "Don't have an account?";
                MainHeading = "Sign In";
                MenuName = "Sign in";
                LoginChangeButton = "Sign Up";
            }
            else
            {
                LoginChangeHeading = "Already Have an account?";
                MainHeading = "Sign Up";
                MenuName = "Sign up";
                LoginChangeButton = "Sign In";
            }
            UserName = string.Empty;
            Password = string.Empty;
        }

        private async Task UpdateFinesAsync()
        {
            await Task.Run(() =>
            {
                var transactions = App.TransactionsRepo.GetItems();
                foreach (var transaction in transactions)
                {
                    if (transaction.DueDate < DateTime.Now)
                    {
                        TimeSpan difference = DateTime.Now - transaction.DueDate;
                        int overdueDays = Math.Max(difference.Days, 1);
                        transaction.Fine = AppSettings.PerDayFineRate * overdueDays;
                        App.TransactionsRepo.SaveItem(transaction);
                    }
                }
            });
        }


        private async void CheckAdmin()
        {

            // Update fines
            await UpdateFinesAsync();


            BookNest.Models.User admin = App.UserRepo.GetItem(a => a.Username == "admin");
            if (admin == null)
            {
                BookNest.Models.User newAdmin = new BookNest.Models.User
                {
                    CreatedAt = DateTime.Now,
                    Username = AppSettings.AdminUserName,
                    Password = AppSettings.AdminPassword,
                    Role = "admin",
                    FullName = AppSettings.AdminFullName
                };
                App.UserRepo.SaveItem(newAdmin);
            }
            else if (admin != null)
            {
                if (admin?.Username != AppSettings.AdminUserName
                    || admin?.Password != AppSettings.AdminPassword
                    || admin?.FullName != AppSettings.AdminFullName)
                {
                    admin.Username = AppSettings.AdminUserName;
                    admin.Password = AppSettings.AdminPassword;
                    admin.FullName = AppSettings.AdminFullName;
                }
                App.UserRepo.SaveItem(admin);
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


        private void ClearControls()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

    }
}
