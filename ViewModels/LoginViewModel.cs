using BookNest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookNest.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        #region PROPERTIES
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
            else
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


            // UI 
            IsSignIn = true;
            UpdatePage();
        }




        #region COMMANDS

        [RelayCommand]
        private async void ConfirmLogin()
        {
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
                            await Shell.Current.GoToAsync("//Admin");
                        }
                        else
                        {
                            // In case of Regular user
                            await Shell.Current.GoToAsync("//User");
                        }
                    }
                    else
                    {
                        // Wrong Password

                    }
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
                    await Shell.Current.GoToAsync("//User");

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
    }
}
