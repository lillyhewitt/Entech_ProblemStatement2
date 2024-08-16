namespace Data.Validations
{
    public class ApplicationIdsValidation
    {
        // if client Id is 1
        private readonly long minId = 12051874;
        private readonly long maxId = 12052927;

        public long GetMinId()
        {
            return minId;
        }

        public long GetMaxId()
        {
            return maxId;
        }

        public bool IsAApplicationId(long? id)
        {
            if (id >= minId && id <= maxId)
            {
                return true;
            }
            return false;
        }
    }
}