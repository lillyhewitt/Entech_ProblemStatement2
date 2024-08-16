using Microsoft.EntityFrameworkCore;
using Services.Interfaces.Applicants;
using Services.Interfaces.Applications;
using Services.Interfaces.ApplicationsDisbursed;
using Services.Interfaces.CreditScore;
using Services.Interfaces.ApplicationsIncome;
using Services.Services.Applicants;
using Services.Services.Applications;
using Services.Services.ApplicationsDisbursed;
using Services.Services.ApplicationsCreditScore;
using Services.Services.ApplicationsIncome;
using Services.Middleware;
using Services.Services.ApplicationsParameters;
using Services.InputValidation;
using Data.Database;
using Data.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppContext");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppContext>(dbContextOptions => dbContextOptions
    .UseMySql(connectionString, serverVersion)
);

builder.Services.AddScoped<ApplicationsDataByApplicantTypeService>();
builder.Services.AddScoped<DisbursementStateIdsValidation>();
builder.Services.AddScoped<ClientIdsValidation>();
builder.Services.AddScoped<ApplicationIdsValidation>();
builder.Services.AddScoped<IApplicationsDataByApplicantTypeService, ApplicationsDataByApplicantTypeService>();
builder.Services.AddScoped<IApplicantsByCreditDecisionService, ApplicantsByCreditDecisionService>();
builder.Services.AddScoped<IApplicantsByProductTypeService, ApplicantsByProductTypeService>();
builder.Services.AddScoped<IApplicationsByAllOptionsService, ApplicationsByAllOptionsService>();
builder.Services.AddScoped<IApplicationsDataByApplicantTypeService, ApplicationsDataByApplicantTypeService>();
builder.Services.AddScoped<IApplicationsDisbursedDataService, ApplicationsDisbursedDataService>();
builder.Services.AddScoped<IApplicationsDataService, ApplicationsDataService>();
builder.Services.AddScoped<IApplicationsCreditScoresService, ApplicationsCreditScoresService>();
builder.Services.AddScoped<IApplicationsDisbursedAPRService, ApplicationsDisbursedAPRService>();
builder.Services.AddScoped<IApplicationsDisbursedDTIService, ApplicationsDisbursedDTIService>();
builder.Services.AddScoped<IApplicationsDisbursedInterestRateService, ApplicationsDisbursedInterestRateService>();
builder.Services.AddScoped<IApplicationsDisbursedMonthlyTotalsService, ApplicationsDisbursedMonthlyTotalsService>();
builder.Services.AddScoped<IApplicationsDisbursedTotalAmountService, ApplicationsDisbursedTotalAmountService>();
builder.Services.AddScoped<IApplicationsDisbursedFinanceChargesService, ApplicationsDisbursedFinanceChargesService>();
builder.Services.AddScoped<IApplicationsIncomesService, ApplicationsIncomesService>();
builder.Services.AddScoped<IApplicationsByApplicationStatusService, ApplicationsByApplicationStatusService>();
builder.Services.AddScoped<IApplicationsByCreditDecisionService, ApplicationsByCreditDecisionService>();
builder.Services.AddScoped<IApplicationsByProductTypeService, ApplicationsByProductTypeService>();
builder.Services.AddScoped<IApplicationsTotalService, ApplicationsTotalService>();
builder.Services.AddScoped<IApplicationsTotalByApplicantTypeService, ApplicationsTotalByApplicantTypeService>();
builder.Services.AddScoped<UserInputValidation>();
builder.Services.AddScoped<ApplicantsService>();
builder.Services.AddScoped<MetricService>();
builder.Services.AddScoped<StatusService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionsMiddlewareAttribute>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();