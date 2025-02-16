using BookNest.ViewModels.Components;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BookNest.Models
{

    [Table("Fine")]

    public class Fine : TableBase
    {
        [NotNull, ForeignKey(typeof(Transaction))]
        public int TransactionId { get; set; }
        [NotNull]
        public decimal Amount { get; set; }
        [NotNull]
        public string FineStatus { get; set; } = string.Empty;
        public DateTime DateIssued { get; set; }
        public DateTime? DatePaid { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Transaction? Transaction { get; set; }
    }
    
}
