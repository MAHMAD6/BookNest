using System.Linq.Expressions;

namespace BookNest.ViewModels.Components
{
    public interface IBaseRepository<T> : IDisposable
        where T : TableBase, new()
    {
 
        T GetItem(Expression<Func<T, bool>> predicate);
        public T GetItemWithChildren(Expression<Func<T, bool>> predicate, bool recursive);
        List<T> GetItems(Expression<Func<T, bool>> predicate);
        public List<T> GetItemsWithChildren(Expression<Func<T, bool>> predicate, bool recursive);


        void SaveItem(T item);
        void SaveItemWithChildren(T item, bool recursive = false);

        void DeleteItem(T item);
    }
}
