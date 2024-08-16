using Data.Validations;
using Services.Enums;
using Services.Extensions;

namespace Services.InputValidation
{
    public class UserInputValidation
    {
        protected ClientIdsValidation _clientIdsValidate;
        protected ApplicationIdsValidation _applicationIdsValidate;

        public UserInputValidation(ClientIdsValidation clientIdValidation, ApplicationIdsValidation applicationIdsValidation)
        {
            _clientIdsValidate = clientIdValidation;
            _applicationIdsValidate = applicationIdsValidation;
        }
        public void ValidateIds(int clientId, long? applicationId = null)
        {
            // throw exception if clientId is not a valid Id
            if (!_clientIdsValidate.IsIdAClientID(clientId))
            {
                throw new ArgumentException("Client ID not found");
            }
            // throw exception if applicationId is not a valid Id
            else if (applicationId != null && !_applicationIdsValidate.IsAApplicationId(applicationId))
            {
                throw new ArgumentException("Application ID not found");
            }
        }

        // check if status is all or disbursed
        public void ValidateStatus(StatusTypes status)
        {
            if (status != StatusTypes.All && status != StatusTypes.Disbursed)
            {
                throw new ArgumentException("Status is not valid. Use all or disbursed in the future.");
            }
        }

        // check if status is all
        public void ValidateAllStatus(StatusTypes status)
        {
            if (status != StatusTypes.All)
            {
                throw new ArgumentException("Status is not valid. Use all in the future.");
            }
        }

        // cyheck if status is disbursed
        public void ValidateDisbursedStatus(StatusTypes status)
        {
            if (status != StatusTypes.Disbursed)
            {
                throw new ArgumentException("Status is not valid. Use disbursed in the future.");
            }
        }

        // check if ranges are valid 
        public void ValidateRanges(decimal? startRange, decimal? endRange)
        {
            if (startRange < 0)
            {
                throw new ArgumentException("Start range cannot be negative.");
            }
            else if (endRange < 0)
            {
                throw new ArgumentException("End range cannot be negative.");
            }
            else if (startRange > endRange)
            {
                throw new ArgumentException("Start range cannot be greater than end range.");
            }
        }

        // check if start and end date are valid
        public void ValidateDates(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null && endDate != null && startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be greater than end date.");
            }
        }

        // convert dates using extension method
        public DateOnly? ToDateOnly(DateTime? date)
        {
            return date.ToDateOnly();
        }
    }
}