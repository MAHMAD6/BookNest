using BookNest.Models;
using System.Globalization;

namespace BookNest.ViewModels.Components
{
    public class TransactionMessageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                string title = (values[0] as Book)?.Title ?? string.Empty;
                Book? book = App.BooksRepo.GetItemWithChildren(b => b.Title  == title);
             if(parameter == null)
                return $"{values[1]} has requested for book '{book?.Title}' by {book?.Author?.Name}.";
             else
                {
                    string response = (values[1] as string) == "Assigned-UserWait" ? "Accepted" : "Rejected";
                  return $"Request for book '{book?.Title}' by {book?.Author?.Name} is {response}";
                }
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
