Working in C# using Visual Studio, I created various queries to collect metrics from the anovaa database using prompts from Problem Statement One and Problem Statement Two. I worked with the .NET 8 Framework, Swagger UI, and MySQL to complete this project. 

# Problem Statement #2
## USE CASE
As a development manager at a loan originations provider, I require a web-based application that enables me to search data within specified date ranges. As an example, between a user specified start and end date, this application should return the mean and median credit scores of borrowers and cosigners. The tool should be able to output and display data to the end user.


# Problem Statement #1
## USE CASE
As the Director of Lending, I require the capability to deliver comprehensive metrics on clients' credit and risk portfolios. Based on data stored within the system, please create meaningful reports that will assist me in determining the below information:

For our client Anovaa, 
* Total loans submitted all time
  - Show graph that displays number of loans submitted by time of application
  - Deconstructed by credit decisions - Examples; Denied, Conditional Approval, Approval, etc.
  - Deconstructed by application statuses - Examples; Denied, Disbursed, Rejected, etc.
  - Deconstructed by product types
  - Deconstructed by single applicant vs joint applications
    - Analyze single applicant and joint credit decisions separately
    - Analyze single applicant and joint product types separately
  - Mean and median credit scores for borrower and cosigners
  - Mean and median stated or verified income amounts for borrower and cosigners
* Total funds disbursed all time
  - Deconstruct by time of disbursement
  - For loans that have been disbursed, analyze credit information to determine the following;
    - Average credit score for borrower and cosigner
    - Average income for borrower and cosigner (identify borrowers who don’t have income)
    - Average DTI for borrowers and cosigners
  - Identify the following values
    - Total finance charges across all disbursed loans
    - Mean and Median Interest rate and APRs across all disbursed loans

## ⭐ Bonus Points: 
Based on loans that have been disbursed and information gathered, assess the following;
* Determine borrower’s ability to repay
* Determine probability of default based on credit data and calculated DTIs
* Assess exposure at default for client meaning total value at risk is a borrower defaults (value at risk would be loan amount funded to borrower that will not be repaid)
* Review maximum potential loss over a give time period with varying confidence intervals
* Review performance if adverse economic conditions were to occur
* Review portfolio concentration in particular sectors, geographic areas, etc.

