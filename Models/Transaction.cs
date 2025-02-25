using BookNest.Models;
using BookNest.ViewModels.Components;
using SQLite;
using SQLiteNetExtensions.Attributes;

[Table("Transaction")]
public class Transaction : TableBase
{
    [NotNull, ForeignKey(typeof(User))]
    public int UserId { get; set; }

    [NotNull, ForeignKey(typeof(Book))]
    public int BookId { get; set; }

    [NotNull]
    public string Status { get; set; } = string.Empty;

    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal? Fine { get; set; } = null;

    [OneToOne(CascadeOperations = CascadeOperation.All)]
    public Fine? FineDetails { get; set; }  // One-to-One relationship

    [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
    public User? User { get; set; }

    [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
    public Book? Book { get; set; }
}
