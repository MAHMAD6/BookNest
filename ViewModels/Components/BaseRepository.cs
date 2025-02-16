using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq.Expressions;

namespace BookNest.ViewModels.Components
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : TableBase, new()
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }

        public BaseRepository()
        {
            connection =
                 new SQLiteConnection(AppSettings.DbPath,
                 AppSettings.DbFlags);
            connection.CreateTable<T>();
        }

        public void DeleteItem(T? item)
        {
            try
            {
                //connection.Delete(item);
                connection.Delete(item, true);
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            connection.Close();
        }


        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>()
                     .Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
            return null;
        }
        public T GetItemWithChildren(Expression<Func<T, bool>> predicate, bool recursive = true)
        {
            try
            {
                var item = connection.Table<T>().Where(predicate).FirstOrDefault();
                if (item != null)
                {
                    connection.GetChildren(item, recursive);
                }
                return item;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return null;
        }



        public List<T> GetItems(Expression<Func<T, bool>>? predicate = null)
        {
            try
            {
                return predicate == null
                    ? connection.Table<T>().ToList()  
                    : connection.Table<T>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return new List<T>(); 
        }

        public List<T> GetItemsWithChildren(Expression<Func<T, bool>>? predicate = null, bool recursive = true)
        {
            try
            {
                var items = predicate == null
                    ? connection.Table<T>().ToList()  
                    : connection.Table<T>().Where(predicate).ToList(); 

                foreach (var item in items)
                {
                    connection.GetChildren(item, recursive);
                }
                return items;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            return new List<T>(); // Return an empty list instead of null to avoid null reference issues
        }


        public void SaveItemWithChildren(T item, bool recursive = false)
        {
            try
            {
                var existingItem = connection.Table<T>().FirstOrDefault(x => x.Id == item.Id);
                if (existingItem != null)
                {
                    connection.UpdateWithChildren(item);
                }
                else
                {
                    connection.InsertWithChildren(item, recursive);
                }
            }
            catch (SQLiteException ex) when (ex.Result == SQLite3.Result.Constraint)
            {
                StatusMessage = "Error: Duplicate entry detected.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }



        public void SaveItem(T? item)
        {
            int result = 0;
            try
            {
                if (item.Id != 0)
                {
                    result =
                         connection.Update(item);
                    StatusMessage =
                         $"{result} row(s) updated";
                }
                else
                {
                    result = connection.Insert(item);
                    StatusMessage =
                         $"{result} row(s) added";
                }

            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }

    }
}
