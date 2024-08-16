using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Services.Services.Applications;
using Services.Interfaces.Applicants;
using Services.InputValidation;
using Services.Interfaces.Applications;
using Services.Interfaces.ApplicationsDisbursed;
using Services.Interfaces.ApplicationsIncome;
using Services.Interfaces.CreditScore;
using Services.Services.Applicants;
using Services.Services.ApplicationsCreditScore;
using Services.Services.ApplicationsDisbursed;
using Services.Services.ApplicationsIncome;
using Services.Services.ApplicationsParameters;
using ConsoleApp.UserInterfaces;
using Services.InputValidations;
using ConsoleApp.Display;
using Data.Database;
using Data.Validations;
using Services.Middleware;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ExceptionHandlingMiddleware.Execute(() =>
            {
                var userInterfaceService = host.Services.GetRequiredService<UserInterface>();
                userInterfaceService.RunProgram(host.Services);
            });
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("AppContext");
                    services.AddDbContext<AppContext>(dbContextOptions => dbContextOptions
                        .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)),
                        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
                    ));

                    services.AddScoped<ApplicationsDataByApplicantTypeService>();
                    services.AddHttpContextAccessor();
                    services.AddScoped<DisbursementStateIdsValidation>();
                    services.AddScoped<ClientIdsValidation>();
                    services.AddScoped<ApplicationIdsValidation>();
                    services.AddScoped<IApplicationsTotalByApplicantTypeService, ApplicationsTotalByApplicantTypeService>();
                    services.AddScoped<IApplicantsByCreditDecisionService, ApplicantsByCreditDecisionService>();
                    services.AddScoped<IApplicantsByProductTypeService, ApplicantsByProductTypeService>();
                    services.AddScoped<IApplicationsDataByApplicantTypeService, ApplicationsDataByApplicantTypeService>();
                    services.AddScoped<IApplicationsTotalService, ApplicationsTotalService>();
                    services.AddScoped<IApplicationsByCreditDecisionService, ApplicationsByCreditDecisionService>();
                    services.AddScoped<IApplicationsByApplicationStatusService, ApplicationsByApplicationStatusService>();
                    services.AddScoped<IApplicationsByProductTypeService, ApplicationsByProductTypeService>();
                    services.AddScoped<IApplicationsByAllOptionsService, ApplicationsByAllOptionsService>();
                    services.AddScoped<IApplicationsIncomesService, ApplicationsIncomesService>();
                    services.AddScoped<IApplicationsCreditScoresService, ApplicationsCreditScoresService>();
                    services.AddScoped<IApplicationsDataService, ApplicationsDataService>();
                    services.AddScoped<IApplicationsDisbursedDTIService, ApplicationsDisbursedDTIService>();
                    services.AddScoped<IApplicationsDisbursedAPRService, ApplicationsDisbursedAPRService>();
                    services.AddScoped<IApplicationsDisbursedInterestRateService, ApplicationsDisbursedInterestRateService>();
                    services.AddScoped<IApplicationsDisbursedDataService, ApplicationsDisbursedDataService>();
                    services.AddScoped<IApplicationsDisbursedTotalAmountService, ApplicationsDisbursedTotalAmountService>();
                    services.AddScoped<IApplicationsDisbursedMonthlyTotalsService, ApplicationsDisbursedMonthlyTotalsService>();
                    services.AddScoped<IApplicationsDisbursedFinanceChargesService, ApplicationsDisbursedFinanceChargesService>();
                    services.AddScoped<UserInputValidation>();
                    services.AddScoped<VariableValidation>();
                    services.AddScoped<UserInterface>();
                    services.AddScoped<ApplicantsService>();
                    services.AddScoped<MetricService>();
                    services.AddScoped<StatusService>();
                    services.AddScoped<ApplicantsByMetricDisplay>();
                    services.AddScoped<ApplicationDataByAppIdDisplay>();
                    services.AddScoped<ApplicationsByMetricAndApplicantTypeDisplay>();
                    services.AddScoped<ApplicationsByMetricDisplay>();
                    services.AddScoped<ApplicationsDataByApplicantTypeDisplay>();
                    services.AddScoped<ApplicationsDataDisplay>();
                    services.AddScoped<ApplicationsDisbursedSumsDisplay>();
                    services.AddScoped<ApplicationsTotalByApplicantTypeDisplay>();
                    services.AddScoped<MeanAndMedianDisplay>();
                });

    }
}