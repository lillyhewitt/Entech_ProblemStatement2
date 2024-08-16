
namespace Services.Responses.Applications
{
    public class ApplicationsDataResponse
    {
        public long ApplicationId { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
