using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;
using Syncfusion.Pdf.Barcode;
using AprajitaRetails.Mobile.Services.Print;



namespace AprajitaRetails.Mobile.Features.Printers.Thermals
{
    public abstract class ThermalPrinter
    {
        public PrintType PrintType { get; set; }
        public string StoreCode { get; set; } = "ARD";

        protected int PageWith = 150;
        protected int PageHeight = 1170;
        protected int FontSize = 7;
        protected float MarginTop, MarginBottom, MarginLeft, MarginRight;
        public bool Page2Inch { get; set; } = false;

        protected const string DotedLine = "---------------------------------\n";
        protected const string DotedLineLong = "--------------------------------------------------\n";

        protected const string FooterFirstMessage = "** Amount Inclusive GST **";

        protected string StoreName { get; set; }
        protected string Address { get; set; }
        protected string City { get; set; }
        protected string Phone { get; set; }
        protected string TaxNo { get; set; }

        public int NoOfCopy { get; set; }
        public bool Reprint { get; set; }

        public string PathName { get; set; }
        public string FileName { get; set; }

        protected string TitleName { get; set; }
        protected bool SubTitle { get; set; }
        protected string SubTitleName { get; set; }

        //Syncfusion Addittion
        protected float Top = 20;

        protected float X = 0, Y = 0;
        protected float LineSpace = 2;
        protected float Margin = 30;

        protected PdfStringFormat formatMiddleCenter = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
        protected PdfStringFormat formatMiddleLeft = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
        protected PdfStringFormat formatMiddleRight = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
        protected PdfStringFormat formatMiddleJustify = new PdfStringFormat(PdfTextAlignment.Justify, PdfVerticalAlignment.Middle);
        protected PdfGraphics graphics;
        protected PdfDocument document;
        protected PdfPage page;
        protected PdfMargins pdfMargins;

        //Colors
        protected static PdfColor darkBlue = Color.FromArgb(255, 65, 104, 209);

        protected static PdfBrush darkBlueBrush = new PdfSolidBrush(darkBlue);

        //Create a brush with a white color.
        protected static PdfBrush whiteBrush = new PdfSolidBrush(Color.FromArgb(255, 255, 255, 255));

        protected static PdfBrush blackBrush = new PdfSolidBrush(Color.Black);

        //Fonts
        protected static PdfFont HeaderFont;

        protected static PdfFont RegularFont;
        protected static PdfFont BoldFont;
        protected static PdfFont SmallFont;
        protected static PdfFont RegularSmallFont;

        protected void SetPageType(bool duplicate)
        {
            if (!Page2Inch)
            {
                PageWith = 240;
                FontSize = 10;
                if (duplicate)
                {
                    PageHeight = 1170 * 2;
                }
            }
            if (Page2Inch)
            { MarginTop = 90; MarginRight = 25; MarginBottom = 90; MarginLeft = 8; }
            else
            { MarginTop = 170; MarginRight = 25; MarginBottom = 90; MarginLeft = 35; }

            X = 0;
            Y = 0;

            pdfMargins = new PdfMargins();
            pdfMargins.Bottom = MarginBottom;
            pdfMargins.Top = MarginTop / 2;
            pdfMargins.Left = MarginLeft;
            pdfMargins.Right = MarginRight;

            HeaderFont = new PdfStandardFont(PdfFontFamily.TimesRoman, FontSize, PdfFontStyle.Bold);
            RegularFont = new PdfStandardFont(PdfFontFamily.TimesRoman, FontSize, PdfFontStyle.Regular);
            BoldFont = new PdfStandardFont(PdfFontFamily.TimesRoman, FontSize, PdfFontStyle.Bold);
            SmallFont = new PdfStandardFont(PdfFontFamily.Courier, FontSize - 2.5f, PdfFontStyle.Italic);
            RegularSmallFont = new PdfStandardFont(PdfFontFamily.Helvetica, FontSize - 1.5f, PdfFontStyle.Bold);
        }

        protected void GenrateFileName(string number)
        {
            if (string.IsNullOrEmpty(PathName))
            {
                PathName = $@"{StoreCode}\";
                switch (PrintType)
                {
                    case PrintType.Invoice:
                        PathName = Path.Combine(PathName, "SaleInvoices");
                        break;

                    case PrintType.PaymentVoucher:
                        PathName = Path.Combine(PathName, "Vouchers\\Payments");
                        break;

                    case PrintType.ReceiptVocuher:
                        PathName = Path.Combine(PathName, "Vouchers\\Receipts");
                        break;

                    case PrintType.CashPaymentVoucher:
                        PathName = Path.Combine(PathName, "Vouchers\\CashPayments");
                        break;

                    case PrintType.CashReceiptVocucher:
                        PathName = Path.Combine(PathName, "Vouchers\\CashReciepts");
                        break;

                    case PrintType.DebitNote:
                        PathName = Path.Combine(PathName, "Vouchers\\DebitNotes");
                        break;

                    case PrintType.CreditNote:
                        PathName = Path.Combine(PathName, "Vouchers\\CreditNotes");
                        break;

                    case PrintType.Expenses:
                        PathName = Path.Combine(PathName, "Vouchers\\ExpensesSlips");
                        break;

                    case PrintType.Note:
                        PathName = Path.Combine(PathName, "Others\\Notes");
                        break;

                    case PrintType.Payslip:
                        PathName = Path.Combine(PathName, "PaySlips");
                        break;

                    default:
                        PathName = Path.Combine(PathName, "Others\\UnSorted");
                        break;
                }
                // Directory.CreateDirectory(PathName);
                FileName = Path.Combine(PathName, $"{number}.pdf");
                Directory.CreateDirectory(FileName.Replace(Path.GetFileName(FileName), ""));
            }
            else
            {
                Directory.CreateDirectory(FileName.Replace(Path.GetFileName(FileName), ""));
            }
        }

        protected void SetStoreInfo()
        {
            this.StoreCode = CurrentSession.StoreCode;
            this.Address = CurrentSession.Address;
            this.City = CurrentSession.CityName;
            this.Phone = CurrentSession.PhoneNo;
            this.StoreName = CurrentSession.StoreName;
            this.TaxNo = CurrentSession.TaxNumber;
        }

        protected void TitleText()
        {
            AddRegularText("  ");
            AddDotedLine();
            AddNormalText(TitleName, formatMiddleCenter);
            AddDotedLine();
            if (SubTitle)
            {
                AddNormalText(SubTitleName, formatMiddleCenter);
                AddDotedLine();
            }
        }

        protected void HeaderText()
        {
            AddHeaderText(StoreName);
            AddHeaderText(Address);
            AddHeaderText(City);
            AddHeaderText("Ph No: " + Phone);
            AddHeaderText(TaxNo);
            //AddSpace();
        }

        protected void AddHeaderText(string text)
        {
            if (text.Contains("\n"))
            {
                var lines = text.Split("\n");
                foreach (var line in lines)
                {
                    SizeF textSize2 = HeaderFont.MeasureString(line);
                    graphics.DrawString(line, HeaderFont, blackBrush, new RectangleF(X, Y, textSize2.Width + 25, textSize2.Height + 10), formatMiddleCenter);
                    Y += RegularFont.Height;// + LineSpace;
                }
                Y += LineSpace;
            }
            else
            {
                SizeF textSize = HeaderFont.MeasureString(text);
                graphics.DrawString(text, HeaderFont, blackBrush, new RectangleF(X, Y, textSize.Width + 25, textSize.Height + 10), formatMiddleCenter);
                Y += RegularFont.Height + LineSpace;
            }
        }

        protected void AddNormalText(string text)
        {
            if (text.Contains("\n"))
            {
                var lines = text.Split("\n");
                foreach (var line in lines)
                {
                    SizeF textSize2 = BoldFont.MeasureString(line);
                    graphics.DrawString(line, BoldFont, blackBrush, new PointF(X, Y), formatMiddleJustify);
                    Y += BoldFont.Height;
                }
                Y += LineSpace;
            }
            else
            {
                SizeF textSize = BoldFont.MeasureString(text);
                graphics.DrawString(text, BoldFont, blackBrush, new PointF(X, Y), formatMiddleJustify);
                Y += BoldFont.Height + LineSpace;
            }
        }

        protected void AddRegularText(string text)
        {
            if (text.Contains("\n"))
            {
                var lines = text.Split("\n");
                foreach (var line in lines)
                {
                    SizeF textSize2 = RegularFont.MeasureString(line);
                    graphics.DrawString(line, RegularFont, blackBrush, new RectangleF(X, Y - 5, textSize2.Width + 25, textSize2.Height + 10), formatMiddleJustify);
                    Y += RegularFont.Height;// + LineSpace;
                }
                Y += LineSpace + 2;
            }
            else
            {
                SizeF textSize = RegularFont.MeasureString(text);
                graphics.DrawString(text, RegularFont, blackBrush, new RectangleF(X, Y - 5, textSize.Width + 25, textSize.Height + 10), formatMiddleJustify);
                Y += RegularFont.Height + LineSpace + 2;
            }
        }

        protected void AddNormalText(string text, PdfStringFormat format)
        {
            if (text.Contains("\n"))
            {
                var lines = text.Split("\n");
                foreach (var line in lines)
                {
                    SizeF textSize2 = BoldFont.MeasureString(line);
                    graphics.DrawString(line, BoldFont, blackBrush, new RectangleF(X, Y - 5, textSize2.Width + 25, textSize2.Height + 10), format);
                    Y += BoldFont.Height;
                }
                Y += LineSpace + 2;
            }
            else
            {
                SizeF textSize = BoldFont.MeasureString(text);
                graphics.DrawString(text, BoldFont, blackBrush, new RectangleF(X, Y - 5, textSize.Width + 25, textSize.Height + 10), format);
                Y += BoldFont.Height + LineSpace + 2;
            }
        }

        protected void AddRegularText(string text, PdfStringFormat format)
        {
            if (text.Contains("\n"))
            {
                var lines = text.Split("\n");
                foreach (var line in lines)
                {
                    SizeF textSize2 = RegularFont.MeasureString(line);
                    graphics.DrawString(line, RegularFont, blackBrush, new RectangleF(X, Y - 5, textSize2.Width + 25, textSize2.Height + 10), format);
                    Y += RegularFont.Height;
                }
                Y += LineSpace + 2;
            }
            else
            {
                SizeF textSize = RegularFont.MeasureString(text);

                graphics.DrawString(text, RegularFont, blackBrush, new RectangleF(X, Y - 5, textSize.Width + 25, textSize.Height + 10), format);
                Y += RegularFont.Height + LineSpace + 2;
            }
        }

        protected void AddSmallText(string text)
        {
            if (text.Contains("\n"))
            {
                var lines = text.Split("\n");
                foreach (var line in lines)
                {
                    SizeF textSize2 = SmallFont.MeasureString(line);
                    graphics.DrawString(line, SmallFont, blackBrush, new PointF(X, Y), formatMiddleJustify);
                    Y += SmallFont.Height;
                }
                Y += LineSpace - 1;
            }
            else
            {
                SizeF textSize = SmallFont.MeasureString(text);
                graphics.DrawString(text, SmallFont, blackBrush, new PointF(X, Y), formatMiddleJustify);
                Y += SmallFont.Height + LineSpace - 1;
            }
        }

        protected void AddSubText(string text)
        {
            if (text.Contains("\n"))
            {
                var lines = text.Split("\n");
                foreach (var line in lines)
                {
                    SizeF textSize2 = RegularSmallFont.MeasureString(line);
                    graphics.DrawString(line, RegularSmallFont, blackBrush, new PointF(X, Y), formatMiddleJustify);
                    Y += RegularSmallFont.Height;
                }
                Y += LineSpace - 1;
            }
            else
            {
                SizeF textSize = RegularSmallFont.MeasureString(text);
                graphics.DrawString(text, RegularSmallFont, blackBrush, new PointF(X, Y), formatMiddleJustify);
                Y += RegularSmallFont.Height + LineSpace - 1;
            }
        }

        protected void AddDotedLine()
        {
            //if (!Page2Inch) AddNormalText(DotedLineLong, formatMiddleCenter); else AddNormalText(DotedLine, formatMiddleCenter);
            AddLine();
        }

        protected void AddLine()
        {
            //graphics.DrawRectangle(darkBlueBrush, new RectangleF(0, Y, PageWith, 5));
            graphics.DrawLine(PdfPens.Blue, 0, Y, PageWith, Y);
            Y += 6;
        }

        protected void AddSpace()
        {
            Y += ((RegularFont.Height + LineSpace) * 3);
        }

        //Abstract Methods
        public abstract MemoryStream PrintPdf(bool duplicate, bool print = false);

        protected abstract void Content();

        protected abstract void Footer();

        protected abstract void DuplicateFooter();

        protected abstract void QRBarcode();

        protected void QRCode(string text)
        {
            PdfQRBarcode barcode = new PdfQRBarcode();
            barcode.ErrorCorrectionLevel = PdfErrorCorrectionLevel.High;
            barcode.XDimension = 3;
            barcode.Text = text;
            SizeF size = new SizeF(35, 35);
            barcode.Draw(page, new PointF((PageWith / 2) - 45, Y), size);
            Y += size.Height + LineSpace;
        }

        //Init
        protected bool Init(bool duplicate = false)
        {
            try
            {
                SetPageType(duplicate);
                SetStoreInfo();
                document = new PdfDocument();

                var pSize = new SizeF(PageWith, PageHeight);
                document.PageSettings.Size = pSize;
                document.PageSettings.Margins = pdfMargins;
                document.PageSettings.Orientation = PdfPageOrientation.Portrait;

                //document.PageSettings.Margins;
                page = document.Pages.Add();
                graphics = page.Graphics;
                HeaderText();
                TitleText();
                AddWaterMark();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        protected void AddWaterMark()
        {
            PdfGraphicsState state = graphics.Save();

            graphics.SetTransparency(0.25f);

            graphics.RotateTransform(-40);

            graphics.DrawString(StoreName + "\n" + TitleName, new PdfStandardFont(PdfFontFamily.Helvetica, 20), PdfPens.Red, PdfBrushes.Red, new PointF(-150, 450));
        }

        protected MemoryStream Save(bool print = false)
        {
            using MemoryStream ms = new();
            //Saves the presentation to the memory stream.
            document.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as a file.
            SavePdf(ms, FileName);

            //Print the pdf file
            if (print)
                PrintPdf(ms, FileName);

            return ms;
        }

        public static void SavePdf(MemoryStream memory, string fileName)
        {
            ServiceHelper.GetService<ISave>().SaveAndView(fileName, "application/pdf", memory);
        }

        public static void PrintPdf(MemoryStream memory, string fileName)
        {
            ServiceHelper.GetService<IPrinterService>().Print(memory, fileName);
        }
    }
}
