using AprajitaRetails.Shared.Models.Inventory;
using System.ComponentModel.DataAnnotations;



namespace AprajitaRetails.Mobile.Features.Printers.Thermals
{
    public class InvoicePrint : ThermalPrinter
    {
        #region Fields
        private const string InvoiceTitle = "                 RETAIL INVOICE";
        private const string ItemLineHeader1 = "SKU Code/Description/ HSN";
        private const string ItemLineHeader2 = "MRP     Qty     Disc     Amount";
        private const string ItemLineHeader3 = "CGST%    AMT     SGST%   AMT";

        private const string FooterThanksMessage = "Thank You";
        private const string FooterLastMessage = "Visit Again";

        private const string Employee = "Cashier: M0001      Name: Manager";

        [Required]
        public bool IsDataSet { get; set; }

        public bool ServiceBill { get; set; } = false;

        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }

        public ProductSale ProductSale { get; set; }
        public List<SalePaymentDetail> PaymentDetails { get; set; }
        public CardPaymentDetail CardDetails { get; set; }
        #endregion
        public override MemoryStream PrintPdf(bool duplicate, bool print = false)
        {
            try
            {
                SetPageType(duplicate);
                if (!IsDataSet) return null;

                this.PrintType = PrintType.Invoice;
                TitleName = InvoiceTitle;
                if (ServiceBill)
                {
                    SubTitle = true;
                    SubTitleName = "Service Invoice";
                }
                SetStoreInfo();
                Init();
                QRBarcode();
                Content();
                Footer();
                AddSpace();
                if (duplicate)
                {
                    AddDotedLine();
                    AddSpace();
                    HeaderText();
                    TitleText();

                    Content();
                    DuplicateFooter();
                    AddSpace();
                }
                return Save(print);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        protected override void Content()
        {
            //Details

            AddNormalText(Employee);
            AddNormalText("Bill No: " + ProductSale.InvoiceNo);

            AddNormalText("  " + "                  Date: " + ProductSale.OnDate.ToString());

            //AddNormalText("  " + "                  Time: " + ProductSale.OnDate.ToShortTimeString()  );

            AddLine();
            AddNormalText("Customer Name: " + CustomerName);
            AddNormalText("Customer Mobile: " + MobileNumber);
            AddLine();

            AddNormalText(ItemLineHeader1);
            AddNormalText(ItemLineHeader2);

            AddLine();

            decimal gstPrice = 0;
            decimal basicPrice = 0;
            string tab = "    ";

            foreach (var itemDetails in ProductSale.Items)
            {
                //TODO: Need to implement HSNCode.
                if (itemDetails != null)
                {
                    AddNormalText($"{itemDetails.Barcode} / {itemDetails.ProductItem.Description}/{itemDetails.ProductItem.HSNCode} /");
                    AddNormalText((itemDetails.Value + itemDetails.DiscountAmount).ToString("0.##") + tab + tab);
                    if (itemDetails.Value == 0)
                    {
                        AddNormalText(itemDetails.BilledQty + tab + tab + itemDetails.DiscountAmount.ToString("0.##") + tab + tab + "Free");
                    }
                    else
                    {
                        AddNormalText(itemDetails.BilledQty + tab + tab + itemDetails.DiscountAmount.ToString("0.##") + tab + tab + itemDetails.Value.ToString("0.##"));
                    }
                    //AddNormalText(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + tab + tab);
                    //AddNormalText(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount  );
                    gstPrice += itemDetails.TaxAmount;
                    basicPrice += itemDetails.BasicAmount;
                }
            }
            AddLine();

            AddNormalText("Total: " + ProductSale.BilledQty + tab + tab + tab + tab + tab + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##"));
            AddNormalText("item(s): " + ProductSale.TotalQty + tab + "Net Amount:" + tab + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##"));

            AddLine();

            AddNormalText("Tender (s)\t\n Paid Amount:\t\t Rs. " + (ProductSale.TotalPrice - ProductSale.RoundOff).ToString("0.##"));

            AddLine();
            AddNormalText("Basic Price: " + basicPrice.ToString("0.##"));
            AddNormalText("CGST: " + gstPrice.ToString("0.##"));
            AddNormalText("SGST: " + gstPrice.ToString("0.##"));
            AddLine();

            if (PaymentDetails.Count > 0)
            {
                AddNormalText(DotedLine);
                foreach (var pd in PaymentDetails)
                {
                    AddNormalText($"Paid Rs. {pd.PaidAmount.ToString("0.##")} in {pd.PayMode}");
                    if (pd.PayMode == PayMode.Card)
                    {
                        if (CardDetails != null)
                            AddNormalText($"{CardDetails.CardType}/{CardDetails.CardLastDigit}");
                    }
                    else if (pd.PayMode == PayMode.UPI || pd.PayMode == PayMode.Wallets)
                    {
                        AddNormalText($"RefNo:{pd.RefId} ");
                    }
                }
                AddLine();
            }

        }

        protected override void DuplicateFooter()
        {

        }

        protected override void Footer()
        {
            //Footer

            AddSubText(FooterFirstMessage);
            if (ServiceBill) AddSubText("** Tailoring Service Invoice **");
            AddLine();
            AddSubText(FooterThanksMessage);
            AddSubText(FooterLastMessage);
            AddLine();

            AddNormalText("\n");// Just to Check;

            if (Reprint)
            {
                AddNormalText("(Reprinted Duplicate)", formatMiddleCenter);
            }
            else
            {
                AddNormalText("(Customer Copy)", formatMiddleCenter);
            }

            AddNormalText("Printed on: " + DateTime.Now + "\n\n\n\n\n");
            AddNormalText("\n\n\n\n\n");

        }

        protected override void QRBarcode()
        {
            //ProductSale.InvoiceNo, ProductSale.OnDate, ProductSale.TotalPrice
            QRCode($"InvNo:{ProductSale.InvoiceNo} On {ProductSale.OnDate.ToString()} of Rs. {ProductSale.TotalPrice}/-");
        }
    }
}
