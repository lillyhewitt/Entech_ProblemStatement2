namespace Data.Validations
{
    public class DisbursementStateIdsValidation
    {
        private readonly List<int> disbursementStateIds = new List<int> { 28, 29, 62, 102, 103, 135, 136, 178, 179, 211, 212, 234, 271, 272, 301, 344, 345, 374 };
        //private readonly List<int> disbursementStateIds = new List<int> { 334, 335, 364 };
        public List<int> GetDisbursementStateIds()
        {
            return disbursementStateIds;
        }
    }
}