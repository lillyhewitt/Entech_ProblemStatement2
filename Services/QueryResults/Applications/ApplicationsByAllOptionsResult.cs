
namespace Services.QueryResults.Applications
{
    public class ApplicationsByAllOptionsResult
    {
        public ApplicationsByApplicationStatusResult? ApplicationStatusResults { get; set; }
        public ApplicationsByCreditDecisionsResult? CreditDecisionResults { get; set; }
        public ApplicationsByProductTypeResult? ProductTypeResults { get; set; }
    }
}
