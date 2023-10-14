

namespace AprajitaRetails.Mobile.Features.Printers.Thermals
{
    public class PaySlipPrint : ThermalPrinter
    {
        public bool IsDataSet { get; set; }
        public string PaySlipNo { get; set; }
        public string StaffName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal WorkingDay { get; set; }
        public decimal Present { get; set; }
        public decimal Absent { get; set; }
        public decimal BasicDayRate { get; set; }
        public decimal Incentive { get; set; }
        public decimal WowBill { get; set; }
        public decimal LastPcs { get; set; }

        public decimal LastMonthAdvance { get; set; }
        public decimal SalaryAdvance { get; set; }
        public decimal CurrentMonthSalary { get; set; }
        public decimal NetPayableSalary { get; set; }

        public override MemoryStream PrintPdf(bool duplicate, bool print = false)
        {
            try
            {
                SetPageType(duplicate);
                if (!IsDataSet) return null;

                this.PrintType = PrintType.Payslip;

                //if (!FileName.Contains(PathName))
                //{
                //    FileName = Path.Combine(PathName, FileName);
                //}
                this.TitleName = $"Pay Slip";
                this.SubTitle = true;
                this.SubTitleName = $"For Month of {Month}/{Year}";
                SetStoreInfo();
                PathName = $@"{StoreCode}/PaySlips/{StaffName}/{Year}/{Month}/";
                //Directory.CreateDirectory(FileName.Replace(Path.GetFileName(FileName), ""));
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

        protected override void QRBarcode()
        {
            QRCode($"PaySlip:{PaySlipNo} On {DateTime.Now.ToString()} of Rs. {NetPayableSalary}/-");
        }

        protected override void Content()
        {
            AddDotedLine();
            AddNormalText("PaySlip No:" + PaySlipNo, formatMiddleCenter);
            AddNormalText("Printed Date: \n" + DateTime.Now.ToString(), formatMiddleCenter);
            AddDotedLine();
            AddNormalText("Employee Name: \n\t" + StaffName, formatMiddleCenter);
            AddNormalText($"Address: {City}");
            AddNormalText($"Period: {Month} / {Year}");
            AddDotedLine();
            AddNormalText($"Working Days: {WorkingDay} \nBasic Rate: Rs. {BasicDayRate.ToString("0.##")}");
            AddNormalText($"Present: {Present.ToString("0.##")} \t Absent: {Absent.ToString("0.##")}");
            if (WowBill > 0 || LastPcs > 0)
                AddNormalText($"WowBill: Rs. {WowBill.ToString("0.##")} \n Last Pcs: Rs. {LastPcs.ToString("0.##")}");
            if (Incentive > 0)
                AddNormalText($"Incentive: Rs. {Incentive.ToString("0.##")}");
            AddDotedLine();
            AddNormalText("Deductions:");
            AddNormalText($"Salary Advance: Rs. {LastMonthAdvance.ToString("0.##")}\nAdavance: Rs. {SalaryAdvance.ToString("0.##")}");
            AddNormalText($"Net Deduction:Rs. {(LastMonthAdvance + SalaryAdvance).ToString("0.##")}");
            AddDotedLine();
            AddNormalText("Salary:");
            AddNormalText($"Net Salary: Rs. {CurrentMonthSalary.ToString("0.##")}");
            AddNormalText($"Net Payable: Rs. {NetPayableSalary.ToString("0.##")}\n(After deductions)");
        }

        protected override void Footer()
        {
            AddDotedLine();
            AddSubText("This is computer generated payslip.");
            AddSubText(" No Sign is requried.");
            AddDotedLine();
            AddSmallText("No of present day also includes half day, \npaid leave, sick leaves and sunday(s)\n");
            AddSmallText("Deducations are basic and computer generated. \nIn actual can varries based on request.\n");
            AddSmallText("Last month advance is deducted \nand Salary advance may not be deducted!.\n\n");
            AddDotedLine();
            AddRegularText($"For {StoreName}\n\n_______________\n(SM/Accounts)\n Signature" + "\n");
            AddRegularText("\n_______________\nEmp Signature" + "\n");
            AddDotedLine();
            if (Reprint)
            {
                AddNormalText("(Reprinted Orginal)\n");
            }
            else
            {
                AddNormalText("(Orignal Copy)\n");
            }
            AddRegularText("Printed on: " + DateTime.Now + "\n\n\n\n\n");
            AddRegularText("\n\n\n\n\n");
        }

        protected override void DuplicateFooter()
        {
            AddDotedLine();
            AddSubText("This is computer generated payslip ");//, formatMiddleCenter);
            AddSubText("No Sign is requried");//, formatMiddleCenter);
            AddDotedLine();

            AddSmallText("No of present day also includes half day, paid leave, sick leaves and sunday(s)\n");
            AddSmallText("Deducations are basic and computer generated. In actual can varries based on request.\n");
            AddSmallText("Last month advance is deducted and Salary advance may not be deducted!.\n\n");
            AddDotedLine();
            AddRegularText($"For {StoreName}\n\n_______________\n(SM/Accounts)\n Signature" + "\n", formatMiddleCenter);
            AddRegularText("\n_______________\nEmp Signature" + "\n", formatMiddleCenter);
            AddDotedLine();
            if (Reprint)
            {
                AddNormalText("(Reprinted Duplicate)", formatMiddleCenter);
            }
            else
            {
                AddNormalText("(Duplicate Copy)", formatMiddleCenter);
            }
            AddNormalText("Printed on: " + DateTime.Now + "\n\n\n\n\n");
            AddNormalText("\n\n\n\n\n");
        }
    }
}
