using BookNest.ViewModels.Components;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BookNest.Models
{
    [Table("User")]
    public class User : TableBase
    {

        [Unique, NotNull]
        public string Username { get; set; } = string.Empty;
        [NotNull]
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Transaction> Transactions { get; set; }
    }
    
}
