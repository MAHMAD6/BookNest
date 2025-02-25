using System.Text.RegularExpressions;

namespace BookNest.ViewModels.Components
{
    public class Validation
    {
        public static bool IsValidUsername(string username)
        {
            const string UsernamePattern = @"^[a-zA-Z0-9_]+$";
            return Regex.IsMatch(username, UsernamePattern);
        }

        public static bool IsValidPassword(string password)
        {
            const string PasswordPattern = @"^[a-zA-Z0-9]+$";
            return Regex.IsMatch(password, PasswordPattern);
        }

        public static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[A-Za-z\s\-']{3,}$");
        }
        public static bool IsValidBookTitle(string title)
        {
            return Regex.IsMatch(title, @"^[A-Za-z0-9\s.,'\""-]{2,100}$");
        }
        public static bool IsValidISBN(string isbn)
        {
            return true;
            return Regex.IsMatch(isbn, @"^(97[89])?\d{9}(\d|X)$");
        }
    }
}
