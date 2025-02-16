using BookNest.ViewModels.Components;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BookNest.Models
{
    [Table("Author")]
    public class Author : TableBase
    {
        public string Name { get; set; } = string.Empty;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Book> Books { get; set; } = new List<Book>();
    }
    
}
