
namespace Services.Responses.ApplicationsDisbursed
{
    public class ApplicationsDisbursedMonthlyTotalsResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? Amount { get; set; }
    }
}
