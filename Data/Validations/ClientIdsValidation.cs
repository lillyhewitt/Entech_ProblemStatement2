namespace Data.Validations
{
    public class ClientIdsValidation
    {
        private readonly List<int> clientIds = new List<int> { 1, 2, 3, 4, 7 };

        public List<int> GetClientIds()
        {
            return clientIds;
        }

        public bool IsIdAClientID(int clientId)
        {
            if (clientIds.Contains(clientId))
            {
                return true;
            }
            return false;
        }
    }
}