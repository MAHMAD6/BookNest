using BookNest.ViewModels.Components;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BookNest.Models
{
    [Table("Book")]
    public class Book : TableBase
    {
        [NotNull]
        public string Title { get; set; } = string.Empty;
        [Unique]
        public string ISBN { get; set; } = string.Empty;
        [NotNull, ForeignKey(typeof(Author))]
        public int AuthorId { get; set; }
        [NotNull, ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [NotNull]
        public string AvailabilityStatus { get; set; } = string.Empty;

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Author? Author { get; set; } 
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Category? Category { get; set; }
    }
    
}
