using ExpenseTracker.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ExpenseTracker.Models
{
    public class Auditable
    {
        public long Id { get; set; }
        [ForeignKey("CreatedByUser")]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        [ForeignKey("UpdatedByUser")]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }

        public ExpenseTrackerUser? CreatedByUser { get; set; }
        public ExpenseTrackerUser? UpdatedByUser { get; set; }
    }
}
