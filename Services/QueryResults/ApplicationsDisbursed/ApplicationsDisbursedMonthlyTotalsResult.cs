
namespace Services.QueryResults.ApplicationsDisbursed
{
    public class ApplicationsDisbursedMonthlyTotalsResult
    {
        public long ApplicationId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? Amount { get; set; }
    }
}
