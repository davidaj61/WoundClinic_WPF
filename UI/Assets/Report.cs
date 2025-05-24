//using System;
//using System.Drawing;
//using System.Windows.Forms;
//using System.Data;
//using Stimulsoft.Controls;
//using Stimulsoft.Base.Drawing;
//using Stimulsoft.Report;
//using Stimulsoft.Report.Dialogs;
//using Stimulsoft.Report.Components;

//namespace Reports
//{
//    public class Report : Stimulsoft.Report.StiReport
//    {
//        public Report()        {
//            this.InitializeComponent();
//        }

//        #region StiReport Designer generated code - do not modify
//        public Stimulsoft.Report.Components.StiPage Page1;
//        public Stimulsoft.Report.Components.StiImage Image1;
//        public Stimulsoft.Report.Components.StiText Text1;
//        public Stimulsoft.Report.Components.StiText Text2;
//        public Stimulsoft.Report.Components.StiWatermark Page1_Watermark;
//        public Stimulsoft.Report.Print.StiPrinterSettings Report_PrinterSettings;
        
//        public void Text1__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
//        {
//            // CheckerInfo: Text Text1
//            e.Value = "کلینیک زخم بیمارستان سیدالشهدا";
//        }
        
//        public void Text2__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
//        {
//            // CheckerInfo: Text Text2
//            e.Value = "برگه پذیرش بیمار";
//        }
        
//        private void InitializeComponent()
//        {
//            this.NeedsCompiling = false;
//            this.EngineVersion = Stimulsoft.Report.Engine.StiEngineVersion.EngineV2;
//            this.ReferencedAssemblies = new string[] {
//                    "System.Dll",
//                    "System.Drawing.Dll",
//                    "System.Windows.Forms.Dll",
//                    "System.Data.Dll",
//                    "System.Xml.Dll",
//                    "Stimulsoft.Controls.Dll",
//                    "Stimulsoft.Base.Dll",
//                    "Stimulsoft.Report.Dll"};
//            this.ReportAlias = "Report";
//            // 
//            // ReportChanged
//            // 
//            this.ReportChanged = new DateTime(2025, 5, 24, 15, 0, 14, 325);
//            // 
//            // ReportCreated
//            // 
//            this.ReportCreated = new DateTime(2025, 5, 24, 14, 45, 56, 38);
//            this.ReportFile = "";
//            this.ReportGuid = "c8b4ca4645294aafba5596a04211ffa4";
//            this.ReportName = "Report";
//            this.ReportUnit = Stimulsoft.Report.StiReportUnitType.Centimeters;
//            this.ReportVersion = "2017.2.2.0";
//            this.ScriptLanguage = Stimulsoft.Report.StiReportLanguageType.CSharp;
//            // 
//            // Page1
//            // 
//            this.Page1 = new Stimulsoft.Report.Components.StiPage();
//            this.Page1.Guid = "6e77bcfc536c4e6d92d369822df8d60e";
//            this.Page1.Name = "Page1";
//            this.Page1.PageHeight = 21;
//            this.Page1.PageWidth = 14.8;
//            this.Page1.PaperSize = System.Drawing.Printing.PaperKind.A5;
//            this.Page1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 2, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
//            this.Page1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
//            // 
//            // Image1
//            // 
//            this.Image1 = new Stimulsoft.Report.Components.StiImage();
//            this.Image1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10, 0.4, 2.4, 2.4);
//            this.Image1.File = "C:\\Users\\Sahand\\source\\repos\\WoundClinic_WPF\\UI\\Assets\\logo.jpg";
//            this.Image1.Name = "Image1";
//            this.Image1.Stretch = true;
//            this.Image1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
//            this.Image1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
//            this.Image1.Guid = null;
//            this.Image1.ImageBytes = null;
//            this.Image1.Interaction = null;
//            this.Image1.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
//            // 
//            // Text1
//            // 
//            this.Text1 = new Stimulsoft.Report.Components.StiText();
//            this.Text1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.6, 0.8, 5.6, 0.8);
//            this.Text1.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
//            this.Text1.Name = "Text1";
//            this.Text1.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text1__GetValue);
//            this.Text1.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
//            this.Text1.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
//            this.Text1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
//            this.Text1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
//            this.Text1.Font = new System.Drawing.Font("Vazirmatn Black", 10F);
//            this.Text1.Guid = null;
//            this.Text1.Indicator = null;
//            this.Text1.Interaction = null;
//            this.Text1.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
//            this.Text1.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 0, 0, 0));
//            this.Text1.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
//            // 
//            // Text2
//            // 
//            this.Text2 = new Stimulsoft.Report.Components.StiText();
//            this.Text2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.6, 1.6, 5.6, 1.2);
//            this.Text2.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
//            this.Text2.Name = "Text2";
//            this.Text2.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text2__GetValue);
//            this.Text2.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
//            this.Text2.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
//            this.Text2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
//            this.Text2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
//            this.Text2.Font = new System.Drawing.Font("Vazirmatn ExtraBold", 16F);
//            this.Text2.Guid = null;
//            this.Text2.Indicator = null;
//            this.Text2.Interaction = null;
//            this.Text2.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
//            this.Text2.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
//            this.Text2.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
//            this.Page1.ExcelSheetValue = null;
//            this.Page1.Interaction = null;
//            this.Page1.Margins = new Stimulsoft.Report.Components.StiMargins(1, 1, 1, 1);
//            this.Page1_Watermark = new Stimulsoft.Report.Components.StiWatermark();
//            this.Page1_Watermark.Font = new System.Drawing.Font("Arial", 100F);
//            this.Page1_Watermark.ImageBytes = null;
//            this.Page1_Watermark.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0));
//            this.Report_PrinterSettings = new Stimulsoft.Report.Print.StiPrinterSettings();
//            this.ReportImage = null;
//            this.PrinterSettings = this.Report_PrinterSettings;
//            this.Page1.Report = this;
//            this.Page1.Watermark = this.Page1_Watermark;
//            this.Image1.Page = this.Page1;
//            this.Image1.Parent = this.Page1;
//            this.Text1.Page = this.Page1;
//            this.Text1.Parent = this.Page1;
//            this.Text2.Page = this.Page1;
//            this.Text2.Parent = this.Page1;
//            // 
//            // Add to Page1.Components
//            // 
//            this.Page1.Components.Clear();
//            this.Page1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
//                        this.Image1,
//                        this.Text1,
//                        this.Text2});
//            // 
//            // Add to Pages
//            // 
//            this.Pages.Clear();
//            this.Pages.AddRange(new Stimulsoft.Report.Components.StiPage[] {
//                        this.Page1});
//        }
//        #endregion StiReport Designer generated code - do not modify
//    }
//}
