
namespace Services.QueryResults.Applications
{
    public class ApplicationsByApplicationStatusResult
    {
        public long ApplicationId { get; set; }
        public string? StatusName { get; set; }
        public int TotalNum { get; set; }
    }
}
