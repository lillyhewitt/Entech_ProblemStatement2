
namespace Services.QueryResults.ApplicationsDisbursed
{
    public class ApplicationsDisbursedDataResult
    {
        public long ApplicationId { get; set; }
        public long DisbursementProfileId { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public DateOnly? Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
