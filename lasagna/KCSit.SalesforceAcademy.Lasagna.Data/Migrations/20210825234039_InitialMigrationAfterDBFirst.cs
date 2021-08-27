using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Migrations
{
    public partial class InitialMigrationAfterDBFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashAndEquivalents = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ShortTermInvestments = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    AccountsReceivable = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Inventories = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherCurrentAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Investments = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PropertyPlantAndEquipment = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Goodwill = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherIntangibleAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    AccountsPayable = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TaxPayable = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ShortTermDebt = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CurrentDeferredRevenue = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherCurrentLabilities = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    LongTermDebt = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CapitalLeases = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NonCurrentDeferredRevenue = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherLiabilities = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    RetainedEarnings = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CommonStock = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    AOCI = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ShareholdersEquity = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalCurrentAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalCurrentLiabilities = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalLiabilities = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalLiabilitiesAndEquity = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    DeferredPolicyCost = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    UnearnedPremiums = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FuturePolicyBenefits = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalInvestments = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    GrossLoans = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    AllowanceLoanLosses = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    UnearnedIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetLoans = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DepositsLiability = table.Column<decimal>(type: "decimal(22,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashFlowStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DepreciationAmortization = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ChangeInWorkingCapital = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ChangeInDeferredTax = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    StockBasedCompensation = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherOperations = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CashFromOperations = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PropertyPlantEquipment = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Acquisitions = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherInvesting = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CashFromInvesting = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetIssuanceOfCommonStock = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetIssuanceOfDebt = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CashPaidForDividends = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherFinancing = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CashFinancing = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Investements = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowStatements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PreviousClose = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revenue = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CostOfGoodsSold = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    GrossProfit = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    SalesGeneralAdministrative = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherOperatingExpense = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OperatingProfit = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetInterestIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OtherNonOperatingIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PreTaxIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    IncomeTax = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EPSBasic = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EPSDiluted = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    SharesBasic = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    SharesDiluted = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Development = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalOperatingExpenses = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalInterestIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalInterestExpense = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetInterestIncomeBank = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalNoninterestRevenue = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CreditLossesProvision = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetInterestACLP = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalNonInterestExpense = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalPremiums = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetIvestmentIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FeesOtherIncome = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PolicyClaims = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PolicyExpense = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    InterestExpense = table.Column<decimal>(type: "decimal(22,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeStatements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Index",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Index", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyRatios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnOnAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ReturnOnEquity = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ReturnOnInvestedCapital = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ReturnOnCapitalEmployed = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    GrossMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EBIDTAMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OperatingMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PretaxMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FreeCashMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    AssetsToEquity = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EquityToAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DebtToEquity = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DebtToAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    RevenueGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    GrossProfitGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EBIDTAGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OperatingIncomeGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PretaxIncomeGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetIncomeGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DilutedEPSGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DilutedSharesGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PPEGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalAssetsGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EquityGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CashFromOperationsGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    CapitalExpendituresGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FreeCashFlowGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FreeCashFlow = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    BookValue = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TangibleBookValue = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    RevenuePerShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EBIDTAPerShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    OperatingIncomePerShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FreeCashFlowPerShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    BookValuePerShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TangibleBookValuePerShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    MarketCapitalization = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PriceToEarnings = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PriceToBook = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PriceToSales = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DividendsPerShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PayoutRatio = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ReturnOnTangibleCapitalEmployed = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PolicyRevenue = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    UnderwritingProfit = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    UnderwritingMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ROI = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PremiumShare = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PremiumGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PolicyRevenueGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalInvestmentsGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EarningAssets = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetInterestMargin = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EarningAssetsToEquity = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    LoanDeposit = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    LoanLossReverseLoans = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetInterestIncomeGrowthBank = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    GrossLoansGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NetLoansGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    TotalDepositGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EarningAssetsGrowth = table.Column<decimal>(type: "decimal(22,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyRatios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ScoringMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sub_Industries_Industries",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Ticker = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Symbol = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    CompanyType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DailyInfoId = table.Column<int>(type: "int", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IndustryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Countries",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_DailyInfo",
                        column: x => x.DailyInfoId,
                        principalTable: "DailyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Exchanges",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Industries",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Industries1",
                        column: x => x.IndustryId,
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyIndex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    IndexId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyIndex_Companies",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyIndex_Index",
                        column: x => x.IndexId,
                        principalTable: "Index",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KeyStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ROAMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ROEMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ROICMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    RevenueCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    AssetsCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FCFCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EPSCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    GrossProfitMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EBITMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PreTaxIncomeMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    FCFMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    AssetsEquityMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DebtEquityMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DebtAssetsMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    PermiumCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    UnderwritingMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    ROIMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    NetInterestIncomeCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    GrossLoansCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EarningAssetsCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    DepositsCAGR = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    NIMMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EarningAEMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    LoanLossRTLMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true),
                    EquityAssetsMedian = table.Column<decimal>(type: "decimal(22,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyStatistics_Companies",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoringMethodId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<double>(type: "float", nullable: true),
                    StickerPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MarginOfSafety = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Companies",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scores_ScoringMethods",
                        column: x => x.ScoringMethodId,
                        principalTable: "ScoringMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearlyReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IncomeStatementId = table.Column<int>(type: "int", nullable: true),
                    BalanceSheetId = table.Column<int>(type: "int", nullable: true),
                    CashFlowStatementId = table.Column<int>(type: "int", nullable: true),
                    KeyRatioId = table.Column<int>(type: "int", nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearlyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearlyReports_BalanceSheets1",
                        column: x => x.BalanceSheetId,
                        principalTable: "BalanceSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearlyReports_CashFlowStatements",
                        column: x => x.CashFlowStatementId,
                        principalTable: "CashFlowStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearlyReports_Companies",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearlyReports_IncomeStatements",
                        column: x => x.IncomeStatementId,
                        principalTable: "IncomeStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearlyReports_KeyRatios",
                        column: x => x.KeyRatioId,
                        principalTable: "KeyRatios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_DailyInfoId",
                table: "Companies",
                column: "DailyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ExchangeId",
                table: "Companies",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IndustryId",
                table: "Companies",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SectorId",
                table: "Companies",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyIndex_CompanyId",
                table: "CompanyIndex",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyIndex_IndexId",
                table: "CompanyIndex",
                column: "IndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Industries_SectorId",
                table: "Industries",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sub_Industries",
                table: "Industries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyStatistics_CompanyId",
                table: "KeyStatistics",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_CompanyId",
                table: "Scores",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ScoringMethodId",
                table: "Scores",
                column: "ScoringMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Industries",
                table: "Sectors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YearlyReports_BalanceSheetId",
                table: "YearlyReports",
                column: "BalanceSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_YearlyReports_CashFlowStatementId",
                table: "YearlyReports",
                column: "CashFlowStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_YearlyReports_CompanyId",
                table: "YearlyReports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_YearlyReports_IncomeStatementId",
                table: "YearlyReports",
                column: "IncomeStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_YearlyReports_KeyRatioId",
                table: "YearlyReports",
                column: "KeyRatioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyIndex");

            migrationBuilder.DropTable(
                name: "KeyStatistics");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "YearlyReports");

            migrationBuilder.DropTable(
                name: "Index");

            migrationBuilder.DropTable(
                name: "ScoringMethods");

            migrationBuilder.DropTable(
                name: "BalanceSheets");

            migrationBuilder.DropTable(
                name: "CashFlowStatements");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "IncomeStatements");

            migrationBuilder.DropTable(
                name: "KeyRatios");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "DailyInfo");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
