using ExpenseTracker.Models;
using ExpenseTracker.Vm;

namespace ExpenseTracker.Interfaces
{
    public interface IExpenseTrackService
    {
        Task<List<ClusteredExpenseVm>> TrackExpenseAsync(DateTime fromDateTimeUtc, DateTime toDateTimeUtc);
    }
}
