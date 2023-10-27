using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

using Syncfusion.Maui.Core.Hosting;
using AprajitaRetails.Mobile.ViewModels.List.Payroll;
using AprajitaRetails.Mobile.Pages.Payroll;
using AprajitaRetails.Mobile.Views.Custom;
using AprajitaRetails.Mobile.ViewModels.List.Inventory;
using AprajitaRetails.Mobile.ViewModels.List.Accounting.Banking;
using AprajitaRetails.Mobile.Pages.Banking;
using AprajitaRetails.Mobile.ViewModels.List.Accounting;
using AprajitaRetails.Mobile.Pages.Accounting;
using AprajitaRetails.Mobile.Pages.EntryPages;
using AprajitaRetails.Mobile.Pages.EntryPages.Payroll;
//using AprajitaRetails.Mobile.ViewModels.EntryPages;

namespace AprajitaRetails.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiCommunityToolkitCore().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
            }).UseMauiCommunityToolkit();

            builder.ConfigureSyncfusionCore();
            // builder.ConfigureSyncfusionDataGrid();

            //builder.Services.AddSingleton<ComboBoxOptionList>();
            //Accounting
            //Voucher
            builder.Services.AddSingleton<VoucherViewModel>();
            builder.Services.AddSingleton<VoucherPage>();
            //builder.Services.AddSingleton<VoucherEntryPage>();
            ////CashVoucher
            builder.Services.AddSingleton<CashVoucherViewModel>();
            builder.Services.AddSingleton<CashVoucherPage>();
            //builder.Services.AddSingleton<CashVoucherEntryPage>();
            ////Dashboardpage
            //builder.Services.AddSingleton<AccountingDashboardViewModel>();
            ////builder.Services.AddSingleton<DashboardPage>();
            //builder.Services.AddSingleton<StoreManagerDashboardPage>();

            //PettyCash
            builder.Services.AddSingleton<PettyCashViewMoldel>();
            builder.Services.AddSingleton<PettyCashPage>();
            builder.Services.AddSingleton<CashDetailPage>();
            ////DailySale
            builder.Services.AddSingleton<DailySaleViewMoldel>();
            builder.Services.AddSingleton<DailySalePage>();
            ////CashDetails
            builder.Services.AddSingleton<CashDetailViewModel>();
            builder.Services.AddSingleton<CashDetailPage>();
            ////Notes
            builder.Services.AddSingleton<NoteViewModel>();
            builder.Services.AddSingleton<NotePage>();

            ////Banking
            builder.Services.AddSingleton<BankViewModel>();
            builder.Services.AddSingleton<BankPage>();
            //builder.Services.AddSingleton<BankEntryPage>();

            builder.Services.AddSingleton<BankAccountViewModel>();
            builder.Services.AddSingleton<BankAccountPage>();

            builder.Services.AddSingleton<VendorAccountViewModel>();
            builder.Services.AddSingleton<VendorAccountPage>();

            ////due
            builder.Services.AddSingleton<CustomerDueViewModel>();
            builder.Services.AddSingleton<CustomerDuePage>();

            ////Due rec
            builder.Services.AddSingleton<DueRecoveryViewModel>();
            builder.Services.AddSingleton<DueRecoveryPage>();

            ////Bankfino
            builder.Services.AddSingleton<BankTransactionViewModel>();
            builder.Services.AddSingleton<BankTranscationPage>();

            ////Attendance
            builder.Services.AddSingleton<AttendanceViewModel>();
            builder.Services.AddSingleton<AttendancePage>();
             
            //Monthly Attendance
            builder.Services.AddSingleton<MonthlyAttendanceViewModel>();
             builder.Services.AddSingleton<MonthlyAttendancePage>();
            //Attendance
            builder.Services.AddSingleton<EmployeesViewModel>();
            builder.Services.AddSingleton<EmployeePage>();

            //builder.Services.AddSingleton<TestPage>();
            //Inventory Sale
            builder.Services.AddSingleton<SaleViewModel>();
            

            builder.Services.AddSingleton<ViewModels.List.Inventory.StockViewModel>();
            builder.Services.AddSingleton<PurchaseViewModel>();

            ////Inventory Sale Entry
            //builder.Services.AddSingleton<SaleEntryViewModel>();

            //builder.Services.AddSingleton<InvoicePage>();







#if DEBUG
            builder.Logging.AddDebug();
           // builder.Services.AddDevHttpClient(7030);
#endif
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("Server Address") });
            return builder.Build();
        }
    }
}