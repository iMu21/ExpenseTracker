using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Expense : Auditable
    {
        public decimal Amount { get; set; }
        public DateTime ExpenseDateTimeUtc { get; set; }
        [ForeignKey("Category")]
        public long? CategoryId { get; set; }
        public Category? Category { get; set; }


        public IEnumerable<Category>? Categories { get; set; }
    }
}
