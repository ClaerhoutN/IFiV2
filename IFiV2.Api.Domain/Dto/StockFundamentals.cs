using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IFiV2.Api.Domain.Dto
{
    public class StockFundamentals
    {
        #region inner dto classes
        public class _General
        {
            public class _AddressData
            {
                public string? City { get; set; }
                public string? Country { get; set; }
                public string? State { get; set; }
                public string? Street { get; set; }
                public string? ZIP { get; set; }
            }
            public class _Listing
            {
                public string? Code { get; set; }
                public string? Exchange { get; set; }
                public string? Name { get; set; }
            }
            public class _Officer
            {
                public string? Name { get; set; }
                public string? Title { get; set; }
                public string? YearBorn { get; set; }
            }
            public string? Address { get; set; }
            public _AddressData AddressData { get; set; }
            public string? CIK { get; set; }
            public string? Code { get; set; }
            public string? CountryISO { get; set; }
            public string? CountryName { get; set; }
            public string? CurrencyCode { get; set; }
            public string? CurrencyName { get; set; }
            public string? CurrencySymbol { get; set; }
            public string? CUSIP { get; set; }
            public string? Description { get; set; }
            public string? EmployerIdNumber { get; set; }
            public string? Exchange { get; set; }
            public string? FiscalYearEnd { get; set; }
            public int FullTimeEmployees { get; set; }
            public string? GicGroup { get; set; }
            public string? GicIndustry { get; set; }
            public string? GicSector { get; set; }
            public string? GicSubIndustry { get; set; }
            public string? HomeCategory { get; set; }
            public string? Industry { get; set; }
            public string? InternationalDomestic { get; set; }
            public DateOnly IPODate { get; set; }
            public bool IsDelisted { get; set; }
            public string? ISIN { get; set; }
            public string? LEI { get; set; }
            public Dictionary<string, _Listing> Listings { get; set; }
            public string? LogoURL { get; set; }
            public string? Name { get; set; }
            public Dictionary<string, _Officer> Officers { get; set; }
            public string? OpenFigi { get; set; }
            public string? Phone { get; set; }
            public string? PrimaryTicker { get; set; }
            public string? Sector { get; set; }
            public string? Type { get; set; }
            public DateOnly UpdatedAt { get; set; }
            public string? WebURL { get; set; }
        }
        public class _HighLights
        {
            public decimal BookValue { get; set; }
            public decimal DilutedEpsTTM { get; set; }
            public decimal DividendShare { get; set; }
            public decimal DividendYield { get; set; }
            public decimal EarningsShare { get; set; }
            public long EBITDA { get; set; }
            public decimal EPSEstimateCurrentQuarter { get; set; }
            public decimal EPSEstimateCurrentYear { get; set; }
            public decimal EPSEstimateNextQuarter { get; set; }
            public decimal EPSEstimateNextYear { get; set; }
            public long GrossProfitTTM { get; set; }
            public long MarketCapitalization { get; set; }
            public decimal MarketCapitalizationMln { get; set; }
            public DateOnly MostRecentQuarter { get; set; }
            public decimal OperatingMarginTTM { get; set; }
            public decimal PEGRatio { get; set; }
            public decimal PERatio { get; set; }
            public decimal ProfitMargin { get; set; }
            public decimal QuarterlyEarningsGrowthYOY { get; set; }
            public decimal QuarterlyRevenueGrowthYOY { get; set; }
            public decimal ReturnOnAssetsTTM { get; set; }
            public decimal ReturnOnEquityTTM { get; set; }
            public decimal RevenuePerShareTTM { get; set; }
            public long RevenueTTM { get; set; }
            public decimal WallStreetTargetPrice { get; set; }
        }
        public class _Valuation
        {
            public long EnterpriseValue { get; set; }
            public decimal EnterpriseValueEbitda { get; set; }
            public decimal EnterpriseValueRevenue { get; set; }
            public decimal ForwardPE { get; set; }
            public decimal PriceBookMRQ { get; set; }
            public decimal PriceSalesTTM { get; set; }
            public decimal TrailingPE { get; set; }
        }
        public class _SharesStats
        {
            public decimal PercentInsiders { get; set; }
            public decimal PercentInstitutions { get; set; }
            public long SharesFloat { get; set; }
            public long SharesOutstanding { get; set; }
            public long? SharesShort { get; set; }
            public long? SharesShortPriorMonth { get; set; }
            public decimal? ShortPercentFloat { get; set; }
            public decimal? ShortPercentOutstanding { get; set; }
            public decimal? ShortRatio { get; set; }
        }
        public class _Technicals
        {
            public decimal _200DayMA { get; set; }
            public decimal _50DayMA { get; set; }
            public decimal _52WeekHigh { get; set; }
            public decimal _52WeekLow { get; set; }
            public decimal Beta { get; set; }
            public long SharesShort { get; set; }
            public long SharesShortPriorMonth { get; set; }
            public decimal ShortPercent { get; set; }
            public decimal ShortRatio { get; set; }
        }
        public class _SplitsDividends
        {
            public DateOnly DividendDate { get; set; }
            public DateOnly ExDividendDate { get; set; }
            public decimal ForwardAnnualDividendRate { get; set; }
            public decimal ForwardAnnualDividendYield { get; set; }
            public DateOnly LastSplitDate { get; set; }
            public string? LastSplitFactor { get; set; }
            public Dictionary<string, NumberDividendsByYearEntry> NumberDividendsByYear { get; set; }
            public decimal PayoutRatio { get; set; }

            public class NumberDividendsByYearEntry
            {
                public int Count { get; set; }
                public int Year { get; set; }
            }
        }
        public class _AnalystRatings
        {
            public int Buy { get; set; }
            public int Hold { get; set; }
            public decimal Rating { get; set; }
            public int Sell { get; set; }
            public int StrongBuy { get; set; }
            public int StrongSell { get; set; }
            public decimal TargetPrice { get; set; }
        }
        public class _Holders
        {
            public Dictionary<string, HolderEntry> Funds { get; set; }
            public Dictionary<string, HolderEntry> Institutions { get; set; }
            public class HolderEntry
            {
                public long change { get; set; }
                public decimal change_p { get; set; }
                public long currentShares { get; set; }
                public DateOnly date { get; set; }
                public string? name { get; set; }
                public decimal totalAssets { get; set; }
                public decimal totalShares { get; set; }
            }
        }
        public class _InsiderTransaction
        {
            public DateOnly date { get; set; }
            public string? ownerCik { get; set; }
            public string? ownerName { get; set; }
            public long? postTransactionAmount { get; set; }
            public string? secLink { get; set; }
            public string? transactionAcquiredDisposed { get; set; }
            public long? transactionAmount { get; set; }
            public string? transactionCode { get; set; }
            public DateOnly transactionDate { get; set; }
            public decimal transactionPrice { get; set; }
        }
        public class _OutstandingsShares
        {
            public Dictionary<string, SharesEntry> annual { get; set; }
            public Dictionary<string, SharesEntry> quarterly { get; set; }

            public class SharesEntry
            {
                public string? date { get; set; }
                public DateOnly dateFormatted { get; set; }
                public long shares { get; set; }
                public string? sharesMln { get; set; }
            }
        }
        public class _Earnings
        {
            public Dictionary<string, EarningsAnnualEntry> Annual { get; set; }
            public Dictionary<string, EarningsHistoryEntry> History { get; set; }
            public Dictionary<string, EarningsTrendEntry> Trend { get; set; }

            public class EarningsAnnualEntry
            {
                public string? date { get; set; }
                public decimal epsActual { get; set; }
            }

            public class EarningsHistoryEntry
            {
                public string? beforeAfterMarket { get; set; }
                public string? currency { get; set; }
                public string? date { get; set; }
                public decimal? epsActual { get; set; }
                public decimal? epsDifference { get; set; }
                public decimal? epsEstimate { get; set; }
                public string? reportDate { get; set; }
                public decimal? surprisePercent { get; set; }
            }

            public class EarningsTrendEntry
            {
                public string? date { get; set; }
                public string? earningsEstimateAvg { get; set; }
                public string? earningsEstimateGrowth { get; set; }
                public string? earningsEstimateHigh { get; set; }
                public string? earningsEstimateLow { get; set; }
                public string? earningsEstimateNumberOfAnalysts { get; set; }
                public string? earningsEstimateYearAgoEps { get; set; }
                public string? epsRevisionsDownLast30days { get; set; }
                public string? epsRevisionsDownLast7days { get; set; }
                public string? epsRevisionsUpLast30days { get; set; }
                public string? epsRevisionsUpLast7days { get; set; }
                public string? epsTrend30daysAgo { get; set; }
                public string? epsTrend60daysAgo { get; set; }
                public string? epsTrend7daysAgo { get; set; }
                public string? epsTrend90daysAgo { get; set; }
                public string? epsTrendCurrent { get; set; }
                public string? growth { get; set; }
                public string? period { get; set; }
                public string? revenueEstimateAvg { get; set; }
                public string? revenueEstimateGrowth { get; set; }
                public string? revenueEstimateHigh { get; set; }
                public string? revenueEstimateLow { get; set; }
                public string? revenueEstimateNumberOfAnalysts { get; set; }
                public string? revenueEstimateYearAgoEps { get; set; }
            }
        }
        public class _Financials
        {
            public class _BalanceSheet
            {
                [JsonPropertyName("currency_symbol")]
                public string? CurrencySymbol { get; set; }
                public Dictionary<DateOnly, BalanceSheetEntry> Yearly { get; set; }
                public Dictionary<DateOnly, BalanceSheetEntry> Quarterly { get; set; }
                public class BalanceSheetEntry
                {
                    public string? accountsPayable { get; set; }
                    public string? accumulatedAmortization { get; set; }
                    public string? accumulatedDepreciation { get; set; }
                    public string? accumulatedOtherComprehensiveIncome { get; set; }
                    public string? additionalPaidInCapital { get; set; }
                    public string? capitalLeaseObligations { get; set; }
                    public string? capitalStock { get; set; }
                    public string? capitalSurpluse { get; set; }
                    public string? cash { get; set; }
                    public string? cashAndEquivalents { get; set; }
                    public string? cashAndShortTermInvestments { get; set; }
                    public string? commonStock { get; set; }
                    public string? commonStockSharesOutstanding { get; set; }
                    public string? commonStockTotalEquity { get; set; }
                    public string? currency_symbol { get; set; }
                    public string? currentDeferredRevenue { get; set; }
                    public string? date { get; set; }
                    public string? deferredLongTermAssetCharges { get; set; }
                    public string? deferredLongTermLiab { get; set; }
                    public string? earningAssets { get; set; }
                    public string? filing_date { get; set; }
                    public string? goodWill { get; set; }
                    public string? intangibleAssets { get; set; }
                    public string? inventory { get; set; }
                    public string? liabilitiesAndStockholdersEquity { get; set; }
                    public string? longTermDebt { get; set; }
                    public string? longTermDebtTotal { get; set; }
                    public string? longTermInvestments { get; set; }
                    public string? negativeGoodwill { get; set; }
                    public string? netDebt { get; set; }
                    public string? netInvestedCapital { get; set; }
                    public string? netReceivables { get; set; }
                    public string? netTangibleAssets { get; set; }
                    public string? netWorkingCapital { get; set; }
                    public string? noncontrollingInterestInConsolidatedEntity { get; set; }
                    public string? nonCurrentAssetsTotal { get; set; }
                    public string? nonCurrentLiabilitiesOther { get; set; }
                    public string? nonCurrentLiabilitiesTotal { get; set; }
                    public string? nonCurrrentAssetsOther { get; set; }
                    public string? otherAssets { get; set; }
                    public string? otherCurrentAssets { get; set; }
                    public string? otherCurrentLiab { get; set; }
                    public string? otherLiab { get; set; }
                    public string? otherStockholderEquity { get; set; }
                    public string? preferredStockRedeemable { get; set; }
                    public string? preferredStockTotalEquity { get; set; }
                    public string? propertyPlantAndEquipmentGross { get; set; }
                    public string? propertyPlantAndEquipmentNet { get; set; }
                    public string? propertyPlantEquipment { get; set; }
                    public string? retainedEarnings { get; set; }
                    public string? retainedEarningsTotalEquity { get; set; }
                    public string? shortLongTermDebt { get; set; }
                    public string? shortLongTermDebtTotal { get; set; }
                    public string? shortTermDebt { get; set; }
                    public string? shortTermInvestments { get; set; }
                    public string? temporaryEquityRedeemableNoncontrollingInterests { get; set; }
                    public string? totalAssets { get; set; }
                    public string? totalCurrentAssets { get; set; }
                    public string? totalCurrentLiabilities { get; set; }
                    public string? totalLiab { get; set; }
                    public string? totalPermanentEquity { get; set; }
                    public string? totalStockholderEquity { get; set; }
                    public string? treasuryStock { get; set; }
                    public string? warrants { get; set; }
                }
            }
            public class _CashFlow
            {
                [JsonPropertyName("currency_symbol")]
                public string? CurrencySymbol { get; set; }
                public Dictionary<DateOnly, CashFlowEntry> Yearly { get; set; }
                public Dictionary<DateOnly, CashFlowEntry> Quarterly { get; set; }
                public class CashFlowEntry
                {
                    public string? beginPeriodCashFlow { get; set; }
                    public string? capitalExpenditures { get; set; }
                    public string? cashAndCashEquivalentsChanges { get; set; }
                    public string? cashFlowsOtherOperating { get; set; }
                    public string? changeInCash { get; set; }
                    public string? changeInWorkingCapital { get; set; }
                    public string? changeReceivables { get; set; }
                    public string? changeToAccountReceivables { get; set; }
                    public string? changeToInventory { get; set; }
                    public string? changeToLiabilities { get; set; }
                    public string? changeToNetincome { get; set; }
                    public string? changeToOperatingActivities { get; set; }
                    public string? currency_symbol { get; set; }
                    public string? date { get; set; }
                    public string? depreciation { get; set; }
                    public string? dividendsPaid { get; set; }
                    public string? endPeriodCashFlow { get; set; }
                    public string? exchangeRateChanges { get; set; }
                    public string? filing_date { get; set; }
                    public string? freeCashFlow { get; set; }
                    public string? investments { get; set; }
                    public string? issuanceOfCapitalStock { get; set; }
                    public string? netBorrowings { get; set; }
                    public string? netIncome { get; set; }
                    public string? otherCashflowsFromFinancingActivities { get; set; }
                    public string? otherCashflowsFromInvestingActivities { get; set; }
                    public string? otherNonCashItems { get; set; }
                    public string? salePurchaseOfStock { get; set; }
                    public string? stockBasedCompensation { get; set; }
                    public string? totalCashflowsFromInvestingActivities { get; set; }
                    public string? totalCashFromFinancingActivities { get; set; }
                    public string? totalCashFromOperatingActivities { get; set; }
                }
            }
            public class _IncomeStatement
            {
                [JsonPropertyName("currency_symbol")]
                public string? CurrencySymbol { get; set; }
                public Dictionary<DateOnly, IncomeStatementEntry> Yearly { get; set; }
                public Dictionary<DateOnly, IncomeStatementEntry> Quarterly { get; set; }
                public class IncomeStatementEntry
                {
                    public string? costOfRevenue { get; set; }
                    public string? currency_symbol { get; set; }
                    public string? date { get; set; }
                    public string? depreciationAndAmortization { get; set; }
                    public string? discontinuedOperations { get; set; }
                    public string? ebit { get; set; }
                    public string? ebitda { get; set; }
                    public string? effectOfAccountingCharges { get; set; }
                    public string? extraordinaryItems { get; set; }
                    public string? filing_date { get; set; }
                    public string? grossProfit { get; set; }
                    public string? incomeBeforeTax { get; set; }
                    public string? incomeTaxExpense { get; set; }
                    public string? interestExpense { get; set; }
                    public string? interestIncome { get; set; }
                    public string? minorityInterest { get; set; }
                    public string? netIncome { get; set; }
                    public string? netIncomeApplicableToCommonShares { get; set; }
                    public string? netIncomeFromContinuingOps { get; set; }
                    public string? netInterestIncome { get; set; }
                    public string? nonOperatingIncomeNetOther { get; set; }
                    public string? nonRecurring { get; set; }
                    public string? operatingIncome { get; set; }
                    public string? otherItems { get; set; }
                    public string? otherOperatingExpenses { get; set; }
                    public string? preferredStockAndOtherAdjustments { get; set; }
                    public string? reconciledDepreciation { get; set; }
                    public string? researchDevelopment { get; set; }
                    public string? sellingAndMarketingExpenses { get; set; }
                    public string? sellingGeneralAdministrative { get; set; }
                    public string? taxProvision { get; set; }
                    public string? totalOperatingExpenses { get; set; }
                    public string? totalOtherIncomeExpenseNet { get; set; }
                    public string? totalRevenue { get; set; }
                }
            }
            [JsonPropertyName("Balance_Sheet")]
            public _BalanceSheet BalanceSheet { get; set; }
            [JsonPropertyName("Cash_Flow")]
            public _CashFlow CashFlow { get; set; }
            [JsonPropertyName("Income_Statement")]
            public _IncomeStatement IncomeStatement { get; set; }
        }
        #endregion
        public _General General { get; set; }
        public _HighLights HighLights { get; set; }
        public _Valuation Valuation { get; set; }
        public _SharesStats SharesStats { get; set; }
        public _Technicals Technicals { get; set; }
        public _SplitsDividends SplitsDividends { get; set; }
        public _AnalystRatings AnalystRatings { get; set; }
        public _Holders Holders { get; set; }
        public Dictionary<string, _InsiderTransaction> InsiderTransactions { get; set; }
        //ommitted: ESGScores
        public _OutstandingsShares OutstandingsShares { get; set; }
        public _Earnings Earnings { get; set; }
        public _Financials Financials { get; set; }

    }
}
