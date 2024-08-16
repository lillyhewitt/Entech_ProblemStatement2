using ConsoleApp.Display;

namespace ConsoleApp.UserInterfaces
{
    public class UserInterface
    {
        private readonly ApplicationsDataByApplicantTypeDisplay _applicantDataDisplay;
        private readonly ApplicationsTotalByApplicantTypeDisplay _applicantCountDisplay;
        private readonly ApplicantsByMetricDisplay _applicantFindByAppIdDisplay;
        private readonly ApplicationsDataDisplay _applicationsDataDisplay;
        private readonly ApplicationDataByAppIdDisplay _applicationsByAppIdDisplay;
        private readonly MeanAndMedianDisplay _meanAndMedianDisplay;
        private readonly ApplicationsByMetricDisplay _applicationsCountDisplay;
        private readonly ApplicationsByMetricAndApplicantTypeDisplay _applicationsFindByAppIdDisplay;
        private readonly ApplicationsDisbursedSumsDisplay _applicationsDisbursedSumsDisplay;

        public UserInterface(
            ApplicationsDataByApplicantTypeDisplay applicantDataResponseService,
            ApplicationsTotalByApplicantTypeDisplay applicantCountResponseService,
            ApplicantsByMetricDisplay applicantFindByAppIdResponseService,
            ApplicationsDataDisplay applicationsResponseService,
            ApplicationDataByAppIdDisplay applicationsByAppIdResponseService,
            MeanAndMedianDisplay meanAndMedianResponseService,
            ApplicationsByMetricDisplay applicationsCountResponseService,
            ApplicationsByMetricAndApplicantTypeDisplay applicationsFindByAppIdResponseService,
            ApplicationsDisbursedSumsDisplay applicationsDisbursedSumsResponseService)
        {
            _applicantDataDisplay = applicantDataResponseService;
            _applicantCountDisplay = applicantCountResponseService;
            _applicantFindByAppIdDisplay = applicantFindByAppIdResponseService;
            _applicationsDataDisplay = applicationsResponseService;
            _applicationsByAppIdDisplay = applicationsByAppIdResponseService;
            _meanAndMedianDisplay = meanAndMedianResponseService;
            _applicationsCountDisplay = applicationsCountResponseService;
            _applicationsFindByAppIdDisplay = applicationsFindByAppIdResponseService;
            _applicationsDisbursedSumsDisplay = applicationsDisbursedSumsResponseService;
        }

        // runs the basic interface for the user to input data
        public void RunProgram(IServiceProvider services)
        {
            while (true)
            {
                int path = PromptForPath();

                if (path < 1 || path > 11)
                {
                    Console.WriteLine("Must choose a number between 1 and 11. No other paths exist at this time.");
                    if (!PromptForRetry())
                    {
                        break;
                    }
                }

                ProcessPath(path);

                if (!PromptForContinuation())
                {
                    break;
                }
            }
        }

        // asks the user for the path they would like to use
        private int PromptForPath()
        {
            Console.WriteLine("\nHello, please input what path you would like to use to find specific metrics:" +
                "\n\t1. applicants" +
                "\n\t2. applicants/{applicationId}" +
                "\n\t3. applicants/count" +
                "\n\t4. applicants/{applicationId}/find" +
                "\n\t5. applications/{status}" +
                "\n\t6. applications/{status}/{applicationId}" +
                "\n\t7. applications/{status}/mean-and-median" +
                "\n\t8. applications/{status}/count" +
                "\n\t9. applications/{status}/{applicationId}/find" +
                "\n\t10. applications/{status}/sum" +
                "\n\t11. applications/{status}/{applicationId}/sum");

            return Convert.ToInt32(Console.ReadLine());
        }

        // call display class based on which path is inputed by user
        private void ProcessPath(int path)
        {
            switch (path)
            {
                case 1:
                    _applicantDataDisplay.ProcessApplicants();
                    break;
                case 2:
                    _applicantDataDisplay.ProcessApplicantById();
                    break;
                case 3:
                    _applicantCountDisplay.ProcessApplicantCount();
                    break;
                case 4:
                    _applicantFindByAppIdDisplay.ProcessApplicantFind();
                    break;
                case 5:
                    _applicationsDataDisplay.ProcessApplicationsByStatus();
                    break;
                case 6:
                    _applicationsByAppIdDisplay.ProcessApplicationByIdAndStatus();
                    break;
                case 7:
                    _meanAndMedianDisplay.ProcessApplicationsMeanAndMedian();
                    break;
                case 8:
                    _applicationsCountDisplay.ProcessApplicationsCountByStatus();
                    break;
                case 9:
                    _applicationsFindByAppIdDisplay.ProcessApplicationFindByStatus();
                    break;
                case 10:
                    _applicationsDisbursedSumsDisplay.ProcessApplicationsSumByStatus();
                    break;
                case 11:
                    _applicationsDisbursedSumsDisplay.ProcessApplicationSumByIdAndStatus();
                    break;
            }
        }

        // creates prompt asking the user if they would like to run another query
        private bool PromptForContinuation()
        {
            return ReadAnswer("\nWould you like to run another query? (Yes/No)");
        }

        // creates prompt asking the user if they would like to retry the query
        private bool PromptForRetry()
        {
            return ReadAnswer("\nWould you like to try again? (Yes/No)");
        }

        // reads the user's input and returns a boolean based on the input
        private bool ReadAnswer(string message)
        {
            Console.WriteLine(message);
            if (Console.ReadLine()?.ToLower() == "yes")
            {
                return true;
            }
            return false;
        }
    }
}