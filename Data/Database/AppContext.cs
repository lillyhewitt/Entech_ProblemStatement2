using Microsoft.EntityFrameworkCore;
using Data.Tables;

namespace Data.Database;

public partial class AppContext : DbContext
{
    public AppContext()
    {
    }

    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationConsolidation> ApplicationConsolidations { get; set; }

    public virtual DbSet<ApplicationDisbursement> ApplicationDisbursements { get; set; }

    public virtual DbSet<ApplicationPrice> ApplicationPrices { get; set; }

    public virtual DbSet<ApplicationStateManagement> ApplicationStateManagements { get; set; }

    public virtual DbSet<CreditDecision> CreditDecisions { get; set; }

    public virtual DbSet<CreditResponse> CreditResponses { get; set; }

    public virtual DbSet<CreditScore> CreditScores { get; set; }

    public virtual DbSet<DisbursementStateManagement> DisbursementStateManagements { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<ProcessState> ProcessStates { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder
            .UseLazyLoadingProxies()
            .UseMySql("server=REDACTED;user=REDACTED;password=REDACTED;database=REDACTED", ServerVersion.Parse("10.6.16-mariadb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Applicant", tb => tb.HasComment("Employee Id to identify the employee who entered the application of behalf of the customer."));

            entity.HasIndex(e => e.BorrowerTypeId, "FK_Applicant_BorrowerType");

            entity.HasIndex(e => e.BranchId, "FK_Applicant_Branch");

            entity.HasIndex(e => e.CitizenshipTypeId, "FK_Applicant_CitizenshipType");

            entity.HasIndex(e => e.ClientId, "FK_Applicant_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_Applicant_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_Applicant_DeletedBy");

            entity.HasIndex(e => e.EmployeeId, "FK_Applicant_Employee");

            entity.HasIndex(e => e.EntityRolesId, "FK_Applicant_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_Applicant_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_Applicant_OwnedBy");

            entity.HasIndex(e => e.UserProfileId, "FK_Applicant_UserProfile");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.AlienId).HasMaxLength(255);
            entity.Property(e => e.BorrowerTypeId).HasColumnType("int(11)");
            entity.Property(e => e.BranchId)
                .HasComment("Branch Id to identify the branch that the application was entered at.")
                .HasColumnType("int(11)");
            entity.Property(e => e.CitizenshipTypeId).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.EmployeeId)
                .HasComment("Employee Id to identify the employee who entered the application of behalf of the customer.")
                .HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.Mla)
                .HasColumnType("bit(1)")
                .HasColumnName("MLA");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.Ssn)
                .HasMaxLength(255)
                .HasColumnName("SSN");
            entity.Property(e => e.SsnlastFour)
                .HasMaxLength(255)
                .HasColumnName("SSNLastFour");
            entity.Property(e => e.UserProfileId).HasColumnType("int(11)");

            entity.HasMany(d => d.Incomes).WithMany(p => p.Applicants)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicantIncome",
                    r => r.HasOne<Income>().WithMany()
                        .HasForeignKey("IncomeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ApplicantIncome_Income"),
                    l => l.HasOne<Applicant>().WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ApplicantIncome_Applicant"),
                    j =>
                    {
                        j.HasKey("ApplicantId", "IncomeId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("ApplicantIncome");
                        j.HasIndex(new[] { "IncomeId" }, "FK_ApplicantIncome_Income");
                        j.IndexerProperty<int>("ApplicantId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("IncomeId").HasColumnType("int(11)");
                    });
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Application", tb => tb.HasComment("Indicates the source of this application, e.g. Branch vs Apply"));

            entity.HasIndex(e => e.ApplicationSourceTypeId, "FK_Application_ApplicationSourceType");

            entity.HasIndex(e => e.ClientId, "FK_Application_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_Application_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_Application_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_Application_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_Application_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_Application_OwnedBy");

            entity.HasIndex(e => e.ProductId, "FK_Application_Product");

            entity.HasIndex(e => e.ServicerAssignmentId, "FK_Application_ServicerAssignment");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ApplicationSourceTypeId)
                .HasComment("Indicates the source of this application, e.g. Branch vs Apply")
                .HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.ConformanceVerifiedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.CustomerTimeZone).HasMaxLength(255);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.HasCosigner)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsMembershipVerified)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsServicerExtract).HasColumnType("bit(1)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.ReferralUrl).HasMaxLength(2048);
            entity.Property(e => e.ServicerAssignmentId).HasColumnType("int(11)");

            entity.HasOne(d => d.Product).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Product");

            entity.HasMany(d => d.Applicants).WithMany(p => p.Applications)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicationApplicant",
                    r => r.HasOne<Applicant>().WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ApplicationApplicant_Applicant"),
                    l => l.HasOne<Application>().WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ApplicationApplicant_Application"),
                    j =>
                    {
                        j.HasKey("ApplicationId", "ApplicantId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("ApplicationApplicant");
                        j.HasIndex(new[] { "ApplicantId" }, "FK_ApplicationApplicant_Applicant");
                        j.IndexerProperty<int>("ApplicationId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("ApplicantId").HasColumnType("int(11)");
                    });
        });

        modelBuilder.Entity<ApplicationConsolidation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ApplicationConsolidation");

            entity.HasIndex(e => e.ApplicationId, "FK_ApplicationConsolidation_Application");

            entity.HasIndex(e => e.ApplicationDisbursementId, "FK_ApplicationConsolidation_ApplicationDisbursement");

            entity.HasIndex(e => e.ClientId, "FK_ApplicationConsolidation_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_ApplicationConsolidation_CreatedBy");

            entity.HasIndex(e => e.CreditorProfileId, "FK_ApplicationConsolidation_CreditorProfile");

            entity.HasIndex(e => e.DeletedBy, "FK_ApplicationConsolidation_DeletedBy");

            entity.HasIndex(e => e.DocumentId, "FK_ApplicationConsolidation_Document");

            entity.HasIndex(e => e.EntityRolesId, "FK_ApplicationConsolidation_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_ApplicationConsolidation_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_ApplicationConsolidation_OwnedBy");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AccountNumber).HasMaxLength(64);
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ApplicationDisbursementId).HasColumnType("int(11)");
            entity.Property(e => e.ApplicationId).HasColumnType("int(11)");
            entity.Property(e => e.CalculatedPayoffAmount).HasPrecision(18, 8);
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.ConfirmedLoanBalance).HasPrecision(18, 8);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.CreditorProfileId).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.DocumentId).HasColumnType("bigint(20)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.EstimatedLoanBalance).HasColumnType("int(11)");
            entity.Property(e => e.InterestRate).HasPrecision(18, 8);
            entity.Property(e => e.IsFederal)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LoanLineIndex).HasColumnType("int(11)");
            entity.Property(e => e.LoanSequenceNumber).HasMaxLength(20);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.MonthlyPayment).HasPrecision(18, 8);
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.SelectedCreditorName).HasMaxLength(255);

            entity.HasOne(d => d.ApplicationDisbursement).WithMany(p => p.ApplicationConsolidations)
                .HasForeignKey(d => d.ApplicationDisbursementId)
                .HasConstraintName("FK_ApplicationConsolidation_ApplicationDisbursement");

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationConsolidations)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationConsolidation_Application");
        });

        modelBuilder.Entity<ApplicationDisbursement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ApplicationDisbursement", tb => tb.HasComment("Pre-Disbursement Reinstated Amount"));

            entity.HasIndex(e => e.ApplicationId, "FK_ApplicationDisbursement_Application");

            entity.HasIndex(e => e.ClientId, "FK_ApplicationDisbursement_Client");

            entity.HasIndex(e => e.CreatedById, "FK_ApplicationDisbursement_CreatedBy");

            entity.HasIndex(e => e.DeletedById, "FK_ApplicationDisbursement_DeletedBy");

            entity.HasIndex(e => e.DisbursementMethodTypeId, "FK_ApplicationDisbursement_DisbursementMethodType");

            entity.HasIndex(e => e.DisbursementProfileId, "FK_ApplicationDisbursement_DisbursementProfile");

            entity.HasIndex(e => e.DisbursementSegmentTypeId, "FK_ApplicationDisbursement_DisbursementSegmentType");

            entity.HasIndex(e => e.DisbursingAgentId, "FK_ApplicationDisbursement_DisbursingAgent");

            entity.HasIndex(e => e.EntityRolesId, "FK_ApplicationDisbursement_EntityRoles");

            entity.HasIndex(e => e.ModifiedById, "FK_ApplicationDisbursement_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_ApplicationDisbursement_OwnedBy");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ApplicationId).HasColumnType("int(11)");
            entity.Property(e => e.CheckNumber).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedById).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedById).HasColumnType("int(11)");
            entity.Property(e => e.DisbursementMethodTypeId).HasColumnType("int(11)");
            entity.Property(e => e.DisbursementProfileId).HasColumnType("int(11)");
            entity.Property(e => e.DisbursementSegmentTypeId).HasColumnType("int(11)");
            entity.Property(e => e.DisbursingAgentId).HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.GrossAmount).HasPrecision(10, 2);
            entity.Property(e => e.HoldFlag).HasColumnType("bit(1)");
            entity.Property(e => e.IsEstimate).HasColumnType("bit(1)");
            entity.Property(e => e.IsReinstated)
                .HasDefaultValueSql("b'0'")
                .HasComment("A 1-character code from CL4 translated to a bit indicating if the previously cancelled disbursement is being reinstated. Y, reinstate previously cancelled disbursement, N = No, this is not a reinstatement")
                .HasColumnType("bit(1)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedById).HasColumnType("int(11)");
            entity.Property(e => e.NetAmount).HasPrecision(10, 2);
            entity.Property(e => e.OriginationFee).HasPrecision(10, 2);
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.PaidCode).HasMaxLength(1);
            entity.Property(e => e.PostDisbCancelledAmount).HasPrecision(10, 2);
            entity.Property(e => e.PostDisbReinstatedAmount)
                .HasPrecision(18, 8)
                .HasComment("Post-Disbursement Reinstated Amount");
            entity.Property(e => e.PreDisbCancelledAmount).HasPrecision(10, 2);
            entity.Property(e => e.PreDisbReinstatedAmount)
                .HasPrecision(18, 8)
                .HasComment("Pre-Disbursement Reinstated Amount");
            entity.Property(e => e.RecommendedAmount).HasPrecision(10, 2);
            entity.Property(e => e.SegmentNumber).HasColumnType("int(11)");

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationDisbursements)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationDisbursement_Application");
        });

        modelBuilder.Entity<ApplicationPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ApplicationPrice", tb => tb.HasComment("Interest accrued during the loan grace period."));

            entity.HasIndex(e => e.ApplicationId, "FK_ApplicationPrice_Application");

            entity.HasIndex(e => e.ApplicationPriceStatusId, "FK_ApplicationPrice_ApplicationPriceStatus");

            entity.HasIndex(e => e.ClientId, "FK_ApplicationPrice_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_ApplicationPrice_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_ApplicationPrice_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_ApplicationPrice_EntityRoles");

            entity.HasIndex(e => e.OfferId, "FK_ApplicationPrice_Offer");

            entity.HasIndex(e => e.OwnedById, "FK_ApplicationPrice_OwnedBy");

            entity.HasIndex(e => e.PriceId, "FK_ApplicationPrice_Price");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ApplicationId).HasColumnType("int(11)");
            entity.Property(e => e.ApplicationPriceStatusId).HasColumnType("int(11)");
            entity.Property(e => e.Apr)
                .HasPrecision(18, 8)
                .HasColumnName("APR");
            entity.Property(e => e.CalculatedRate).HasPrecision(18, 8);
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.CustomDeferredInterest).HasPrecision(18, 8);
            entity.Property(e => e.DeferredInterestAccrual).HasPrecision(18, 8);
            entity.Property(e => e.DeferredMonthlyPayment)
                .HasPrecision(18, 8)
                .HasComment("Monthly payment due during the loan deferment period.");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.Dti)
                .HasPrecision(18, 8)
                .HasColumnName("DTI");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.FixedRate).HasPrecision(18, 8);
            entity.Property(e => e.GraceInterestAccrual)
                .HasPrecision(18, 8)
                .HasComment("Interest accrued during the loan grace period.");
            entity.Property(e => e.GraceMonthlyPayment)
                .HasPrecision(18, 8)
                .HasComment("Monthly payment due during the loan grace period.");
            entity.Property(e => e.IndexedRate).HasPrecision(18, 8);
            entity.Property(e => e.InterestRateTypeCode).HasMaxLength(10);
            entity.Property(e => e.LastMonthlyPayment).HasPrecision(18, 8);
            entity.Property(e => e.MarginRate).HasPrecision(18, 8);
            entity.Property(e => e.NumberOfPayments).HasColumnType("int(11)");
            entity.Property(e => e.OfferId).HasColumnType("int(11)");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.PriceId).HasColumnType("bigint(20)");
            entity.Property(e => e.RepaymentTypeCode).HasMaxLength(20);
            entity.Property(e => e.SelectedDate).HasColumnType("datetime");
            entity.Property(e => e.Term).HasColumnType("int(11)");
            entity.Property(e => e.TotalLoan).HasPrecision(18, 8);
            entity.Property(e => e.TotalMonthlyPayment).HasPrecision(18, 8);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationPrices)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationPrice_Application");
        });

        modelBuilder.Entity<ApplicationStateManagement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ApplicationStateManagement");

            entity.HasIndex(e => e.ApplicationStateId, "FK_ApplicationStateManagement_ApplicationState");

            entity.HasIndex(e => e.ClientId, "FK_ApplicationStateManagement_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_ApplicationStateManagement_CreatedBy");

            entity.HasIndex(e => e.DecisionStateId, "FK_ApplicationStateManagement_DecisionState");

            entity.HasIndex(e => e.DeletedBy, "FK_ApplicationStateManagement_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_ApplicationStateManagement_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_ApplicationStateManagement_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_ApplicationStateManagement_OwnedBy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ApplicationStateId).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.DecisionStateId).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");

            entity.HasOne(d => d.ApplicationState).WithMany(p => p.ApplicationStateManagementApplicationStates)
                .HasForeignKey(d => d.ApplicationStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationStateManagement_ApplicationState");

            entity.HasOne(d => d.DecisionState).WithMany(p => p.ApplicationStateManagementDecisionStates)
                .HasForeignKey(d => d.DecisionStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationStateManagement_DecisionState");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ApplicationStateManagement)
                .HasForeignKey<ApplicationStateManagement>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationStateManagement_Application");
        });

        modelBuilder.Entity<CreditDecision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CreditDecision");

            entity.HasIndex(e => e.ApplicationId, "FK_CreditDecision_Application");

            entity.HasIndex(e => e.ClientId, "FK_CreditDecision_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_CreditDecision_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_CreditDecision_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_CreditDecision_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_CreditDecision_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_CreditDecision_OwnedBy");

            entity.HasIndex(e => e.SystemDecisionTypeId, "FK_CreditDecision_SystemDecisionType");

            entity.HasIndex(e => e.UnderwriterDecisionTypeId, "FK_CreditDecision_UnderwriterDecisionType");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ApplicationId).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.ProviderDecision).HasColumnType("int(11)");
            entity.Property(e => e.ProviderDecisionDescription).HasMaxLength(255);
            entity.Property(e => e.SystemDecisionTypeId).HasColumnType("int(11)");
            entity.Property(e => e.UnderwriterDecisionTypeId).HasColumnType("int(11)");

            entity.HasOne(d => d.Application).WithMany(p => p.CreditDecisions)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditDecision_Application");
        });

        modelBuilder.Entity<CreditResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CreditResponse");

            entity.HasIndex(e => e.ApplicantId, "FK_CreditResponse_Applicant");

            entity.HasIndex(e => e.ApplicationId, "FK_CreditResponse_Application");

            entity.HasIndex(e => e.BorrowerTypeId, "FK_CreditResponse_BorrowerType");

            entity.HasIndex(e => e.ClientId, "FK_CreditResponse_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_CreditResponse_CreatedBy");

            entity.HasIndex(e => e.CreditReportId, "FK_CreditResponse_CreditReport");

            entity.HasIndex(e => e.CreditScore, "FK_CreditResponse_CreditScore");

            entity.HasIndex(e => e.DeletedBy, "FK_CreditResponse_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_CreditResponse_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_CreditResponse_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_CreditResponse_OwnedBy");

            entity.HasIndex(e => e.ProviderId, "FK_CreditResponse_Provider");

            entity.HasIndex(e => e.ResidenceTypeId, "FK_CreditResponse_ResidenceType");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ApplicantId).HasColumnType("int(11)");
            entity.Property(e => e.ApplicationId).HasColumnType("int(11)");
            entity.Property(e => e.BorrowerTypeId).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.CreditReportId).HasColumnType("int(11)");
            entity.Property(e => e.CreditResult).HasColumnType("int(11)");
            entity.Property(e => e.CreditResultDescription).HasMaxLength(255);
            entity.Property(e => e.CreditScore).HasColumnType("int(11)");
            entity.Property(e => e.DecisionDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.Dti)
                .HasPrecision(18, 8)
                .HasColumnName("DTI");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.FraudFlag)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.FreeCashFlow).HasPrecision(18, 8);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.MonthlyHousingExpenses).HasPrecision(18, 8);
            entity.Property(e => e.MonthlyInstallmentExpenses).HasPrecision(18, 8);
            entity.Property(e => e.MonthlyLineOfCreditDebt).HasPrecision(18, 8);
            entity.Property(e => e.MonthlyOpenExpenses).HasPrecision(18, 8);
            entity.Property(e => e.MonthlyRevolvingExpenses).HasPrecision(18, 8);
            entity.Property(e => e.Ofac)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)")
                .HasColumnName("OFAC");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.ProviderId).HasColumnType("int(11)");
            entity.Property(e => e.ResidenceTypeId).HasColumnType("int(11)");
            entity.Property(e => e.ScoreReason).HasMaxLength(255);
            entity.Property(e => e.StatedMonthlyIncome).HasPrecision(18, 8);
            entity.Property(e => e.UnderwriterAdjustment).HasPrecision(18, 8);

            entity.HasOne(d => d.Applicant).WithMany(p => p.CreditResponses)
                .HasForeignKey(d => d.ApplicantId)
                .HasConstraintName("FK_CreditResponse_Applicant");

            entity.HasOne(d => d.Application).WithMany(p => p.CreditResponses)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditResponse_Application");
        });

        modelBuilder.Entity<CreditScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CreditScore");

            entity.HasIndex(e => e.ClientId, "FK_CreditScore_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_CreditScore_CreatedBy");

            entity.HasIndex(e => e.CreditResponseId, "FK_CreditScore_CreditResponse");

            entity.HasIndex(e => e.CreditScoreTypeId, "FK_CreditScore_CreditScoreType");

            entity.HasIndex(e => e.EntityRolesId, "FK_CreditScore_EntityRoles");

            entity.HasIndex(e => e.OwnedById, "FK_CreditScore_OwnedBy");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.CreditResponseId).HasColumnType("int(11)");
            entity.Property(e => e.CreditScoreTypeId).HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.FactActCode)
                .HasMaxLength(255)
                .HasComment("Code returned from credit for a score.factact record.");
            entity.Property(e => e.IsFactActFound)
                .HasDefaultValueSql("b'0'")
                .HasComment("Value pulled from credit response.")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsPassed)
                .HasDefaultValueSql("b'0'")
                .HasComment("Value pulled from credit response.")
                .HasColumnType("bit(1)");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
            entity.Property(e => e.Percentile)
                .HasPrecision(18, 8)
                .HasComment("Credit Score Percentile pulled from credit response for each score.");
            entity.Property(e => e.Value)
                .HasComment("Value pulled from credit response for each score.")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.CreditResponse).WithMany(p => p.CreditScores)
                .HasForeignKey(d => d.CreditResponseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditScore_CreditResponse");
        });

        modelBuilder.Entity<DisbursementStateManagement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DisbursementStateManagement");

            entity.HasIndex(e => e.ClientId, "FK_DisbursementStateManagement_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_DisbursementStateManagement_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_DisbursementStateManagement_DeletedBy");

            entity.HasIndex(e => e.DisbursementStateId, "FK_DisbursementStateManagement_DisbursementState");

            entity.HasIndex(e => e.EntityRolesId, "FK_DisbursementStateManagement_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_DisbursementStateManagement_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_DisbursementStateManagement_OwnedBy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.DisbursementStateId).HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");

            entity.HasOne(d => d.DisbursementState).WithMany(p => p.DisbursementStateManagements)
                .HasForeignKey(d => d.DisbursementStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DisbursementStateManagement_DisbursementState");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.DisbursementStateManagement)
                .HasForeignKey<DisbursementStateManagement>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DisbursementStateManagement_ApplicationDisbursement");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Income");

            entity.HasIndex(e => e.ClientId, "FK_Income_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_Income_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_Income_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_Income_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_Income_ModifiedBy");

            entity.HasIndex(e => e.OwnedById, "FK_Income_OwnedBy");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.Amount).HasPrecision(18, 8);
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.IsPrimary).HasColumnType("bit(1)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.OtherIncomeAmount).HasPrecision(18, 8);
            entity.Property(e => e.OtherIncomeSource).HasMaxLength(255);
            entity.Property(e => e.OwnedById).HasColumnType("int(11)");
        });

        modelBuilder.Entity<ProcessState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ProcessState");

            entity.HasIndex(e => e.ClientId, "FK_ProcessState_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_ProcessState_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_ProcessState_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_ProcessState_EntityRoles");

            entity.HasIndex(e => e.ModifiedBy, "FK_ProcessState_ModifiedBy");

            entity.HasIndex(e => e.ProcessStateGroupId, "FK_ProcessState_ProcessStateGroup");

            entity.HasIndex(e => e.ProcessStateTypeId, "FK_ProcessState_ProcessStateType");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DisplayName).HasMaxLength(255);
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.ProcessStateGroupId).HasColumnType("int(11)");
            entity.Property(e => e.ProcessStateTypeId).HasColumnType("int(11)");
            entity.Property(e => e.StatusName).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ClientId, "FK_Product_Client");

            entity.HasIndex(e => e.CreatedBy, "FK_Product_CreatedBy");

            entity.HasIndex(e => e.DeletedBy, "FK_Product_DeletedBy");

            entity.HasIndex(e => e.EntityRolesId, "FK_Product_EntityRoles");

            entity.HasIndex(e => e.LenderProfileId, "FK_Product_LenderProfile");

            entity.HasIndex(e => e.ModifiedBy, "FK_Product_ModifiedBy");

            entity.HasIndex(e => e.ProductCategoryId, "FK_Product_ProductCategory");

            entity.HasIndex(e => e.ProductGroupId, "FK_Product_ProductGroup");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Active).HasColumnType("bit(1)");
            entity.Property(e => e.ButtonText).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Disabled).HasColumnType("bit(1)");
            entity.Property(e => e.DisplayName).HasMaxLength(255);
            entity.Property(e => e.EntityRolesId).HasColumnType("bigint(20)");
            entity.Property(e => e.Icon).HasMaxLength(255);
            entity.Property(e => e.LenderProfileId).HasColumnType("int(11)");
            entity.Property(e => e.Link).HasMaxLength(255);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            entity.Property(e => e.ProductCategoryId).HasColumnType("int(11)");
            entity.Property(e => e.ProductGroupId).HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}