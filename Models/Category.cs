using BookNest.ViewModels.Components;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BookNest.Models
{

    [Table("Category")]
    public class Category : TableBase
    {
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Book> Books { get; set; }
    }

}
