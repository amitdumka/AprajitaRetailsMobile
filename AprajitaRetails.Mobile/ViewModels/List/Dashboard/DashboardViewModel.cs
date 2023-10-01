//using AKS.Shared.Commons.Ops;
using CommunityToolkit.Mvvm.ComponentModel;
//using AprajitaRetails.Mobile.MAUILib.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using static AprajitaRetails.Mobile.Views.ListWidget;
using AprajitaRetails.Mobile.DataModels.Base;

namespace AprajitaRetails.Mobile.ViewModels.List.Dashboard
{
    public partial class AccountingDashboardViewModel : BaseDashoardViewModel<AccountWidget>
    {
        private bool _localSync;

        [ObservableProperty]
        private List<ItemList> _attData;

        [ObservableProperty]
        private List<ItemList> _saleData;

        [ObservableProperty]
        private ItemList _bankData;

        [ObservableProperty]
        private ItemList _incomeExpenseData;

        [ObservableProperty]
        private List<ItemList> _voucherList;

        [ObservableProperty]
        private List<ItemList> _cashVoucherList;

        public void OnAppearing()
        {
            InitView();
            this.Icon = Resources.Styles.IconFont.BookReader;
            this.Title = "Dashboard";
        }

        public AccountingDashboardViewModel()
        {
            DataModel = new DashboardDataModel();
            //DataModel.Mode = DBType.Azure;
        }

        protected void InitView()
        {
            DataModel.Connect();
            Fetch();
        }

        protected void Reload()
        {
            VoucherList = new List<ItemList> {
                new ItemList { Title = "Payment", Description = Entity.TotalPayment.ToString() },
                new ItemList { Title = "Expenses", Description = Entity.TotalExpenses.ToString() },
                new ItemList { Title = "Receipts", Description = Entity.TotalReceipt.ToString() }
            };

            CashVoucherList = new List<ItemList> {
                new ItemList { Title = "Payment", Description = Entity.TotalCashPayment.ToString() },
                new ItemList { Title = "Receipts", Description = Entity.TotalCashReceipt.ToString() }};

            BankData = new ItemList { Title = Entity.BankWithdrwal.ToString(), Description = Entity.BankDeposit.ToString() };
            IncomeExpenseData = new ItemList { Title = Entity.TotalIncome.ToString(), Description = Entity.TotalExpense.ToString() };

            SaleData = new List<ItemList> {
                new ItemList { Title = $"Sale: {Entity.Sale}", Description = $" Cash : {Entity.CashSale} \n Non Cash: {Entity.NonCashSale}"},
                new ItemList { Title = $"Monthly: {Entity.TotalMonthlySale}", Description = $" Cash:  {Entity.TotalMonthlyCashSale} \n Non Cash:{Entity.TotalMonthlyNonCashSale}"},
                new ItemList { Title = $"Yearly: {Entity.TotalSale}", Description = $" Cash: {Entity.TotalCashSale} \n Non Cash: {Entity.TotalNonCashSale}"}
            };

            //SaleData = new List<ItemList> {
            //    new ItemList { Title = $"Sale/Monthly: {Entity.Sale}/{Entity.TotalMonthlySale} ",
            //        Description = $" Yearly : {Entity.TotalSale} "},

            //    new ItemList { Title = $"Cash/Montly: {Entity.CashSale} / {Entity.TotalMonthlyCashSale}  ",
            //        Description = $"Yearly : {Entity.TotalCashSale}"},
            //    new ItemList { Title = $"Other/Montly: {Entity.NonCashSale} / {Entity.TotalMonthlyNonCashSale} ",
            //        Description = $"Yearly :  {Entity.TotalNonCashSale}"}
            //};
        }

        protected async void Fetch()
        {
            if (Entity == null)
            {
                var voucherData = await DataModel.GetContext().Vouchers.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    .GroupBy(c => c.VoucherType).Select(c => new { VT = c.Key, TAmount = c.Sum(x => x.Amount) }).ToListAsync();
                var cashVoucherData = await DataModel.GetContext().CashVouchers.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Year == DateTime.Today.Year)
                    .GroupBy(c => c.VoucherType).Select(c => new { VT = c.Key, TAmount = c.Sum(x => x.Amount) }).ToListAsync();
                var due = await DataModel.GetContext().CustomerDues.Where(c => c.StoreId == CurrentSession.StoreCode).SumAsync(c => c.Amount);
                var rec = await DataModel.GetContext().DueRecovery.Where(c => c.StoreId == CurrentSession.StoreCode).SumAsync(c => c.Amount);

                var monthlysale = await DataModel.GetContext().DailySales.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Month == DateTime.Today.Month
                            && c.OnDate.Year == DateTime.Today.Year).GroupBy(c => c.StoreId)
                            .Select(c => new { TS = c.Sum(x => x.Amount), CS = c.Sum(c => c.CashAmount) }).FirstOrDefaultAsync(); ;
                var yearlysale = await DataModel.GetContext().DailySales.Where(c => c.StoreId == CurrentSession.StoreCode
                            && c.OnDate.Year == DateTime.Today.Year).GroupBy(c => c.StoreId)
                            .Select(c => new { TS = c.Sum(x => x.Amount), CS = c.Sum(c => c.CashAmount) }).FirstOrDefaultAsync(); ;
                var todaysale = await DataModel.GetContext().DailySales.Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Month == DateTime.Today.Month
                            && c.OnDate.Date == DateTime.Today.Date).GroupBy(c => c.StoreId)
                            .Select(c => new { TS = c.Sum(x => x.Amount), CS = c.Sum(c => c.CashAmount) }).FirstOrDefaultAsync(); ;
                AttData = await DataModel.GetContext().Attendances.Include(c=>c.Employee).Where(c => c.StoreId == CurrentSession.StoreCode && c.OnDate.Date == DateTime.Today.Date).Select(c => new ItemList { Title = c.Employee.StaffName, Description = c.Status.ToString() }).ToListAsync();

                Entity = new AccountWidget
                {
                    OnDate = DateTime.Now,
                    TotalReceipt = voucherData.Where(c => c.VT == VoucherType.Receipt).FirstOrDefault().TAmount,
                    TotalCashPayment = cashVoucherData.Where(c => c.VT == VoucherType.CashPayment).FirstOrDefault().TAmount,
                    TotalCashReceipt = cashVoucherData.Where(c => c.VT == VoucherType.CashReceipt).FirstOrDefault().TAmount,
                    TotalExpenses = voucherData.Where(c => c.VT == VoucherType.Expense).FirstOrDefault().TAmount,
                    TotalPayment = voucherData.Where(c => c.VT == VoucherType.Payment).FirstOrDefault().TAmount,
                    BankDeposit = -1,
                    BankWithdrwal = -1,
                    CashInBank = -1,
                    TotalDueRecorver = rec,
                    TotalDueAmount = due,
                    CashInHand = -1,
                    TotalSale = yearlysale.TS,
                    TotalCashSale = yearlysale.CS,
                    TotalMonthlyCashSale = monthlysale.CS,
                    TotalMonthlySale = monthlysale.TS,
                    CashSale = todaysale.CS,
                    Sale = todaysale.TS
                };
                Reload();
            }
        }
    }

    public class AccountWidget
    {
        public DateTime OnDate { get; set; }

        public decimal TotalCashPayment { get; set; }
        public decimal TotalCashReceipt { get; set; }

        public decimal TotalPayment { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalReceipt { get; set; }

        public decimal BankDeposit { get; set; }
        public decimal BankWithdrwal { get; set; }

        public decimal CashInHand { get; set; }
        public decimal CashInBank { get; set; }

        public decimal TotalDueAmount { get; set; }
        public decimal TotalDueRecorver { get; set; }

        public decimal TotalDuePending
        { get { return TotalDueAmount - TotalDueRecorver; } }

        public decimal Sale { get; set; }

        public decimal NonCashSale
        { get { return Sale - CashSale; } }

        public decimal CashSale { get; set; }

        public decimal TotalSale { get; set; }

        public decimal TotalNonCashSale
        { get { return TotalSale - TotalCashSale; } }

        public decimal TotalCashSale { get; set; }

        public decimal TotalMonthlySale { get; set; }

        public decimal TotalMonthlyNonCashSale
        { get { return TotalMonthlySale - TotalMonthlyCashSale; } }

        public decimal TotalMonthlyCashSale { get; set; }

        public decimal TotalIncome
        { get { return TotalSale + TotalCashReceipt + TotalReceipt; } }

        public decimal TotalExpense
        { get { return TotalPayment + TotalCashPayment + TotalExpenses; } }
    }
}