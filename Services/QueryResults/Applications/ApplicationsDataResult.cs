namespace Services.QueryResults.Applications
{
    public class ApplicationsDataResult
    {
        public long ApplicationId { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
