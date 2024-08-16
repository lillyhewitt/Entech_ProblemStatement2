
﻿using Services.Responses.ApplicationsDisbursed;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsDisbursedFinanceChargesService
    {
        public ApplicationsDisbursedFinanceChargesResponse CreateAppsDisbursedFinanceChargesResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
