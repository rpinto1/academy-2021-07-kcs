using System;
using System.Data.Entity.Infrastructure;
using KCSit.SalesforceAcademy.Lasagna.Data.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class lasagnakcsContext : IdentityDbContext<ApplicationUser>
    {
        public lasagnakcsContext()
        {
        }

        public lasagnakcsContext(DbContextOptions<lasagnakcsContext> options)
            : base(options)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = int.MaxValue;
            
        }

        public virtual DbSet<BalanceSheet> BalanceSheets { get; set; }
        public virtual DbSet<CashFlowStatement> CashFlowStatements { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyIndex> CompanyIndices { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DailyInfo> DailyInfos { get; set; }
        public virtual DbSet<Exchange> Exchanges { get; set; }
        public virtual DbSet<IncomeStatement> IncomeStatements { get; set; }
        public virtual DbSet<Index> Indices { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<KeyRatio> KeyRatios { get; set; }
        public virtual DbSet<KeyStatistic> KeyStatistics { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<ScoringMethod> ScoringMethods { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<YearlyReport> YearlyReports { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<PortfolioCompany> PortfolioCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=tcp:academies-moongy.database.windows.net,1433;user=appadmin;password=qwert#4477;database=lasagna-kcs");//,options =>options.EnableRetryOnFailure());
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BalanceSheet>(entity =>
            {
                entity.Property(e => e.AccountsPayable).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.AccountsReceivable).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.AllowanceLoanLosses).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Aoci)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("AOCI");

                entity.Property(e => e.CapitalLeases).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CashAndEquivalents).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CommonStock).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CurrentDeferredRevenue).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DeferredPolicyCost).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DepositsLiability).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.FuturePolicyBenefits).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Goodwill).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.GrossLoans).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Inventories).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Investments).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.LongTermDebt).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetLoans).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NonCurrentDeferredRevenue).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherCurrentAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherCurrentLabilities).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherIntangibleAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherLiabilities).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PropertyPlantAndEquipment).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.RetainedEarnings).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ShareholdersEquity).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ShortTermDebt).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ShortTermInvestments).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TaxPayable).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalCurrentAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalCurrentLiabilities).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalInvestments).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalLiabilities).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalLiabilitiesAndEquity).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.UnearnedIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.UnearnedPremiums).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<CashFlowStatement>(entity =>
            {
                entity.Property(e => e.Acquisitions).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CashFinancing).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CashFromInvesting).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CashFromOperations).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CashPaidForDividends).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ChangeInDeferredTax).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ChangeInWorkingCapital).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DepreciationAmortization).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Investements).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetIssuanceOfCommonStock).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetIssuanceOfDebt).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherFinancing).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherInvesting).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherOperations).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PropertyPlantEquipment).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.StockBasedCompensation).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ticker)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Countries");

                entity.HasOne(d => d.DailyInfo)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.DailyInfoId)
                    .HasConstraintName("FK_Companies_DailyInfo");

                entity.HasOne(d => d.Exchange)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.ExchangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Exchanges");

                entity.HasOne(d => d.Industry)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.IndustryId)
                    .HasConstraintName("FK_Companies_Industries1");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.SectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Industries");
            });

            modelBuilder.Entity<CompanyIndex>(entity =>
            {
                entity.ToTable("CompanyIndex");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyIndices)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CompanyIndex_Companies");

                entity.HasOne(d => d.Index)
                    .WithMany(p => p.CompanyIndices)
                    .HasForeignKey(d => d.IndexId)
                    .HasConstraintName("FK_CompanyIndex_Index");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<DailyInfo>(entity =>
            {
                entity.ToTable("DailyInfo");

                entity.Property(e => e.PreviousClose).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.StockPrice).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Exchange>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<IncomeStatement>(entity =>
            {
                entity.Property(e => e.CostOfGoodsSold).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CreditLossesProvision).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Development).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Epsbasic)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EPSBasic");

                entity.Property(e => e.Epsdiluted)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EPSDiluted");

                entity.Property(e => e.FeesOtherIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.GrossProfit).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.IncomeTax).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.InterestExpense).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetInterestAclp)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("NetInterestACLP");

                entity.Property(e => e.NetInterestIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetInterestIncomeBank).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetIvestmentIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OperatingProfit).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherNonOperatingIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OtherOperatingExpense).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PolicyClaims).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PolicyExpense).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PreTaxIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Revenue).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.SalesGeneralAdministrative).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.SharesBasic).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.SharesDiluted).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalInterestExpense).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalInterestIncome).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalNonInterestExpense).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalNoninterestRevenue).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalOperatingExpenses).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalPremiums).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Index>(entity =>
            {
                entity.ToTable("Index");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Industry>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Sub_Industries")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Industries)
                    .HasForeignKey(d => d.SectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sub_Industries_Industries");
            });

            modelBuilder.Entity<KeyRatio>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssetsToEquity).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.BookValue).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.BookValuePerShare).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CapitalExpendituresGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.CashFromOperationsGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DebtToAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DebtToEquity).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DilutedEpsgrowth)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("DilutedEPSGrowth");

                entity.Property(e => e.DilutedSharesGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DividendsPerShare).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.EarningAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.EarningAssetsGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.EarningAssetsToEquity).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Ebidtagrowth)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EBIDTAGrowth");

                entity.Property(e => e.Ebidtamargin)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EBIDTAMargin");

                entity.Property(e => e.EbidtaperShare)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EBIDTAPerShare");

                entity.Property(e => e.EquityGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.EquityToAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.FreeCashFlow).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.FreeCashFlowGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.FreeCashFlowPerShare).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.FreeCashMargin).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.GrossLoansGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.GrossMargin).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.GrossProfitGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.LoanDeposit).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.LoanLossReverseLoans).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.MarketCapitalization).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetIncomeGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetInterestIncomeGrowthBank).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetInterestMargin).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetLoansGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.NetMargin).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OperatingIncomeGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OperatingIncomePerShare).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.OperatingMargin).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PayoutRatio).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PolicyRevenue).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PolicyRevenueGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Ppegrowth)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("PPEGrowth");

                entity.Property(e => e.PremiumGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PremiumShare).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PretaxIncomeGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PretaxMargin).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PriceToBook).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PriceToEarnings).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.PriceToSales).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ReturnOnAssets).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ReturnOnCapitalEmployed).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ReturnOnEquity).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ReturnOnInvestedCapital).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.ReturnOnTangibleCapitalEmployed).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.RevenueGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.RevenuePerShare).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Roi)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("ROI");

                entity.Property(e => e.TangibleBookValue).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TangibleBookValuePerShare).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalAssetsGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalDepositGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.TotalInvestmentsGrowth).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.UnderwritingMargin).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.UnderwritingProfit).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<KeyStatistic>(entity =>
            {
                entity.Property(e => e.AssetsCagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("AssetsCAGR");

                entity.Property(e => e.AssetsEquityMedian).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DebtAssetsMedian).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DebtEquityMedian).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.DepositsCagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("DepositsCAGR");

                entity.Property(e => e.EarningAemedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EarningAEMedian");

                entity.Property(e => e.EarningAssetsCagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EarningAssetsCAGR");

                entity.Property(e => e.Ebitmedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EBITMedian");

                entity.Property(e => e.Epscagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("EPSCAGR");

                entity.Property(e => e.EquityAssetsMedian).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Fcfcagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("FCFCAGR");

                entity.Property(e => e.Fcfmedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("FCFMedian");

                entity.Property(e => e.GrossLoansCagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("GrossLoansCAGR");

                entity.Property(e => e.GrossProfitMedian).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.LoanLossRtlmedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("LoanLossRTLMedian");

                entity.Property(e => e.NetInterestIncomeCagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("NetInterestIncomeCAGR");

                entity.Property(e => e.Nimmedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("NIMMedian");

                entity.Property(e => e.PermiumCagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("PermiumCAGR");

                entity.Property(e => e.PreTaxIncomeMedian).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.RevenueCagr)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("RevenueCAGR");

                entity.Property(e => e.Roamedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("ROAMedian");

                entity.Property(e => e.Roemedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("ROEMedian");

                entity.Property(e => e.Roicmedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("ROICMedian");

                entity.Property(e => e.Roimedian)
                    .HasColumnType("decimal(22, 2)")
                    .HasColumnName("ROIMedian");

                entity.Property(e => e.UnderwritingMedian).HasColumnType("decimal(22, 2)");

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.KeyStatistics)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeyStatistics_Companies");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.Property(e => e.MarginOfSafety).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Score1).HasColumnName("Score");

                entity.Property(e => e.StickerPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Scores_Companies");

                entity.HasOne(d => d.ScoringMethod)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.ScoringMethodId)
                    .HasConstraintName("FK_Scores_ScoringMethods");
            });

            modelBuilder.Entity<ScoringMethod>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Industries")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<YearlyReport>(entity =>
            {
                entity.Property(e => e.Uuid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.BalanceSheet)
                    .WithMany(p => p.YearlyReports)
                    .HasForeignKey(d => d.BalanceSheetId)
                    .HasConstraintName("FK_YearlyReports_BalanceSheets1");

                entity.HasOne(d => d.CashFlowStatement)
                    .WithMany(p => p.YearlyReports)
                    .HasForeignKey(d => d.CashFlowStatementId)
                    .HasConstraintName("FK_YearlyReports_CashFlowStatements");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.YearlyReports)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_YearlyReports_Companies");

                entity.HasOne(d => d.IncomeStatement)
                    .WithMany(p => p.YearlyReports)
                    .HasForeignKey(d => d.IncomeStatementId)
                    .HasConstraintName("FK_YearlyReports_IncomeStatements");

                entity.HasOne(d => d.KeyRatio)
                    .WithMany(p => p.YearlyReports)
                    .HasForeignKey(d => d.KeyRatioId)
                    .HasConstraintName("FK_YearlyReports_KeyRatios");
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Portfolios_AspNetUsers");
            });

            modelBuilder.Entity<PortfolioCompany>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PortfolioCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PortfolioCompanies_Companies");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.PortfolioCompanies)
                    .HasForeignKey(d => d.PortfolioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PortfolioCompanies_Portfolios");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
