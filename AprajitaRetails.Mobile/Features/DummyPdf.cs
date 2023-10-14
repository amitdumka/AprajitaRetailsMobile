using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;
using AprajitaRetails.Mobile.Services.Print;


namespace AprajitaRetails.Mobile.Features.Helpers
{
    public class DummyPdf
    {
        //For Release Model make it within context
        private static PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 30);

        private static PdfFont RegularFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 18);
        private static PdfFont BoldFont = new PdfStandardFont(PdfFontFamily.Courier, 9);

        //Create a border pen and draw the border to on the PDF page.
        private static PdfColor borderColor = Color.FromArgb(255, 142, 170, 219);

        private static PdfPen borderPen = new PdfPen(borderColor, 1f);

        //Create a brush with a light blue color.
        private static PdfColor lightBlue = Color.FromArgb(255, 91, 126, 215);

        private static PdfBrush lightBlueBrush = new PdfSolidBrush(lightBlue);

        //Create a brush with a dark blue color.
        private static PdfColor darkBlue = Color.FromArgb(255, 65, 104, 209);

        private static PdfBrush darkBlueBrush = new PdfSolidBrush(darkBlue);

        //Create a brush with a white color.
        private static PdfBrush whiteBrush = new PdfSolidBrush(Color.FromArgb(255, 255, 255, 255));

        //Set the header height.
        private static float headerHeight = 90;

        public static MemoryStream PrintPdf(string headerTitle, List<string> lines, string fileName)
        {
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            document.PageSettings.Size = PdfPageSize.Note;
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Get the page width and height.
            float pageWidth = page.GetClientSize().Width;
            float pageHeight = page.GetClientSize().Height;

            //Create a string format.
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            float y = 0;
            float x = 0;

            //Set the margins of the address.
            float margin = 30;

            //Set the line space.
            float lineSpace = 10;

            graphics.DrawRectangle(borderPen, new RectangleF(0, 0, pageWidth, pageHeight));
            //Fill the header with a light blue brush.
            graphics.DrawRectangle(lightBlueBrush, new RectangleF(0, 0, pageWidth, headerHeight));

            //Specificy the bounds for the total value.
            RectangleF headerTotalBounds = new RectangleF(400, 0, pageWidth - 400, headerHeight);

            //Measure the string size using the font.
            SizeF textSize = headerFont.MeasureString(headerTitle);
            graphics.DrawString(headerTitle, headerFont, whiteBrush, new RectangleF(0, 0, textSize.Width + 50, headerHeight), format);

            //Draw a rectangle in the PDF page.
            graphics.DrawRectangle(darkBlueBrush, headerTotalBounds);

            y = headerHeight + margin;
            x = margin;
            foreach (var line in lines)
            {
                //Draw text to a PDF page with the provided font and location.
                graphics.DrawString(line, RegularFont, PdfBrushes.Black, new PointF(x, y));
                y += RegularFont.Height + lineSpace;
            }

            //Saving and printing
            using MemoryStream ms = new();

            //Saves the presentation to the memory stream.
            document.Save(ms);
            ms.Position = 0;

            //Saves the memory stream as a file.
            SavePdf(ms, fileName);
            //Print the pdf file
            PrintPdf(ms, fileName);

            return ms;
        }

        public static MemoryStream Get(string fileName)
        {
            RectangleF TotalPriceCellBounds = RectangleF.Empty;
            RectangleF QuantityCellBounds = RectangleF.Empty;

            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Get the page width and height.
            float pageWidth = page.GetClientSize().Width;
            float pageHeight = page.GetClientSize().Height;

            //Create a string format.
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            float y = 0;
            float x = 0;

            //Set the margins of the address.
            float margin = 30;

            //Set the line space.
            float lineSpace = 10;

            graphics.DrawRectangle(borderPen, new RectangleF(0, 0, pageWidth, pageHeight));
            //Fill the header with a light blue brush.
            graphics.DrawRectangle(lightBlueBrush, new RectangleF(0, 0, pageWidth, headerHeight));

            string title = "INVOICE";

            //Specificy the bounds for the total value.
            RectangleF headerTotalBounds = new RectangleF(400, 0, pageWidth - 400, headerHeight);

            //Measure the string size using the font.
            SizeF textSize = headerFont.MeasureString(title);
            graphics.DrawString(title, headerFont, whiteBrush, new RectangleF(0, 0, textSize.Width + 50, headerHeight), format);

            //Draw a rectangle in the PDF page.
            graphics.DrawRectangle(darkBlueBrush, headerTotalBounds);

            //Draw the total value to the PDF page.
            graphics.DrawString("$" + "6000", RegularFont, whiteBrush, new RectangleF(400, 0, pageWidth - 400, headerHeight + 10), format);

            //Create a font from the font stream.
            // arialRegularFont = new PdfTrueTypeFont(fontStream, 9, PdfFontStyle.Regular);
            //Set the bottom line alignment and draw the text to the PDF page.
            format.LineAlignment = PdfVerticalAlignment.Bottom;
            graphics.DrawString("Amount", RegularFont, whiteBrush, new RectangleF(400, 0, pageWidth - 400, headerHeight / 2 - RegularFont.Height), format);

            //Measure the string size using the font.
            SizeF size = RegularFont.MeasureString("Invoice Number: 2058557939");
            y = headerHeight + margin;
            x = (pageWidth - margin) - size.Width;
            //Draw text to a PDF page with the provided font and location.
            graphics.DrawString("Invoice Number: 2058557939", RegularFont, PdfBrushes.Black, new PointF(x, y));
            //Measure the string size using the font.
            size = RegularFont.MeasureString("Date :" + DateTime.Now.ToString("dddd dd, MMMM yyyy"));
            x = (pageWidth - margin) - size.Width;
            y += RegularFont.Height + lineSpace;
            //Draw text to a PDF page with the provided font and location.
            graphics.DrawString("Date: " + DateTime.Now.ToString("dddd dd, MMMM yyyy"), RegularFont, PdfBrushes.Black, new PointF(x, y));

            y = headerHeight + margin;
            x = margin;
            //Draw text to a PDF page with the provided font and location.
            graphics.DrawString("Bill To:", RegularFont, PdfBrushes.Black, new PointF(x, y));
            y += RegularFont.Height + lineSpace;
            graphics.DrawString("Abraham Swearegin,", RegularFont, PdfBrushes.Black, new PointF(x, y));
            y += RegularFont.Height + lineSpace;
            graphics.DrawString("United States, California, San Mateo,", RegularFont, PdfBrushes.Black, new PointF(x, y));
            y += RegularFont.Height + lineSpace;
            graphics.DrawString("9920 BridgePointe Parkway,", RegularFont, PdfBrushes.Black, new PointF(x, y));
            y += RegularFont.Height + lineSpace;
            graphics.DrawString("9365550136", RegularFont, PdfBrushes.Black, new PointF(x, y));

            using MemoryStream ms = new();

            //Saves the presentation to the memory stream.
            document.Save(ms);
            ms.Position = 0;

            //Saves the memory stream as a file.
            SavePdf(ms, fileName);
            //Print the pdf file
            PrintPdf(ms, fileName);

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

 
 
 
